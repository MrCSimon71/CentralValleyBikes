using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentralValleyBikes.Api.DTOs
{
    public class LogOutDto
    {
        public Guid Id { get; set; }
        public string EmailAddress { get; set; }
    }
}
