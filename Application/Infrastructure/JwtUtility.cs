﻿using System.Linq;

//namespace Application.Infrastructure

namespace Infrastructure
{
	public static class JwtUtility
	{
		static JwtUtility()
		{
		}

		public static string GenerateJwtToken(Models.User user, string secretKey)
		{
			// Generate token that is valid for 8 hours

			var key =
				System.Text.Encoding.ASCII.GetBytes(secretKey);

			var symmetricSecurityKey =
				new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(key: key);

			var securityAlgorithm =
				Microsoft.IdentityModel.Tokens.SecurityAlgorithms.HmacSha256Signature;

			var signingCredentials =
				new Microsoft.IdentityModel.Tokens
				.SigningCredentials(key: symmetricSecurityKey, algorithm: securityAlgorithm);

			var tokenDescriptor =
				new Microsoft.IdentityModel.Tokens.SecurityTokenDescriptor
				{
					Subject =
						new System.Security.Claims.ClaimsIdentity
						(new[]
						{
							new System.Security.Claims.Claim
								(type: "FullName", value: user.FullName),

							new System.Security.Claims.Claim
								(type: System.Security.Claims.ClaimTypes.Name, value: user.Username),

							new System.Security.Claims.Claim
								(type: System.Security.Claims.ClaimTypes.NameIdentifier, value: user.Id.ToString()),
						}),

					Expires =
						System.DateTime.UtcNow.AddHours(8),

					SigningCredentials = signingCredentials,
				};

			var tokenHandler =
				new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();

			var token =
				tokenHandler.CreateToken(tokenDescriptor: tokenDescriptor);

			string tokenString =
				tokenHandler.WriteToken(token: token);

			return tokenString;
		}

		public static async System.Threading.Tasks.Task AttachUserToContext
			(Microsoft.AspNetCore.Http.HttpContext context,
			Application.Services.IUserService userService, string token, string secretKey)
		{
			try
			{
				var tokenHandler =
					new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();

				var key =
					System.Text.Encoding.ASCII.GetBytes(secretKey);

				tokenHandler.ValidateToken(token: token,
					validationParameters: new Microsoft.IdentityModel.Tokens.TokenValidationParameters
					{
						ValidateIssuer = false,
						ValidateAudience = false,
						ValidateIssuerSigningKey = true,

						IssuerSigningKey =
							new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(key),

						// Set clockskew to zero so tokens expire
						// exactly at token expiration time (instead of 5 minutes later)
						ClockSkew =
							System.TimeSpan.Zero
					}, out Microsoft.IdentityModel.Tokens.SecurityToken validatedToken);

				var jwtToken =
					validatedToken as System.IdentityModel.Tokens.Jwt.JwtSecurityToken;

				System.Security.Claims.Claim userIdClaim =
					jwtToken.Claims
					.Where(current => current.Type.ToLower() == "NameId".ToLower())
					.FirstOrDefault();

				// دقت کنید که دستور ذیل کار نمی‌کند
				//.Where(current => current.Type == System.Security.Claims.ClaimTypes.NameIdentifier)

				if (userIdClaim == null)
				{
					return;
				}

				var userId =
					System.Guid.Parse(userIdClaim.Value);

				var foundedUser =
					await userService.GetByIdAsync(userId);

				if (foundedUser == null)
				{
					return;
				}

				// Attach user to context on successful jwt validation
				context.Items["User"] = foundedUser.ValueOrDefault;
			}
			catch // (System.Exception ex)
			{
				// Log ex
				//string errorMessage = ex.Message;

				// Do nothing if jwt validation fails
				// user is not attached to context so request won't have access to secure routes
			}
		}
	}
}
