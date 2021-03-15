using Master_Project.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Master_Project.Data_Access_Layer__DAL_
{
    public class MasterProjectContext : DbContext
    {
        public MasterProjectContext(): base("DefaultConnection")
        {

        }
        public DbSet<Trains> Trains { get; set; }
        public DbSet<Timetables> Timetables { get; set; }
        public DbSet<WarningsList> Warningslist { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<ServicePlan> ServicePlans { get; set; }
    }
}