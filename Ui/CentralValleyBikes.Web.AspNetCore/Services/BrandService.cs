using Microsoft.AspNetCore.Http;
using System.Diagnostics.Eventing.Reader;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CentralValleyBikes.Web.Services
{
    public class BrandService : BaseService, IBrandService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BrandService(IHttpContextAccessor httpContextAccessor, HttpClient httpClient) : base(httpClient, "brands")
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<string> GetBrandsAsync(int? id = null)
        {
            if (id == null)
            {
                if (!_httpContextAccessor.HttpContext.Session.TryGetValue("Brands", out var brandsJsonByteData))
                {
                    var brandsJsonData = await base.GetAsync();
                    _httpContextAccessor.HttpContext.Session.SetString("Brands", brandsJsonData);
                    return brandsJsonData;
                }
                else
                {
                    return Encoding.UTF8.GetString(brandsJsonByteData, 0, brandsJsonByteData.Length);
                }
            }
            else
            {
                return await base.GetByIdAsync(id.Value);
            }
        }
    }
}
