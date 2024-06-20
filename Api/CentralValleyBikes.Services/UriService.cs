using CentralValleyBikes.Domain.Services;
using Microsoft.AspNetCore.WebUtilities;
using CentralValleyBikes.Domain.Filters;

namespace CentralValleyBikes.Services
{
    public class UriService : IUriService
    {
        private readonly string _baseUri;

        public UriService(string baseUri)
        {
            _baseUri = baseUri;
        }

        public Uri GetPageUri(SearchFilter filter, string route, List<SearchCriteria> searchParams)
        {
            var _enpointUri = new Uri(string.Concat(_baseUri, route));
            var modifiedUri = QueryHelpers.AddQueryString(_enpointUri.ToString(), "pageNumber", filter.PageNumber.ToString());
            modifiedUri = QueryHelpers.AddQueryString(modifiedUri, "pageSize", filter.PageSize.ToString());

            //if (searchParams.Count > 0)
            //{
            //    foreach (var param in searchParams)
            //    {
            //        modifiedUri = QueryHelpers.AddQueryString(modifiedUri, param.Key.ToLower(), param.Value);
            //    }
            //}

            return new Uri(modifiedUri);
        }
    }
}
