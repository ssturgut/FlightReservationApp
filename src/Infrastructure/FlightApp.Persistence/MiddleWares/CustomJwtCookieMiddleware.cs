using System;
using FlightApp.Application.Interfaces;
using Microsoft.AspNetCore.Http;

namespace FlightApp.Persistence.MiddleWares
{
    public class CustomJwtCookieMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ICookieService _cookieService;

        public CustomJwtCookieMiddleware(RequestDelegate next, ICookieService cookieService)
        {
            _next = next;
            _cookieService = cookieService;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Cookies.ContainsKey("jwt"))
            {
                var token = _cookieService.GetCookie(context.Request, "jwt");
                if (!string.IsNullOrEmpty(token) && !context.Request.Headers.ContainsKey("Authorization"))
                {
                    context.Request.Headers.Add("Authorization", $"Bearer {token}");
                }
            }

            await _next(context);
        }
    }
}


