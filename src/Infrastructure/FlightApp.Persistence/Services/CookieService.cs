using FlightApp.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using System;

namespace FlightApp.Persistence.Services
{
    public class CookieService : ICookieService
    {
        public void SetCookie(HttpResponse response, string key, string value, CookieOptions options)
        {
            var option = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.Now.AddMinutes(30)

        };


            response.Cookies.Append(key, value, option);
        }

        public string GetCookie(HttpRequest request, string key)
        {
            return request.Cookies[key];
        }

        public void RemoveCookie(HttpResponse response, string key)
        {
            response.Cookies.Delete(key);
        }
    }
}
