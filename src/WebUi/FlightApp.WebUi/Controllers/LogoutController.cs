using System;
using FlightApp.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FlightApp.WebUi.Controllers
{
	public class LogoutController : Controller
    {
        private readonly ICookieService _cookieService;

        public LogoutController(ICookieService cookieService)
        {
            _cookieService = cookieService;
        }

        [HttpPost]
        public IActionResult Index()
        {
            _cookieService.RemoveCookie(Response, "jwt");
            return RedirectToAction("Index", "Login");
        }
    }
}