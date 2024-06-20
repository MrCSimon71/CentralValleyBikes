using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Net.Http;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CentralValleyBikes.Api.Data;
using ProductsModel = CentralValleyBikes.Api.Models.Product;
using CentralValleyBikes.Web.Services;

namespace CentralValleyBikes.Web.Areas.Products.Pages
{
    public class IndexModel : PageModel
    {
        private IProductService _productService;
        public IList<ProductsModel> Products { get; set; }

        public IndexModel(IProductService productService)
        {
            _productService = productService;
        }

        public async Task OnGetAsync()
        {
            string responseData = await _productService.GetProductsAsync();
            Products = JsonConvert.DeserializeObject<List<ProductsModel>>(responseData);
        }
    }
}
