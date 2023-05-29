using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using GarbageRemovals.Models;

namespace GarbageRemovals.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            string UserCookie = HttpContext.Request.Cookies["user"];
            string ManagerCookie = HttpContext.Request.Cookies["manager"];
            if (!string.IsNullOrEmpty(UserCookie))
            {
                return RedirectToAction("Index","Garbage");
            }
            else if (!string.IsNullOrEmpty(ManagerCookie))
            {
                return RedirectToAction("Dashboard", "Garbage");
            }
            {
                
            }
            return RedirectToAction("Login", "User");
            
        }

        public IActionResult Privacy()
        {
            string UserCookie = HttpContext.Request.Cookies["user"];
            string ManagerCookie = HttpContext.Request.Cookies["manager"];
            if (string.IsNullOrEmpty(UserCookie) && string.IsNullOrEmpty(ManagerCookie))
            {
                return RedirectToAction("Login", "User");
            }
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
