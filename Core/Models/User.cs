using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Core.Models
{
    public class User : IdentityUser<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<Booking> Booking { get; set; }
        public Role Role { get; set; }
    }
}
