using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FastShare.Core.Api
{
    internal class FastShareApi
    {
        private HttpClient _httpClient = new HttpClient();

#if DEBUG
        private readonly string url = "http://localhost:8080";
#else
        private readonly string url = "https://vicart.dev:8090";
#endif

        public FastShareApi()
        {
            _httpClient.BaseAddress = new Uri(url);
        }

        public async Task<int> GetCode()
        {
            var response = int.Parse(await _httpClient.GetStringAsync("/api/code"));
            return response;
        }

        public async Task<string> GetAddress(int code)
        {
            var response = await _httpClient.GetStringAsync("/api/address?code=" + code);
            return response;
        }
    }
}
