using System.Threading.Tasks;

namespace CentralValleyBikes.Web.Services
{
    public interface IBrandService
    {
        Task<string> GetBrandsAsync(int? id = null);
    }
}
