using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace CentralValleyBikes.Web.Services
{
    public class BaseService : IBaseService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "https://localhost:7212/";
        private string _controller = string.Empty;

        public BaseService(HttpClient httpClient, string controller)
        {
            _httpClient = httpClient;
            _controller = controller;
        }

        public async Task<string> GetAsync()
        {
            string content = string.Empty;

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(_baseUrl + _controller))
                {
                    content = await response.Content.ReadAsStringAsync();
                }
            }

            return content;
        }

        public async Task<string> GetAsync(string? filter, int? pageNumber, int? pageSize)
        {
            var url = _baseUrl + _controller;
            string content = string.Empty;

            url += pageNumber.HasValue ? $"?pageNumber={pageNumber.Value}" : "?pageNumber=1";
            url += pageSize.HasValue ? $"&pageSize={pageSize.Value}" : "";
            url += !string.IsNullOrEmpty(filter) ? $"&{filter}" : "";

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(url))
                {
                    content = await response.Content.ReadAsStringAsync();
                }
            }

            return content;
        }

        public async Task<string> GetByIdAsync(int id)
        {
            string content = string.Empty;

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(_baseUrl + _controller + '\\' + id))
                {
                    content = await response.Content.ReadAsStringAsync();
                }
            }

            return content;
        }

        public async Task<string> SaveAsync(string data)
        {
            string content = string.Empty;

            using (var httpClient = new HttpClient())
            {
                var httpContent = new StringContent(data);
                
                httpContent.Headers.Add("Content-Type", "application/json");

                using (var response = await httpClient.PostAsync(_baseUrl + _controller, httpContent))
                {
                    content = await response.Content.ReadAsStringAsync();
                }
            }

            return content;
        }
    }
}
