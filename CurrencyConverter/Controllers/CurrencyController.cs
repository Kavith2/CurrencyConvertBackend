using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace CurrencyConverter.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CurrencyController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private const string ApiKey = "tWTdo1sidkTTmXLMcVKcRiv3Sypq55Eb";

        public CurrencyController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("apikey", ApiKey);
        }

        [HttpGet("symbols")]
        public async Task<IActionResult> GetSymbols()
        {
            var url = "https://api.apilayer.com/exchangerates_data/symbols";
            var response = await _httpClient.GetAsync(url);

            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                return StatusCode((int)response.StatusCode, content);

            return Content(content, "application/json");
        }

        [HttpGet("convert")]
        public async Task<IActionResult> Convert( [FromQuery] string from,[FromQuery] string to,[FromQuery] decimal amount)
        {
            var url = $"https://api.apilayer.com/exchangerates_data/convert?from={from}&to={to}&amount={amount}";
            var response = await _httpClient.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                return StatusCode((int)response.StatusCode, content);

            return Content(content, "application/json");
        }
    }
}
