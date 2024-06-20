using CentralValleyBikes.Api.Pages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentralValleyBikes.Api.Code
{
    public class PaginationResult<T>
    {
        public int Count { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int TotalRecords { get; set; }
        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; } = new List<string>();

        [JsonProperty(PropertyName = "_links")]
        public PagingLinks Links { get; set; }

        public List<T> Data { get; set; }

        public bool HasPreviousPage => CurrentPage > 1;

        public bool HasNextPage => CurrentPage < TotalPages;

        public PaginationResult() { }
    }

    public class PagingLinks
    {
        public string Self { get; set; }
        public string First { get; set; }
        public string Previous { get; set; }
        public string Next { get; set; }
        public string Last { get; set; }
    }
}
