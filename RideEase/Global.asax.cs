using System;
using System.Data.Entity;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using RideEase.Models;

namespace RideEase
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {

            Database.SetInitializer(new RideEase.Models.DbInitializer());

            // Existing setup
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // 🔐 Add this block to create Admin role & assign an admin user
            var context = new ApplicationDbContext();
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            // Create the Admin role if it doesn't exist
            if (!roleManager.RoleExists("Admin"))
            {
                roleManager.Create(new IdentityRole("Admin"));
            }

            // Assign a specific user to the Admin role
            var adminEmail = "admin@gmail.com"; // <-- CHANGE this to your actual email
            var adminUser = userManager.FindByEmail(adminEmail);
            if (adminUser != null && !userManager.IsInRole(adminUser.Id, "Admin"))
            {
                userManager.AddToRole(adminUser.Id, "Admin");
            }
        }
    }
}
