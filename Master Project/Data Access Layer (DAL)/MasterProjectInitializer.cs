using Master_Project.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Master_Project.Data_Access_Layer__DAL_
{
    public class MasterProjectInitializer : DropCreateDatabaseIfModelChanges<MasterProjectContext>
    {
        protected override void Seed(MasterProjectContext context)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

            if (!roleManager.RoleExists("Administrator"))
            {
                var role = new IdentityRole();
                role.Name = "Administrator";
                roleManager.Create(role);

            }

            // Tworzenie roli Dyspozytor     
            if (!roleManager.RoleExists("Dispatcher"))
            {
                var role = new IdentityRole();
                role.Name = "Dispatcher";
                roleManager.Create(role);

            }

            // Tworzenie roli Pracownik   
            if (!roleManager.RoleExists("Worker"))
            {
                var role = new IdentityRole();
                role.Name = "Worker";
                roleManager.Create(role);

            }
        }
    }
}