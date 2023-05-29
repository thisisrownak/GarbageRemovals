using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GarbageRemovals.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GarbageRemovals.ViewModels
{
    public class PickRequestVM
    {
        public Request Request { get; set; }
        public List<Request> RequestList { get; set; }
        public List<SelectListItem>AreaListDropDown { get; set; }
    }
}
