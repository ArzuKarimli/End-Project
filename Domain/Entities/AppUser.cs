

using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; }
        public string Specialization { get; set; }


    }
}
