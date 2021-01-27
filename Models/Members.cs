using System;
using System.Collections.Generic;

namespace BikeStoreWebAppDotNetCore.Models
{
    public partial class Members
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ProjectId { get; set; }

        public virtual Projects Project { get; set; }
    }
}
