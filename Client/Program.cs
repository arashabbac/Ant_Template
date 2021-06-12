using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;

namespace Client
{
    public class Program
    {
        public static async System.Threading.Tasks.Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddScoped
                (sp => new System.Net.Http.HttpClient
                {
                    BaseAddress = new System.Uri(builder.Configuration.GetValue<string>("BaseUrl")),
                });

            var host = builder.Build();

            var culture = new System.Globalization.CultureInfo(1065);
            System.Globalization.CultureInfo.DefaultThreadCurrentCulture = culture;
            System.Globalization.CultureInfo.DefaultThreadCurrentUICulture = culture;

            builder.Services.AddSweetAlert2();
            builder.Services.AddAntDesign();

            builder.Services.AddScoped<Services.UserService>();
            builder.Services.AddScoped<Services.TokenService>();
            builder.Services.AddScoped<Services.AccountService>();

            await builder.Build().RunAsync();


        }

    }
}
