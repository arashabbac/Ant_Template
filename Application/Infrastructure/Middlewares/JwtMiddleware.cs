﻿using System.Linq;

//namespace Application.Infrastructure.Middlewares

namespace Infrastructure.Middlewares
{
	public class JwtMiddleware : object
	{
		public JwtMiddleware
			(Microsoft.AspNetCore.Http.RequestDelegate next,
			Microsoft.Extensions.Options.IOptions<ApplicationSettings.Main> options) : base()
		{
			Next = next;
			MainSettings = options.Value;
		}

		protected ApplicationSettings.Main MainSettings { get; }

		protected Microsoft.AspNetCore.Http.RequestDelegate Next { get; }

		public async System.Threading.Tasks.Task
			Invoke(Microsoft.AspNetCore.Http.HttpContext context, Application.Services.IUserService userService)
		{
			var requestHeaders =
				context.Request.Headers["Authorization"];

			var token =
				requestHeaders
				.FirstOrDefault()
				?.Split(" ")
				.Last();

			if (token != null)
			{
				await JwtUtility.AttachUserToContext(context: context,
					userService: userService, token: token, secretKey: MainSettings.SecretKey);
			}

			await Next(context);
		}
	}
}
