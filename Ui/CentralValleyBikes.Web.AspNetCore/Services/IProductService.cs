using System.Threading.Tasks;

namespace CentralValleyBikes.Web.Services
{
    public interface IProductService
    {
        Task<string> GetProductsAsync(int? id = null);
    }
}
