using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using GarbageRemovals.Data;
using GarbageRemovals.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GarbageRemovals.Common;
using GarbageRemovals.ViewModels;
using System.Drawing;
using System.Globalization;
using Microsoft.EntityFrameworkCore;

namespace GarbageRemovals.Controllers
{
    public class GarbageController : Controller
    {
        private ApplicationDbContext _db;
        private IWebHostEnvironment _environment;
        private ExtensionMethods _extension;
        public GarbageController(ApplicationDbContext dbContext,IWebHostEnvironment webHostEnvironment)
        {
            this._db = dbContext;
            this._environment = webHostEnvironment;
            _extension=new ExtensionMethods(_db);
        }
        public IActionResult Index()
        {
            DashboardVW dashboard=new DashboardVW();
            string userCookie = HttpContext.Request.Cookies["user"];
            string managerCookie = HttpContext.Request.Cookies["manager"];
            if (string.IsNullOrEmpty(userCookie) && string.IsNullOrEmpty(managerCookie))
            {
                return RedirectToAction("Login", "User");
            }

            if (!string.IsNullOrEmpty(userCookie))
            {
                var user = _extension.GetCurrentUser(userCookie);
                var request= _db.Requests.Where(x => x.ApplicationUserId == user.Id).Include(x => x.ApplicationUser).Include(x => x.Area).ToList();
                dashboard.New = request.Where(x => x.Status == false).ToList().Count.ToString();
                dashboard.Done = request.Where(x => x.Status == true).ToList().Count.ToString();
                dashboard.Rate = request.Count > 0
                    ? (Convert.ToDecimal(dashboard.Done) * 100 / request.Count).ToString(CultureInfo.InvariantCulture)
                    : "0";
                dashboard.Total = request.Count.ToString();
            }
            
            return View(dashboard);
        }

        [HttpGet]
        public IActionResult PickRequest(string id)
        {
            string userCookie = HttpContext.Request.Cookies["user"];
            if (string.IsNullOrEmpty(userCookie))
            {
                return RedirectToAction("Login", "User");
            }
            Request request=new Request();
            if (!string.IsNullOrEmpty(id))
            {
                request = _db.Requests.FirstOrDefault(x => x.Id == Convert.ToInt64(id));
            }
            var user = _extension.GetCurrentUser(userCookie);
            var requestList = _db.Requests.Where(x => x.ApplicationUserId == user.Id).Include(x => x.ApplicationUser).Include(x => x.Area).ToList();

            PickRequestVM pickRequestVm = new PickRequestVM()
            {
                Request = request,
                RequestList = requestList,
                AreaListDropDown = _extension.AreaDD()
            };
            return View(pickRequestVm);
        }
        [HttpPost]
        public IActionResult PickRequest(Request request)
        {
            string userCookie = HttpContext.Request.Cookies["user"];
            if (string.IsNullOrEmpty(userCookie))
            {
                return RedirectToAction("Login", "User");
            }
            if (ModelState.IsValid)
            {
                string cookie = HttpContext.Request.Cookies["user"];
                ApplicationUser user = new ApplicationUser();
                user = _extension.GetCurrentUser(cookie);
                request.ApplicationUserId = user.Id;
                if (request.Image != null)
                {
                    request.ImagePath = ProcessUploadedFile(request.Image);
                }
                 
                if (request.Id>0)
                {
                    _db.Update(request);
                }
                else
                {
                    _db.Requests.Add(request);
                }
                _db.SaveChanges();
                
            }
           
            PickRequestVM pickRequestVm = new PickRequestVM()
            {
                RequestList = _db.Requests.Where(x=>x.ApplicationUser.Email==userCookie).Include(x => x.ApplicationUser).Include(x => x.Area).ToList()
            };
            return View(pickRequestVm);
        }
        public IActionResult RequestList()
        {
            List<Request> requestList =new List<Request>();
            string userCookie = HttpContext.Request.Cookies["user"];
            string managerCookie = HttpContext.Request.Cookies["manager"];
            if (string.IsNullOrEmpty(userCookie) && string.IsNullOrEmpty(managerCookie))
            {
                return RedirectToAction("Login", "User");
            }

            if (!string.IsNullOrEmpty(userCookie))
            {
                var user = _extension.GetCurrentUser(userCookie);
                requestList = _db.Requests.Where(x => x.ApplicationUserId == user.Id).Include(x => x.ApplicationUser).Include(x => x.Area).ToList();
            }

            if (!string.IsNullOrEmpty(managerCookie))
            {
                var manager = _db.Managers.FirstOrDefault(x => x.Email == managerCookie);
                requestList = _db.Requests.Where(x => x.AreaId == manager.AreaId).ToList();
            }

            return PartialView(requestList);
        }
        public IActionResult RequestListforUser()
        {
            List<Request> requestList = new List<Request>();
            string userCookie = HttpContext.Request.Cookies["user"];
            string managerCookie = HttpContext.Request.Cookies["manager"];
            if (string.IsNullOrEmpty(userCookie) && string.IsNullOrEmpty(managerCookie))
            {
                return RedirectToAction("Login", "User");
            }

            if (!string.IsNullOrEmpty(userCookie))
            {
                var user = _extension.GetCurrentUser(userCookie);
                requestList = _db.Requests.Where(x => x.ApplicationUserId == user.Id).Include(x => x.ApplicationUser).Include(x => x.Area).ToList();
            }

            if (!string.IsNullOrEmpty(managerCookie))
            {
                var manager = _db.Managers.FirstOrDefault(x => x.Email == managerCookie);
                requestList = _db.Requests.Where(x => x.AreaId == manager.AreaId).ToList();
            }
            return View(requestList);
        }

        [HttpGet]
        public IActionResult PickAndDump(string id)
        {
            string managerCookie = HttpContext.Request.Cookies["manager"];
            if (string.IsNullOrEmpty(managerCookie))
            {
                return RedirectToAction("ManagerLogin", "User");
            }
            PickedAndDump pickaback=new PickedAndDump();
            if (!string.IsNullOrEmpty(id))
            {
                 pickaback = _db.PickedAndDumps.FirstOrDefault(x => x.Id == Convert.ToInt32(id));
            }
            string result = HttpContext.Request.Cookies["manager"];
            var manager = _db.Managers.FirstOrDefault(x => x.Email==result);
            if (manager != null)
            {
                PickAndDumpVM vm = new PickAndDumpVM()
                {
                    PickedAndDumpList = _db.PickedAndDumps.Where(x=>x.ManagerId==manager.Id).ToList(),
                    PickedAndDump = pickaback,
                    RequestList = _extension.RequestListDD(manager.AreaId.ToString())
                };
                return View(vm);
            }
            else
            {
                return RedirectToAction("ManagerLogin", "User");
            }
        }
        [HttpPost]
        public IActionResult PickAndDump(PickedAndDump pickedAndDump)
        {
            string ManagerCookie = HttpContext.Request.Cookies["manager"];
            if (string.IsNullOrEmpty(ManagerCookie))
            {
                return RedirectToAction("ManagerLogin", "User");
            }
            // PickedAndDump pickanddump = new PickedAndDump();
            string result = HttpContext.Request.Cookies["manager"];
            var manager = _db.Managers.FirstOrDefault(x => x.Email == result);
            if (ModelState.IsValid)
            {
                Models.Request request = _db.Requests.FirstOrDefault(x => x.Id == pickedAndDump.RequestId);
                if (pickedAndDump.Id > 0)
                {
                    pickedAndDump.ManagerId = manager.Id;
                    _db.Update(pickedAndDump);
                    request.Status = true;
                    _db.Update(request);

                }
                else
                {
                    pickedAndDump.ManagerId = manager.Id;
                    _db.Add(pickedAndDump);
                    request.Status = true;
                    _db.Update(request);
                }

                _db.SaveChanges();
            }
            PickAndDumpVM vm = new PickAndDumpVM()
            {
                PickedAndDumpList = _db.PickedAndDumps.Where(x => x.ManagerId == manager.Id).ToList(),
                RequestList = _extension.RequestListDD(manager.AreaId.ToString())
            };
            return View(vm);
        }

        [HttpGet]
        public IActionResult PickAndDumpList()
        {
            string ManagerCookie = HttpContext.Request.Cookies["manager"];
            if (string.IsNullOrEmpty(ManagerCookie))
            {
                return RedirectToAction("ManagerLogin", "User");
            }
            return PartialView();
        }

        private string ProcessUploadedFile(IFormFile Photo)
        {
            string uniqueFileName = null;

            if (Photo != null)
            {
                string uploadsFolder = Path.Combine(_environment.WebRootPath, "RequestImages");
                uniqueFileName =Guid.NewGuid().ToString() + "_" + Photo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    Photo.CopyTo(fileStream);
                }
            }

            return uniqueFileName;
        }

        public IActionResult Dashboard()
        {
            DashboardVW dashboard = new DashboardVW();
            string managerCookie = HttpContext.Request.Cookies["manager"];
            if (string.IsNullOrEmpty(managerCookie))
            {
                return RedirectToAction("Login", "User");
            }

            if (!string.IsNullOrEmpty(managerCookie))
            {
                var manager = _extension.GetCurrentManager(managerCookie);
                var request = _db.Requests.Where(x => x.AreaId == manager.AreaId).Include(x => x.ApplicationUser).Include(x => x.Area).ToList();
                var duped = _db.PickedAndDumps.Where(x => x.ManagerId == manager.Id).Include(x => x.Request).ToList();
                dashboard.New = request.Where(x => x.Status == false).ToList().Count.ToString();
                dashboard.Done = duped.Count.ToString();
                dashboard.Total = (request.Count + duped.Count).ToString();
                if (!string.IsNullOrEmpty(dashboard.Total))
                {
                    dashboard.Rate = request.Count > 0
                        ? $"{(Convert.ToDecimal(dashboard.Done) * 100 / Convert.ToDecimal(dashboard.Total)):0.00}"
                        : "0";
                }
                dashboard.Total = (request.Count+duped.Count).ToString();
            }
            return View(dashboard);
        }
    }
}