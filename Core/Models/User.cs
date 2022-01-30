using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.AspNetCore.Identity;

namespace Core.Models
{
    public class User : IdentityUser<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<Booking> Booking { get; set; }
        [DefaultValue(false)]
        public bool IsDeleted  { get; set; }
    }
}
