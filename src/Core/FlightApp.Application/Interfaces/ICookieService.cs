using System;
using Microsoft.AspNetCore.Http;

namespace FlightApp.Application.Interfaces
{
    public interface ICookieService
    {
        void SetCookie(HttpResponse response, string key, string value, CookieOptions options);
        string GetCookie(HttpRequest request, string key);
        void RemoveCookie(HttpResponse response, string key);
    }
}

