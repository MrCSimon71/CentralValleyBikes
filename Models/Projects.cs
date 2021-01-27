using System;
using System.Collections.Generic;

namespace BikeStoreWebAppDotNetCore.Models
{
    public partial class Projects
    {
        public Projects()
        {
            Members = new HashSet<Members>();
        }

        public int Id { get; set; }
        public string Title { get; set; }

        public virtual ICollection<Members> Members { get; set; }
    }
}
