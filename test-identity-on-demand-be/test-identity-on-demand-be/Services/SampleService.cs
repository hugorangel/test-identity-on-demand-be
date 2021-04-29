using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using test_identity_on_demand_be.Interfaces;
using test_identity_on_demand_be.Model;

namespace test_identity_on_demand_be.Services
{
    public class SampleService : ISampleService
    {
        private IHttpClientFactory _httpClientFactory;

        // this should be in a configuration file / environment
        private const string _baseTokenUrl = "https://api.signicat.io/";
        private const string _baseSessionUrl = "https://api.idfy.io/";

        public SampleService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<TokenDto> GetBearerTokenAsync()
        {
            var fullUrl = _baseTokenUrl + "oauth/connect/token";
            var request = new HttpRequestMessage(HttpMethod.Post, fullUrl);

            // Have a stringLiterals singleton for all the strings
            request.Headers.Add("grant_type", "client_credentials");
            request.Headers.Add("scope", "identify");
            request.Headers.Add("Authorization", "Basic **PLEASE EPLACE WITH BASE64 STRING***");

            request.Content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                {"grant_type", "client_credentials" },
                {"scope", "identify" }
            });


            var client = _httpClientFactory.CreateClient();

            var response = await client.SendAsync(request);


            if (response.IsSuccessStatusCode)
            {
                var rawObject = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TokenDto>(rawObject);
            }
            else
            {
                // this should be handled better
                throw new Exception(response.RequestMessage.ToString());
            } 

        }

        public async Task<SessionDto> GetSessionInfoAsync(SessionDc sessionData, string Authorization)
        {
            var fullUrl = _baseSessionUrl + "identification/v2/sessions";

            var json = JsonConvert.SerializeObject(sessionData, Formatting.Indented);
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

            var request = new HttpRequestMessage(HttpMethod.Post, fullUrl)
            {
                Content = stringContent
            };

            request.Headers.Add("Authorization", Authorization);

            var client = _httpClientFactory.CreateClient();

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var rawObject = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<SessionDto>(rawObject);
            }
            else
            {
                // this should be handled better
                throw new Exception(response.RequestMessage.ToString());
            }


        }

        public async Task<SessionInfoDto> GetUserInfoAsync(string sessionId, string Authorization)
        {
            var fullUrl = _baseTokenUrl + "identification/v2/sessions/" + sessionId;
            var request = new HttpRequestMessage(HttpMethod.Get, fullUrl);

            request.Headers.Add("Authorization", Authorization);

            var client = _httpClientFactory.CreateClient();

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var rawObject = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<SessionInfoDto>(rawObject);
            }
            else
            {
                // this should be handled better
                throw new Exception(response.RequestMessage.ToString());
            }
        }
    }
}
