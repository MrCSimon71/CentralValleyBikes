﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentralValleyBikes.Api.DTOs
{
    public class LoginDto
    {
        public string EmailAddress { get; set; }
        public string Password { get; set; }
    }
}
