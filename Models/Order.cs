﻿using System;
using System.Collections.Generic;

namespace BikeStoreWebAppDotNetCore.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderItems = new HashSet<OrderItems>();
        }

        public int OrderId { get; set; }
        public int? CustomerId { get; set; }
        public byte OrderStatus { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime RequiredDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public int StoreId { get; set; }
        public int StaffId { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Staffs Staff { get; set; }
        public virtual Stores Store { get; set; }
        public virtual ICollection<OrderItems> OrderItems { get; set; }
    }
}
