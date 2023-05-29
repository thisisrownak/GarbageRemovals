using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using GarbageRemovals.Data;
using GarbageRemovals.Models;
using GarbageRemovals.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GarbageRemovals.Common
{
    public class ExtensionMethods
    {
        ApplicationDbContext _db;
        public ExtensionMethods(ApplicationDbContext applicationDbContext)
        {
            this._db = applicationDbContext;
        }
        public bool IsUserExist(string email)
        {
            bool result = false;
            ApplicationUser applicationUser = _db.AppicationUsers.FirstOrDefault(x => x.Email == email);
            return applicationUser != null;
        }
        public ApplicationUser GetCurrentUser(string email)
        {
            ApplicationUser applicationUser = _db.AppicationUsers.FirstOrDefault(x => x.Email == email);
            return applicationUser;
        }
        public Manager GetCurrentManager(string email)
        {
            Manager applicationUser = _db.Managers.FirstOrDefault(x => x.Email == email);
            return applicationUser;
        }
        public List<SelectListItem> AreaDD()
        {
            List<SelectListItem> areaDropDowns=new List<SelectListItem>();
            var areas = _db.Areas.ToList();
            foreach (var area in areas)
            {
                SelectListItem down = new SelectListItem
                {
                    Text = area.Name,
                    Value = area.Id.ToString()
                };
                areaDropDowns.Add(down);
            }
            return areaDropDowns;
        }
        public List<SelectListItem> DupmStationDD()
        {
            List<SelectListItem> areaDropDowns = new List<SelectListItem>();
            var areas = _db.DumpStations.ToList();
            foreach (var area in areas)
            {
                SelectListItem down = new SelectListItem
                {
                    Text = area.Name,
                    Value = area.Id.ToString()
                };
                areaDropDowns.Add(down);
            }
            return areaDropDowns;
        }
        public List<SelectListItem> ManagerDD()
        {
            List<SelectListItem> areaDropDowns = new List<SelectListItem>();
            var areas = _db.Managers.ToList();
            foreach (var area in areas)
            {
                SelectListItem down = new SelectListItem
                {
                    Text = area.Name,
                    Value = area.Id.ToString()
                };
                areaDropDowns.Add(down);
            }
            return areaDropDowns;
        }
        public List<SelectListItem>RequestListDD(string areaid)
        {
            List<Request>areas=new List<Request>();
            List<SelectListItem> areaDropDowns = new List<SelectListItem>();
            if (string.IsNullOrEmpty(areaid))
            {
                areas = _db.Requests.Where(x=>x.Status==false).OrderByDescending(x=>x.Id).ToList();
            }
            else
            {
                areas = _db.Requests.Where(x => x.AreaId == Convert.ToInt32(areaid) && x.Status == false).OrderByDescending(x => x.Id).ToList();
            }
            
            foreach (var area in areas)
            {
                SelectListItem down = new SelectListItem
                {
                    Text = area.Title,
                    Value = area.Id.ToString()
                };
                areaDropDowns.Add(down);
            }
            return areaDropDowns;
        }

    }
}
