using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using FlightApp.Application.Settings;
using FlightApp.Domain.Entities;

namespace FlightApp.WebUi.Controllers
{
    public class ProductController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly ApiSettings _apiSettings;

        public ProductController(HttpClient httpClient, IOptions<ApiSettings> apiSettings)
        {
            _httpClient = httpClient;
            _apiSettings = apiSettings.Value;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _httpClient.GetFromJsonAsync<List<Product>>($"{_apiSettings.BaseUrl}Product");
            return View(products);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            var response = await _httpClient.PostAsJsonAsync($"{_apiSettings.BaseUrl}Product", product);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View(product);
        }
    }
}