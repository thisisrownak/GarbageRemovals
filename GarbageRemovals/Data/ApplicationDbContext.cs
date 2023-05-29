using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using GarbageRemovals.Models;
namespace GarbageRemovals.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<ApplicationUser> AppicationUsers { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<DumpStation> DumpStations{ get; set; }
        public DbSet<PickedAndDump> PickedAndDumps{ get; set; }
        public DbSet<Request> Requests{ get; set; }
        public DbSet<Area> Areas{ get; set; }
        
    }
}
