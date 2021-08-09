using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Generator.Interfaces;
using Microsoft.AspNetCore.WebUtilities;

namespace Generator.Implementations
{
    public class RandomGeneratorViaAPI : IGenerator
    {
        private readonly HttpClient _client;
        private readonly RandomGeneratorViaAPISettings _options;

        public static string Name => "api";

        public static string Shorthand => "a";

        public RandomGeneratorViaAPI(HttpClient client, RandomGeneratorViaAPISettings options)
        {
            _options = options;
            _client = client;
        }

        /// <summary>
        /// Generates random numbers between 0 and 1000. The upper limit is kept low to
        /// avoid Arithmatic overlfows.
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="howMany"></param>
        /// <returns></returns>
        public async Task<int[]> Generate(int min = 0, int max = 1000, int howMany = 2)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "");

            var queryParams = new Dictionary<string, string>() 
            {
                { "min", min + "" },
                { "max", max + "" },
                { "count", howMany + ""}
            };

            string url = QueryHelpers.AddQueryString(_options.URL, queryParams);

            _client.BaseAddress = new Uri(url);

            try {
                var response = await _client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var result = await JsonSerializer.DeserializeAsync<int[]>(await response.Content.ReadAsStreamAsync());
                    return result;
                }
            }
            catch(Exception)
            {
                // blanket catching all exceptions. Log it                
            }            

            return new int[0];
        }
    }
}