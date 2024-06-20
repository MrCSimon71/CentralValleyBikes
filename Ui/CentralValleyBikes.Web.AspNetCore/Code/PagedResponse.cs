using CentralValleyBikes.Api.Pages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentralValleyBikes.Api.Code
{
    public class PagedResponse<T>
    {
        public int Count { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int TotalRecords { get; set; }
        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public List<T> Data { get; set; }
        public List<string> Errors { get; set; } = new List<string>();

        [JsonProperty(PropertyName = "_links")]
        public PagingLinks Links { get; set; }

        public bool HasPreviousPage => CurrentPage > 1;

        public bool HasNextPage => CurrentPage < TotalPages;

        public PagedResponse() {}
    }
}
