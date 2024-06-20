using System.Threading.Tasks;

namespace CentralValleyBikes.Web.Services
{
    public interface IBaseService
    {
        Task<string> GetAsync();
        Task<string> GetAsync(string? filter, int? pageNumber, int? pageSize);
        Task<string> GetByIdAsync(int id);
        Task<string> SaveAsync(string content);
    }
}
