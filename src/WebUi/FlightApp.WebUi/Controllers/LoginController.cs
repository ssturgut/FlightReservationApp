
using FlightApp.Application.Dtos;
using FlightApp.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using FlightApp.Application.Interfaces.Repository;

namespace FlightApp.WebUi.Controllers.Account
{
    public class LoginController : Controller
    {
        private readonly IAuthenticationService _authService;
        private readonly IMongoDatabase _database;
        private readonly ITokenService _tokenService;
        private readonly ICookieService _cookieService;

        public LoginController(IAuthenticationService authService, IMongoDatabase database, ITokenService tokenService, ICookieService cookieService)
        {
            _authService = authService;
            _database = database;
            _tokenService = tokenService;
            _cookieService = cookieService;
        }

        [HttpGet]
        public async Task<IActionResult> IndexAsync()
        {
            return View(new LoginViewDto());
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewDto model)
        {
            if (ModelState.IsValid)
            {
                var response = await _authService.LoginAsync(model);
                if (response.IsSuccessful)
                {
                    var cookieOptions = new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = true,
                        SameSite = SameSiteMode.Strict,
                        Expires = DateTime.Now.AddMinutes(30)
                    };
                    _cookieService.SetCookie(Response, "jwt", response.Token, cookieOptions);

                    return RedirectToAction("Index", "Booking");
                }

                ModelState.AddModelError(string.Empty, response.ErrorMessage ?? "An unexpected error occurred.");
            }

            return View("Index", model);
        }
    }
}




