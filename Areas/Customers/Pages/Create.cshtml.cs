using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BikeStoreWebAppDotNetCore.Data;
using BikeStoreWebAppDotNetCore.Models;
using CustomersModel = BikeStoreWebAppDotNetCore.Models.Customer;

namespace BikeStoreWebAppDotNetCore.Areas.Customers
{
    public class CreateModel : PageModel
    {
        private readonly BikeStoreWebAppDotNetCore.Data.BikeStoresContext _context;

        public CreateModel(BikeStoreWebAppDotNetCore.Data.BikeStoresContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public CustomersModel Customers { get; set; }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Customers.Add(Customers);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
