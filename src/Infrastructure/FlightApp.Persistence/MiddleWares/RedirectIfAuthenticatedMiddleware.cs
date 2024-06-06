using System.Threading.Tasks;
using FlightApp.Application.Interfaces;
using Microsoft.AspNetCore.Http;

namespace FlightApp.Persistence.MiddleWares
{
    public class RedirectIfAuthenticatedMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ICookieService _cookieService;

        public RedirectIfAuthenticatedMiddleware(RequestDelegate next, ICookieService cookieService)
        {
            _next = next;
            _cookieService = cookieService;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Path == "/" || context.Request.Path == "/Login" )
            {
                if (context.Request.Cookies.ContainsKey("jwt"))
                {
                    var token = _cookieService.GetCookie(context.Request, "jwt");
                    if (!string.IsNullOrEmpty(token) && !context.Request.Headers.ContainsKey("Authorization"))
                    {
                        context.Request.Headers.Add("Authorization", $"Bearer {token}");
                    }

                    context.Response.Redirect("/Booking");
                    return;
                }
            }
            //else
            //{
            //    // Token yoksa kullanıcıyı login sayfasına yönlendir, ancak login sayfasına yapılan POST isteklerini kontrol et
            //    if (context.Request.Path != "/Login" && context.Request.Path != "/")
            //    {
            //        context.Response.Redirect("/Login");
            //        return;
            //    }
            //}


            await _next(context);
        }
    }
}