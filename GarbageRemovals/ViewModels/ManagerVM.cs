using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GarbageRemovals.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GarbageRemovals.ViewModels
{
    public class ManagerVM
    {
        public Manager Manager { get; set; }
        public List<Manager> Managers { get; set; }
        public List<SelectListItem> AreaList { get; set; }
    }
}
