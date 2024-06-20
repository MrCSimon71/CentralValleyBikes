using Microsoft.AspNetCore.Http;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CentralValleyBikes.Web.Services
{
    public class CategoryService : BaseService, ICategoryService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CategoryService(IHttpContextAccessor httpContextAccessor, HttpClient httpClient) : base(httpClient, "categories")
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<string> GetCategoriesAsync(int? id = null)
        {
            if (id == null)
            {
                if (!_httpContextAccessor.HttpContext.Session.TryGetValue("Categories", out var categoriesJsonByteData))
                {
                    var categoriesJsonData = await base.GetAsync();
                    _httpContextAccessor.HttpContext.Session.SetString("Categories", categoriesJsonData);
                    return categoriesJsonData;
                }
                else
                {
                    return Encoding.UTF8.GetString(categoriesJsonByteData, 0, categoriesJsonByteData.Length);
                }
            }
            else
            {
                return await base.GetByIdAsync(id.Value);
            }
        }
    }
}
