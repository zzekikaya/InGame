using Microsoft.AspNetCore.Identity;
using System;

namespace InGame.Core.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public DateTime Birthdate { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}
