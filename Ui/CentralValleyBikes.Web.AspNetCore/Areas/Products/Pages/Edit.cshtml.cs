using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CentralValleyBikes.Api.Data;
using CentralValleyBikes.Api.Models;
using ProductsModel = CentralValleyBikes.Api.Models.Product;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using CentralValleyBikes.Web.Services;

namespace CentralValleyBikes.Web.Areas.Products.Pages
{
    public class EditModel : PageModel
    {
        private IProductService _productService;
        private IBrandService _brandService;
        private ICategoryService _categoryService;

        [BindProperty]
        public ProductsModel Product { get; set; }
        public List<Brand> Brands { get; set; }
        public List<Category> Categories { get; set; }

        public EditModel(IProductService productService, IBrandService brandService, ICategoryService categoryService)
        {
            _productService = productService;
            _brandService = brandService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Task[] tasks = new Task[3];
            tasks[0] = _productService.GetProductsAsync(id);
            tasks[1] = _brandService.GetBrandsAsync();
            tasks[2] = _categoryService.GetCategoriesAsync();

            await Task.WhenAll(tasks);

            Product = JsonConvert.DeserializeObject<ProductsModel>(((Task<string>)tasks[0]).Result);
            Brands = JsonConvert.DeserializeObject<List<Brand>>(((Task<string>)tasks[1]).Result);
            Categories = JsonConvert.DeserializeObject<List<Category>>(((Task<string>)tasks[2]).Result);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                ProductsModel receivedProduct = new ProductsModel();

                using (var httpClient = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(Product), Encoding.UTF8, "application/json");

                    using (var response = await httpClient.PutAsync("https://localhost:7212/products/" + Product.ProductId, content))
                    {
                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                            receivedProduct = JsonConvert.DeserializeObject<ProductsModel>(apiResponse);
                        }
                        else
                        {
                            return Page();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return RedirectToPage("./Index");
        }
    }
}
