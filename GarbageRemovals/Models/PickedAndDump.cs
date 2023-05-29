using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GarbageRemovals.Models
{
    public class PickedAndDump
    {
        [Key]
        public int Id { get; set; }
        public decimal Amount { get; set; }
        [Required]
        [Display(Name ="Select Request")]
        public int RequestId { get; set; }
        [ForeignKey("RequestId")]
        public Request Request { get; set; }
        public int ManagerId { get; set; }
        [ForeignKey("ManagerId")]
        public Manager Manager { get; set; }

    }
}
