using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GarbageRemovals.Models
{
    public class DumpStation
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Enter Name")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Enter Phone")]
        public string Phone { get; set; }
        [Required]
        [Display(Name ="Select Area")]
        public int AreaId { get; set; }
        [ForeignKey("AreaId")]
        public Area Area { get; set; }
    }
}
