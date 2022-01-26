using Microsoft.AspNetCore.Identity;
using System;

namespace Core.Models
{
    public class Role : IdentityRole<Guid>
    {
        public Role() : base() { }

        public Role(string name)
            : base(name)
        { }
    }
}
