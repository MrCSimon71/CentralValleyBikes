using System.Net.Http;
using System.Threading.Tasks;

namespace CentralValleyBikes.Web.Services
{
    public class ProductService : BaseService, IProductService
    {
        public ProductService(HttpClient httpClient) : base(httpClient, "products")
        {
        }

        public async Task<string> GetProductsAsync(int? id)
        {
            if (id == null)
            {
                return await base.GetAsync();
            }
            else
            {
                return await base.GetByIdAsync(id.Value);
            }
        }
    }
}
