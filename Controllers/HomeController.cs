using BusinessNewsApp.Models;
using Microsoft.AspNetCore.Mvc;
using BusinessNewsApp.Models;
using System.Text.Json;

namespace BusinessNewsApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public HomeController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public async Task<IActionResult> Index()
        {
            var articles = new List<Article>();

            try
            {
                var apiKey = _configuration["NewsApi:ApiKey"];
                var baseUrl = _configuration["NewsApi:BaseUrl"];
                var requestUrl = $"{baseUrl}{apiKey}";

                var client = _httpClientFactory.CreateClient();
                // NewsAPI requires a User-Agent header
                client.DefaultRequestHeaders.Add("User-Agent", "NewsApp/1.0");

                var response = await client.GetAsync(requestUrl);
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var newsResponse = JsonSerializer.Deserialize<NewsApiResponse>(json, options);

                if (newsResponse?.Articles != null)
                {
                    articles = newsResponse.Articles;
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"Error retrieving news: {ex.Message}";
            }

            return View(articles);
        }
    }
}