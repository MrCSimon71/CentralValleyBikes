using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CentralValleyBikes.Api.Data;
using CentralValleyBikes.Api.Models;
using CustomersModel = CentralValleyBikes.Api.Models.Customer;

namespace CentralValleyBikes.Web.Areas.Customers
{
    public class DetailsModel : PageModel
    {
        private readonly CentralValleyBikes.Api.Data.BikeStoresContext _context;

        public DetailsModel(CentralValleyBikes.Api.Data.BikeStoresContext context)
        {
            _context = context;
        }

        public CustomersModel Customers { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Customers = await _context.Customers.FirstOrDefaultAsync(m => m.CustomerId == id);

            if (Customers == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
