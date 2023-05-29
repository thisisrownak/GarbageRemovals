using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GarbageRemovals.Common;
using GarbageRemovals.Data;
using GarbageRemovals.Models;
using GarbageRemovals.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.EntityFrameworkCore;

namespace GarbageRemovals.Controllers
{
    public class UserController : Controller
    {
        ApplicationDbContext _db;
        private ExtensionMethods _extensionMethods;
        public UserController(ApplicationDbContext applicationDbContext)
        {
            this._db = applicationDbContext;
             this._extensionMethods = new ExtensionMethods(_db);
        }
        [HttpGet]
        public IActionResult Login()
        {
            string cookie =HttpContext.Request.Cookies["user"];
            if (string.IsNullOrEmpty(cookie))
            {
                return View();
            }
            return RedirectToAction("Index", "Garbage");
        }

        [HttpPost]
        public async Task<IActionResult> Login(Login login)
        {
            string ERROR_MESSAGE = null;
            if (ModelState.IsValid)
            {
                login.Email=login.Email.ToUpper();
                ApplicationUser applicationUser =await _db.AppicationUsers
                    .Where(x => x.Email == login.Email && x.Password == login.Password).FirstOrDefaultAsync();
                if (applicationUser!=null)
                {
                    SetCookie("user", login.Email, 100000);
                    SetCookie("userName",applicationUser.FirstName+" "+applicationUser.LastName, 100000);
                    Response.Cookies.Delete("manager");
                    Response.Cookies.Delete("managerName");
                    return RedirectToAction("Index", "Garbage");
                }
                else
                {
                    ERROR_MESSAGE = "Invalid user name password!";
                }
               
            }

            ViewBag.Error = ERROR_MESSAGE;
            return View();
        }

        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Registration(ApplicationUser applicationUser)
        {
           
            if (ModelState.IsValid)
            {
                applicationUser.Email = applicationUser.Email.ToUpper();
                if (!_extensionMethods.IsUserExist(applicationUser.Email))
                {
                    _db.Add(applicationUser);
                    _db.SaveChanges();
                    ViewBag.Count = '1';
                    ViewBag.Message = "Successfully Added";
                    return RedirectToAction("Login","User");
                }
                else
                {
                    ViewBag.Count = '2';
                    ViewBag.Message = "This email already in use";
                     return View();
                }
            }
            else
            {
                ViewBag.Count = '2';
                ViewBag.Message = "input Error";
                 return View();
            }          
        }

        public void SetCookie(string key, string value, int? expireTime)
        {
            CookieOptions option = new CookieOptions();

            if (expireTime.HasValue)
                option.Expires = DateTime.Now.AddMinutes(expireTime.Value);
            else
                option.Expires = DateTime.Now.AddMilliseconds(10);

            Response.Cookies.Append(key, value, option);
        }
        [HttpGet]
        public IActionResult ManagerLogin()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ManagerLogin(Login login)
        {
            string ERROR_MESSAGE = null;
            if (ModelState.IsValid)
            {
                Manager manager = _db.Managers
                    .FirstOrDefault(x => x.Email == login.Email && x.Password == login.Password);
                if (manager!=null)
                {
                    SetCookie("manager", login.Email, 1000000);
                    SetCookie("managerName", manager.Name, 1000000);
                    Response.Cookies.Delete("user");
                    Response.Cookies.Delete("userName");
                    return RedirectToAction("Dashboard", "Garbage");
                }
                else
                {
                    ERROR_MESSAGE = "Invalid user name password!";
                    ViewBag.Error = ERROR_MESSAGE;
                    return View();
                }
            }
            else
            {
                ViewBag.Error = ERROR_MESSAGE;
                return View();
            }
        }
        public IActionResult Logout()
        {
            Response.Cookies.Delete("user");
            Response.Cookies.Delete("userName");
            return RedirectToAction("Login", "User");
        }
        public IActionResult ManagerLogout()
        {
            Response.Cookies.Delete("manager");
            Response.Cookies.Delete("managerName");
            return RedirectToAction("ManagerLogin", "User");
        }

        public async Task<IActionResult> Profile()
        {
            string cookie = HttpContext.Request.Cookies["user"];
            if (string.IsNullOrEmpty(cookie))
            {
                return RedirectToAction("Login","User");
            }

            var userDetail = await _db.AppicationUsers.Where(x => x.Email == cookie).FirstOrDefaultAsync();

            UserVW user = new UserVW()
            {
                FirstName = userDetail.FirstName,
                LastName = userDetail.LastName,
                Email = userDetail.Email,
                Phone = userDetail.Phone,
                Address = userDetail.Address
            };
            return View(user);
        }

    }
}