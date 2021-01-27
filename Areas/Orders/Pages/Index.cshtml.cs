using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BikeStoreWebAppDotNetCore.Data;
using BikeStoreWebAppDotNetCore.Models;
using OrdersModel = BikeStoreWebAppDotNetCore.Models.Order;

namespace BikeStoreWebAppDotNetCore.Areas.Orders.Pages
{
    public class IndexModel : PageModel
    {
        private readonly BikeStoreWebAppDotNetCore.Data.BikeStoresContext _context;

        public IndexModel(BikeStoreWebAppDotNetCore.Data.BikeStoresContext context)
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
