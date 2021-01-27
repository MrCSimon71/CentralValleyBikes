using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BikeStoreWebAppDotNetCore.Data;
using BikeStoreWebAppDotNetCore.Models;
using ProductsModel = BikeStoreWebAppDotNetCore.Models.Product;

namespace BikeStoreWebAppDotNetCore.Areas.Products.Pages
{
    public class DetailsModel : PageModel
    {
        private readonly BikeStoreWebAppDotNetCore.Data.BikeStoresContext _context;

        public DetailsModel(BikeStoreWebAppDotNetCore.Data.BikeStoresContext context)
        {
            _context = context;
        }

        public ProductsModel Product { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Product = await _context.Products.FirstOrDefaultAsync(m => m.ProductId == id);

            if (Product == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
