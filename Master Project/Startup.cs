using Master_Project.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Master_Project.Startup))]
namespace Master_Project
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

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
