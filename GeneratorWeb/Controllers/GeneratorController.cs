using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Generator.Interfaces;
using Generator.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace GeneratorWeb.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GeneratorController : ControllerBase
    {

        private readonly ILogger<GeneratorController> _logger;
        private readonly IOptions<RandomGeneratorSelectorSettings> _randomGeneratorSelector;
        private readonly GeneratorResolver _generatorResolver;
        private readonly HttpClient _client;

        public GeneratorController(ILogger<GeneratorController> logger, IOptions<RandomGeneratorSelectorSettings> randomGeneratorSelector,
            GeneratorResolver generatorResolver, HttpClient client)
        {
            _logger = logger;
            _randomGeneratorSelector = randomGeneratorSelector;
            _generatorResolver = generatorResolver;
            _client = client;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<int>>> Get([FromQuery] string op = "add", [FromQuery] string source = "local")
        {
            var generator = _generatorResolver(source.ToString());
            // generator would never be null as we are defaulting to a known source type 'local'. Just a sanity check
            if (generator == null) return BadRequest("Invlaid number generator provided");

            var numbers = await generator.Generate();

            var queryParams = new Dictionary<string, string>()
            {
                { "op", op + "" }
            };

            string url = QueryHelpers.AddQueryString("https://localhost:5005", queryParams);

            try
            {
                var response = await _client.PostAsync(url, JsonContent.Create<int[]>(numbers));

                if (response.IsSuccessStatusCode)
                {
                    var result = await JsonSerializer.DeserializeAsync<int[]>(await response.Content.ReadAsStreamAsync());
                    return result;
                }
            }
            catch (Exception)
            {
                // blanket catching all exceptions. Log it                
            }

            return Array.Empty<int>();
        }
    }
}
