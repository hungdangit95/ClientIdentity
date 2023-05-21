using Client.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Client.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HomeController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> Privacy()
        {
            var httpClient = _httpClientFactory.CreateClient("APIClient");
            var response = await httpClient.GetAsync("WeatherForecast").ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var companiesString = await response.Content.ReadAsStringAsync();
            var companies = JsonSerializer.Deserialize<List<WeatherForecast>>(companiesString,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return View(companies);
        }
    }
}
