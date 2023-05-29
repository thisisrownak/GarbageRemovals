using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GarbageRemovals.Models
{
    public class Manager
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name="Enter Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Enter Phone")]
        public string Phone { get; set; }

        [Required]
        [Display(Name = "Enter Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [Display(Name = "Enter Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Select Area")]
        public int AreaId { get; set; }
        [ForeignKey("AreaId")]
        public Area Area { get; set; }
    }
}