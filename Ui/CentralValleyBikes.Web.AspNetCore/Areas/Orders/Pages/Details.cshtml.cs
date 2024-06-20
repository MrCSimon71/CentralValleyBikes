using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CentralValleyBikes.Api.Data;
using CentralValleyBikes.Api.Models;
using OrdersModel = CentralValleyBikes.Api.Models.Order;

namespace CentralValleyBikes.Web.Areas.Orders.Pages
{
    public class DetailsModel : PageModel
    {
        private readonly CentralValleyBikes.Api.Data.BikeStoresContext _context;

        public DetailsModel(CentralValleyBikes.Api.Data.BikeStoresContext context)
        {
            _context = context;
        }

        public OrdersModel Orders { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Orders = await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.Staff)
                .Include(o => o.Store).FirstOrDefaultAsync(m => m.OrderId == id);

            if (Orders == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
