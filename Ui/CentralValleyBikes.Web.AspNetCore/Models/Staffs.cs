﻿using System;
using System.Collections.Generic;

namespace CentralValleyBikes.Api.Models
{
    public partial class Staffs
    {
        public Staffs()
        {
            InverseManager = new HashSet<Staffs>();
            Orders = new HashSet<Order>();
        }

        public int StaffId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public byte Active { get; set; }
        public int StoreId { get; set; }
        public int? ManagerId { get; set; }

        public virtual Staffs Manager { get; set; }
        public virtual Stores Store { get; set; }
        public virtual ICollection<Staffs> InverseManager { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
