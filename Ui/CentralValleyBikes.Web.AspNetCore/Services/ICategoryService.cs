using System.Threading.Tasks;

namespace CentralValleyBikes.Web.Services
{
    public interface ICategoryService
    {
        Task<string> GetCategoriesAsync(int? id = null);
    }
}
