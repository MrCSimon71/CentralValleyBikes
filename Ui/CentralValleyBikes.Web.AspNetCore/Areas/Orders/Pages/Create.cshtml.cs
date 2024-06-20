using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using CentralValleyBikes.Api.Data;
using CentralValleyBikes.Api.Models;
using OrdersModel = CentralValleyBikes.Api.Models.Order;

namespace CentralValleyBikes.Web.Areas.Orders.Pages
{
    public class CreateModel : PageModel
    {
        private readonly CentralValleyBikes.Api.Data.BikeStoresContext _context;

        public CreateModel(CentralValleyBikes.Api.Data.BikeStoresContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "Email");
        ViewData["StaffId"] = new SelectList(_context.Staffs, "StaffId", "Email");
        ViewData["StoreId"] = new SelectList(_context.Stores, "StoreId", "StoreName");
            return Page();
        }

        [BindProperty]
        public OrdersModel Orders { get; set; }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Orders.Add(Orders);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
