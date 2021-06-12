using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace Application
{
    public class Startup : object
    {
        public const string AdminCorsPolicy = "_ADMIN_CORS_POLICY";
        public Startup
            (Microsoft.Extensions.Configuration.IConfiguration configuration)
        {
            Configuration = configuration;
        }
         
        public Microsoft.Extensions.Configuration.IConfiguration Configuration { get; }

        public void ConfigureServices
            (Microsoft.Extensions.DependencyInjection.IServiceCollection services)
        {
            services.AddDbContext<Data.DatabaseContext>(options =>
                options.UseSqlServer
                    (connectionString: Configuration.GetSection(key: "ConnectionStrings").GetSection(key: "MyConnectionString").Value));

            services.AddCors(options =>
            {
                options.AddPolicy(AdminCorsPolicy,
                    builder =>
                    {
                        builder
                            //.WithOrigins("http://10.10.1.14:5003","http://31.47.54.247:5000")
                            .WithOrigins("https://localhost:44313")
                            .AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                    });
            });

            //services.AddSignalR();

            //services.AddControllers()
            //    .ConfigureApiBehaviorOptions(options => {
            //        options.SuppressModelStateInvalidFilter = true;
            //    }).SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Latest);
            services.AddControllers();
            services.AddHttpClient();
            services.AddHttpContextAccessor();

            // Configure strongly typed settings object
            services.Configure<Infrastructure.ApplicationSettings.Main>
                (Configuration.GetSection("AppSettings"));

            // Configure Swagger for application services
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Version = "v1",
                    Title = "Swagger Demo",
                    Description = "Swagger Demo",
                    TermsOfService = new System.Uri("https://example.com/terms"),
                    Contact = new Microsoft.OpenApi.Models.OpenApiContact()
                    {
                        Name = "Dariush Tasdighi",
                        Email = "Dariush.Tasdighi@Gmail.com",
                        Url = new System.Uri("https://www.linkedin.com/in/dariush-tasdighi"),
                    },
                    License = new Microsoft.OpenApi.Models.OpenApiLicense
                    {
                        Name = "Use under LICX",
                        Url = new System.Uri("https://example.com/license"),
                    }
                });

                c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme.",
                    Name = "Authorization",
                    In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                    Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement()
                {
                    {
                        new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                        {
                            Reference = new Microsoft.OpenApi.Models.OpenApiReference
                            {
                                Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                        },
                        new System.Collections.Generic.List<string>()
                    }
                });

                var xmlPath = System.IO.Path.Combine(System.AppContext.BaseDirectory, "Application.xml");
                c.IncludeXmlComments(xmlPath);

            });

            services.AddTransient<Data.IUnitOfWork, Data.UnitOfWork>(sp =>
            {
                Data.Tools.Options options =
                    new Data.Tools.Options
                    {
                        Provider =
                            (Data.Tools.Enums.Provider)
                            System.Convert.ToInt32(Configuration.GetSection(key: "databaseProvider").Value),

                        ConnectionString =
                            Configuration.GetSection(key: "ConnectionStrings").GetSection(key: "MyConnectionString").Value,
                    };

                return new Data.UnitOfWork(options: options);
            });

            services.AddResponseCompression(opts =>
            {
                opts.MimeTypes = Microsoft.AspNetCore.ResponseCompression.ResponseCompressionDefaults.MimeTypes.Concat(
                    new[] { "application/octet-stream" });
            });

            // If using Kestrel:
            services.Configure<Microsoft.AspNetCore.Server.Kestrel.Core.KestrelServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });

            // If using IIS:
            services.Configure<IISServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });

            var culture = new System.Globalization.CultureInfo(1065);
            System.Globalization.CultureInfo.DefaultThreadCurrentCulture = culture;
            System.Globalization.CultureInfo.DefaultThreadCurrentUICulture = culture;

            services.AddTransient<Data.Repositories.IDapper, Data.Repositories.Dapper>();

            // Configure DI for application services
            services.AddScoped<Services.IAccountService, Services.AccountService>();
            services.AddScoped<Services.IUserService, Services.UserService>();
        }

        public void Configure
            (Microsoft.AspNetCore.Builder.IApplicationBuilder app,
            Microsoft.AspNetCore.Hosting.IWebHostEnvironment env)
        {
            //using (var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            //{
            //    scope.ServiceProvider.GetRequiredService<Data.DatabaseContext>().Database.Migrate();
            //}

            app.UseResponseCompression();

            //app.UseHttpsRedirection();

            app.UseRouting();

            // Custom JWT auth middleware
            app.UseMiddleware<Infrastructure.Middlewares.JwtMiddleware>();

            app.UseStaticFiles();

            // Swagger middleware
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            });

            app.UseCors(policyName: AdminCorsPolicy);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
