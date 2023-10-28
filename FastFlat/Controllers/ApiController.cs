using FastFlat.DAL;
using FastFlat.Models;
using FastFlat.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Text.Json.Serialization;

namespace FastFlat.Controllers
{
    // This controller handles operations related to exploring listings on the platform.
    public class ApiController : Controller
    {

        private readonly HttpClient _httpClient;

        private readonly ILogger<ExplorerController> _logger;

        public ApiController(IHttpClientFactory httpClientFactory, ILogger<ExplorerController> logger)
        {
            _httpClient = httpClientFactory.CreateClient();
            _logger = logger;
        }

        [HttpGet("api/address-search")]
        public async Task<IActionResult> AutocompleteAddress([FromQuery] string input)
        {
            var apiKey = "AIzaSyBHz8GEbiV4GVf1ZLD1FBMOlFhYSOOp3oI";
            var requestUri = $"https://maps.googleapis.com/maps/api/place/autocomplete/json?input={input}&types=address&key={apiKey}";

            var response = await _httpClient.GetAsync(requestUri);

            if (!response.IsSuccessStatusCode)
            {
                return BadRequest("Failed to get suggestions from Google Places.");
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var data = JObject.Parse(jsonResponse);
            return Ok(data);
        }

        [HttpGet("api/locality-search")]
        public async Task<IActionResult> Autocomplete([FromQuery] string input)
        {
            var apiKey = "AIzaSyBHz8GEbiV4GVf1ZLD1FBMOlFhYSOOp3oI"; // Reminder: You should not expose your API key in public forums or repositories.
            var requestUri = $"https://maps.googleapis.com/maps/api/place/autocomplete/json?input={input}&types={System.Net.WebUtility.UrlEncode("locality|country")}&key={apiKey}";

            var response = await _httpClient.GetAsync(requestUri);

            if (!response.IsSuccessStatusCode)
            {
                return BadRequest("Failed to get suggestions from Google Places.");
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var data = JObject.Parse(jsonResponse);
            return Ok(data);
        }



        [HttpGet("api/place-details")]
        public async Task<IActionResult> PlaceDetails(string placeId)
        {
            var apiKey = "AIzaSyBHz8GEbiV4GVf1ZLD1FBMOlFhYSOOp3oI";
            var requestUri = $"https://maps.googleapis.com/maps/api/place/details/json?place_id={placeId}&key={apiKey}";

            var response = await _httpClient.GetAsync(requestUri);

            if (!response.IsSuccessStatusCode)
            {
                return BadRequest("Failed to get details from Google Places.");
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var data = JObject.Parse(jsonResponse);
            return Ok(data);
        }



    }
}
