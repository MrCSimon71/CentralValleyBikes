﻿using System;
using System.Collections.Generic;

namespace BikeStoreWebAppDotNetCore.Models
{
    public partial class Stocks
    {
        public int StoreId { get; set; }
        public int ProductId { get; set; }
        public int? Quantity { get; set; }

        public virtual Product Product { get; set; }
        public virtual Stores Store { get; set; }
    }
}
