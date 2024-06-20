using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CentralValleyBikes.Domain.Filters;

namespace CentralValleyBikes.Domain.Services
{
    public interface IUriService
    {
        public Uri GetPageUri(SearchFilter filter, string route, List<SearchCriteria> searchParams);
    }
}
