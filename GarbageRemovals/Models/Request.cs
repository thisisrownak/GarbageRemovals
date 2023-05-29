using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using GarbageRemovals.Models;
using Microsoft.AspNetCore.Mvc;

namespace GarbageRemovals.Models
{
    public class Request
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Title")]
        public string Title { get; set; }
        [Required]
        [Display(Name ="Detail")]
        public string AsumedGrabange{get;set;}

        public string ImagePath { get; set; }
        [NotMapped]
        [Display(Name ="Select Image")]
        [BindProperty]
        public IFormFile Image { get; set; }
        [Required]
        [Display(Name ="Select Area")]
        public int AreaId { get; set; }
        [ForeignKey("AreaId")]
        public Area Area { get; set; }
        public bool Status { get; set; }
        public string ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        public ApplicationUser ApplicationUser { get; set; }
    }
}
