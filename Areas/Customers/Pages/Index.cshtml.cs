using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BikeStoreWebAppDotNetCore.Data;
using BikeStoreWebAppDotNetCore.Models;
using CustomerModel = BikeStoreWebAppDotNetCore.Models.Customer;
using BikeStoreWebAppDotNetCore.Code;

namespace BikeStoreWebAppDotNetCore.Areas.Customers 
{
    public class IndexModel : PageModel
    {
        private readonly BikeStoreWebAppDotNetCore.Data.BikeStoresContext _context;

        [ViewData]
        public string SortDirection { get; set; }
        public string CurrentFilter { get; set; }

        public IndexModel(BikeStoreWebAppDotNetCore.Data.BikeStoresContext context)
        {
            _context = context;
        }

        public PaginatedList<CustomerModel> Customers { get;set; }

        public async Task OnGetAsync(string sortColumn, string sortOrder, string currentFilter, string searchString, int? pageIndex)
        {
            IQueryable<CustomerModel> customers = from s in _context.Customers
                                                  select s;

            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            CurrentFilter = searchString;

            if (!String.IsNullOrEmpty(searchString))
            {
                customers = customers.Where(s => s.LastName.Contains(searchString)
                                       || s.FirstName.Contains(searchString));
            }

            if (sortColumn != null)
            {
                if (String.IsNullOrEmpty(sortOrder) || sortOrder == "ASC")
                {
                    customers = customers.OrderBy(p => EF.Property<object>(p, sortColumn));
                    SortDirection = "DESC";
                }
                else
                {
                    customers = customers.OrderByDescending(p => EF.Property<object>(p, sortColumn));
                    SortDirection = "ASC";
                }                
            }

            Customers = await PaginatedList<CustomerModel>.CreateAsync(
                    customers.AsNoTracking().Include(o => o.Orders)
                        .ThenInclude(i => i.OrderItems)
                            .ThenInclude(p => p.Product), pageIndex ?? 1, 10);
        }
    }
}
