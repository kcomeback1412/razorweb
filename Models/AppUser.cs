using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace CS58_Razor09EF.Models
{
    public class AppUser : IdentityUser
    {
        [StringLength(400)]
        public string? HomeAddress { get; set; } 
    }
}
