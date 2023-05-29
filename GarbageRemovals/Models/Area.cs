using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GarbageRemovals.Models
{
    public class Area
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name="Name")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "City")]
        public string City { get; set; }
        [Required]
        [Display(Name = "Country")]
        public string Country { get; set; }
    }
}
