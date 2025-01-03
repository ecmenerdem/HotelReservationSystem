﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.Tasks;
using HotelReservation.Application.Common;
using HotelReservation.Domain.Exceptions;

namespace HotelReservation.WebAPI.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class ApiAuthorizationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;
        private readonly IOptionsMonitor<JWTExceptURLList> _jwtExcepURLList;

        public ApiAuthorizationMiddleware(RequestDelegate next, IConfiguration configuration,
            IOptionsMonitor<JWTExceptURLList> jwtExcepUrlList)
        {
            _next = next;
            _configuration = configuration;
            _jwtExcepURLList = jwtExcepUrlList;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            if (!_jwtExcepURLList.CurrentValue.URLList.Contains(httpContext.Request.Path))
            {
                var jwtHandler = new JwtSecurityTokenHandler();
                string authHeader = httpContext.Request.Headers["Authorization"];

                if (authHeader != null)
                {
                    var token = authHeader.Replace("Bearer ", "");
                    var key = Encoding.UTF8.GetBytes(_configuration.GetValue<string>("AppSettings:JWTKey") ??
                                                     string.Empty);

                    jwtHandler.ValidateToken(token, new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    }, out SecurityToken validatedToken);

                    var jwtToken = (JwtSecurityToken)validatedToken;


                    if (jwtToken.ValidTo < DateTime.Now)
                    {
                        throw new TokenInvalidException();
                    }
                }

                else
                {
                    throw new TokenNotFoundException();
                }
            }

            await _next(httpContext);
        }

        // Extension method used to add the middleware to the HTTP request pipeline.
    }

    public static class ApiAuthorizationMiddlewareExtensions
    {
        public static IApplicationBuilder UseApiAuthorizationMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ApiAuthorizationMiddleware>();
        }
    }
}