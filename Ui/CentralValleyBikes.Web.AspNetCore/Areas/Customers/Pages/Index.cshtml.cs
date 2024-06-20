using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using CentralValleyBikes.Api.Code;
using CentralValleyBikes.Web.Services;
using CustomerModel = CentralValleyBikes.Api.Models.Customer;
using System.Collections.Generic;

namespace CentralValleyBikes.Web.Areas.Customers 
{
    public class IndexModel : PageModel
    {
        private ICustomerService _customerService;
        private readonly int _defaultPageSize = 20;

        public PaginationResult<CustomerModel> PaginationResult;
        public List<CustomerModel> Customers { get; set; }

        [ViewData]
        public string SortDirection { get; set; }
        [BindProperty]
        public string FilterType { get; set; }
        [BindProperty]
        public string CurrentFilter { get; set; }

        public IndexModel(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public async Task OnGetAsync(string sortColumn, string sortOrder, string filterType, string currentFilter, string searchString, int? pageNumber, int? pageSize)
        {
            var filter = !string.IsNullOrEmpty(filterType) && !string.IsNullOrEmpty(searchString) ? $"{filterType}={searchString}" : "";
            string responseData = await _customerService.GetAsync(filter, pageNumber, pageSize.HasValue ? pageSize.Value : _defaultPageSize);

            PaginationResult = JsonConvert.DeserializeObject<PaginationResult<CustomerModel>>(responseData);
            Customers = PaginationResult.Data;

            FilterType = filterType;
            CurrentFilter = searchString;
        }
    }
}
