using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GarbageRemovals.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GarbageRemovals.ViewModels
{
    public class PickAndDumpVM
    {
        public PickedAndDump PickedAndDump { get; set; }
        public List<PickedAndDump> PickedAndDumpList { get; set; }
        public List<SelectListItem> RequestList{ get; set; }
    }
}
