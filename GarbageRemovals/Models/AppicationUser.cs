using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GarbageRemovals.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [Display(Name ="First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Address")]
        public string Address { get; set; }

        [Required]
        [Display(Name = "Phone")]
        public string Phone { get; set; }

        [Required]
        [Display(Name = "Password"),DataType(DataType.Password)]
        public string Password { get; set; }
        public string FullName { get; set; }

        public ApplicationUser()
        {
            FullName = FirstName + " " + LastName;
        }
    }
}