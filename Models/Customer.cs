﻿using System;
using System.Collections.Generic;

namespace BikeStoreWebAppDotNetCore.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Orders = new HashSet<Order>();
        }

        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string FullName { get { return FirstName + " " + LastName;  } }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
