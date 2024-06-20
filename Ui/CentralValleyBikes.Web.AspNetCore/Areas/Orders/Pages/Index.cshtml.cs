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
    public class IndexModel : PageModel
    {
        private readonly CentralValleyBikes.Api.Data.BikeStoresContext _context;

        public IndexModel(CentralValleyBikes.Api.Data.BikeStoresContext context)
        {
            _context = context;
        }

        public IList<OrdersModel> Orders { get;set; }

        public async Task OnGetAsync()
        {
            Orders = await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.Staff)
                .Include(o => o.Store).ToListAsync();
        }
    }
}
