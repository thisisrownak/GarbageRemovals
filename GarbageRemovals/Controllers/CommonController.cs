using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GarbageRemovals.Data;
using Microsoft.AspNetCore.Mvc;
using GarbageRemovals.Models;
using GarbageRemovals.Common;
using GarbageRemovals.ViewModels;

namespace GarbageRemovals.Controllers
{
    public class CommonController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly ExtensionMethods _extention;
        public CommonController(ApplicationDbContext db)
        {
            this._db = db;
           this._extention=new ExtensionMethods(_db);
        }
        [HttpGet]
        public IActionResult Index()
        {
            string managerCookie = HttpContext.Request.Cookies["manager"];
            if (string.IsNullOrEmpty(managerCookie))
            {
                return RedirectToAction("ManagerLogin", "User");
            }
            return View();
        }
        [HttpGet]
        public IActionResult AddManager(string id)
        {
            string managerCookie = HttpContext.Request.Cookies["manager"];
            if (string.IsNullOrEmpty(managerCookie))
            {
                return RedirectToAction("ManagerLogin", "User");
            }
            Manager manager=new Manager();
            if (!string.IsNullOrEmpty(id))
            { 
                manager = _db.Managers.FirstOrDefault(x => x.Id == Convert.ToInt32(id));
            }
            
            ManagerVM managerVm=new ManagerVM()
            {
                Manager = manager,
                Managers = _db.Managers.ToList(),
                AreaList = _extention.AreaDD()
            };
            return View(managerVm);
        }
        [HttpPost]
        public IActionResult AddManager(Manager manager)
        {
            string managerCookie = HttpContext.Request.Cookies["manager"];
            if (string.IsNullOrEmpty(managerCookie))
            {
                return RedirectToAction("ManagerLogin", "User");
            }
            if (ModelState.IsValid)
            {
                if (manager.Id >0)
                {
                    _db.Update(manager);
                }
                else
                {
                    _db.Add(manager);
                }
               
                _db.SaveChanges();
            }
            ManagerVM managerVm = new ManagerVM()
            {
                Managers = _db.Managers.ToList()
            };
            return View(managerVm);
        }
        public IActionResult ManagerList()
        {
            string managerCookie = HttpContext.Request.Cookies["manager"];
            if (string.IsNullOrEmpty(managerCookie))
            {
                return RedirectToAction("ManagerLogin", "User");
            }

            var managers = _db.Managers.ToList();
            return PartialView(managers);
        }

        public IActionResult ManagerListView()
        {
            var managers = _db.Managers.ToList();
            return View(managers);
        }
        [HttpGet]
        public IActionResult AddArea(string id)
        {
            string ManagerCookie = HttpContext.Request.Cookies["manager"];
            if (string.IsNullOrEmpty(ManagerCookie))
            {
                return RedirectToAction("ManagerLogin", "User");
            }
            Area area = new Area();
            if (!string.IsNullOrEmpty(id))
            {
                area = _db.Areas.FirstOrDefault(x => x.Id == Convert.ToInt32(id));
            }
            AreaVM areaVm = new AreaVM()
            {
                Area = area,
                AreaList = _db.Areas.ToList()
            };
            return View(areaVm);
        }
        [HttpPost]
        public IActionResult AddArea(Area area)
        {
            string ManagerCookie = HttpContext.Request.Cookies["manager"];
            if (string.IsNullOrEmpty(ManagerCookie))
            {
                return RedirectToAction("ManagerLogin", "User");
            }
            if (ModelState.IsValid)
            {
                if (area.Id > 0)
                {
                    _db.Update(area);
                }
                else
                {
                    _db.Areas.Add(area);
                }
                
                _db.SaveChanges();
            }
            AreaVM areaVm = new AreaVM()
            {
                AreaList = _db.Areas.ToList()
            };
            return View(areaVm);
        }
        public IActionResult AreaList()
        {
            string managerCookie = HttpContext.Request.Cookies["manager"];
            if (string.IsNullOrEmpty(managerCookie))
            {
                return RedirectToAction("ManagerLogin", "User");
            }

            var areas = _db.Areas.ToList();
            return PartialView(areas);
        }
        public IActionResult AreaListView()
        {
            string managerCookie = HttpContext.Request.Cookies["manager"];
            if (string.IsNullOrEmpty(managerCookie))
            {
                return RedirectToAction("ManagerLogin", "User");
            }

            var areas = _db.Areas.ToList();
            return View(areas);
        }
        [HttpGet]
        public IActionResult AddDumpStation(string id)
        {
            string ManagerCookie = HttpContext.Request.Cookies["manager"];
            if (string.IsNullOrEmpty(ManagerCookie))
            {
                return RedirectToAction("ManagerLogin", "User");
            }
            DumpStation dumpStation= new DumpStation();
            if (!string.IsNullOrEmpty(id))
            {
                dumpStation = _db.DumpStations.FirstOrDefault(x => x.Id == Convert.ToInt32(id));
            }
            DumpStationVM dumpStationVm = new DumpStationVM()
            {
                DumpStation = dumpStation,
                DumpStationList = _db.DumpStations.ToList(),
                AreDropDowns = _extention.AreaDD()
            };
            return View(dumpStationVm);
        }
        [HttpPost]
        public IActionResult AddDumpStation(DumpStation dumpStation)
        {
            string ManagerCookie = HttpContext.Request.Cookies["manager"];
            if (string.IsNullOrEmpty(ManagerCookie))
            {
                return RedirectToAction("ManagerLogin", "User");
            }
            if (ModelState.IsValid)
            {
                if (dumpStation.Id > 0)
                {
                    _db.Update(dumpStation);
                }
                else
                {
                    _db.DumpStations.Add(dumpStation);
                }
               
                _db.SaveChanges();
            }

            DumpStationVM dumpStationVm = new DumpStationVM()
            {
                DumpStationList = _db.DumpStations.ToList()
            };
            return View(dumpStationVm);
        }
        public IActionResult DumpStationList()
        {
            string managerCookie = HttpContext.Request.Cookies["manager"];
            if (string.IsNullOrEmpty(managerCookie))
            {
                return RedirectToAction("ManagerLogin", "User");
            }

            var dumpStations = _db.DumpStations.ToList();
            return PartialView(dumpStations);
        }
        public IActionResult DumpStationListView()
        {
            string managerCookie = HttpContext.Request.Cookies["manager"];
            if (string.IsNullOrEmpty(managerCookie))
            {
                return RedirectToAction("ManagerLogin", "User");
            }

            var dumpStations = _db.DumpStations.ToList();
            return View(dumpStations);
        }
    }
}