using System;
using System.Collections.Generic;
using System.Text;

namespace CentralValleyBikes.Domain.JWT
{
    public class JWToken
    {
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
