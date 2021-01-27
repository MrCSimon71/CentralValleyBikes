using System;
using System.Collections.Generic;

namespace BikeStoreWebAppDotNetCore.Models
{
    public partial class SalesSummary
    {
        public string Brand { get; set; }
        public string Category { get; set; }
        public short ModelYear { get; set; }
        public decimal? Sales { get; set; }
    }
}
