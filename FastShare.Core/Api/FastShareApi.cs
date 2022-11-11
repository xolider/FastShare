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

        public FastShareApi()
        {
            _httpClient.BaseAddress = new Uri("http://localhost:8080/api");
        }

        public async Task<int> GetCode()
        {
            var response = int.Parse(await _httpClient.GetStringAsync("/code"));
            return response;
        }

        public async Task<string> GetAddress(int code)
        {
            var response = await _httpClient.GetStringAsync("/address?code=" + code);
            return response;
        }
    }
}
