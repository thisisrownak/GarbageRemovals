using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GarbageRemovals.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GarbageRemovals.ViewModels
{
    public class DumpStationVM
    {
        public DumpStation DumpStation { get; set; }
        public List<DumpStation> DumpStationList { get; set; }
        public List<SelectListItem> AreDropDowns { get; set; }
    }
}
