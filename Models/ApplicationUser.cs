using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace mini_store.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required ]
         public string FirstName { get; set; }


        
           [Required ]
         public string LastName { get; set; }
    }
}