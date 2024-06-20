using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CentralValleyBikes.Api.Data;
using CentralValleyBikes.Api.Models;
using ProductsModel = CentralValleyBikes.Api.Models.Product;

namespace CentralValleyBikes.Web.Areas.Products.Pages
{
    public class DeleteModel : PageModel
    {
        private readonly CentralValleyBikes.Api.Data.BikeStoresContext _context;

        public DeleteModel(CentralValleyBikes.Api.Data.BikeStoresContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Product = await _context.Products.FindAsync(id);

            if (Product != null)
            {
                _context.Products.Remove(Product);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
