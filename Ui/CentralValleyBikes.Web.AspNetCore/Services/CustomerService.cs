using CentralValleyBikes.Api.Code;
using CentralValleyBikes.Api.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace CentralValleyBikes.Web.Services
{
    public class CustomerService : BaseService, ICustomerService
    {
        public CustomerService(HttpClient httpClient) : base(httpClient, "customers") { }

        public async Task<string> GetAsync(string? filter, int? pageNumber, int? pageSize)
        {
            return await base.GetAsync(filter, pageNumber, pageSize);
        }

        public async Task<string> GetByIdAsync(int id)
        {
            return await base.GetByIdAsync(id);
        }

        public async Task<string> SaveAsync(Customer customer)
        {
            throw new System.NotImplementedException();
        }
    }
}
