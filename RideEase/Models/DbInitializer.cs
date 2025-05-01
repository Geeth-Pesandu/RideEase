using System.Collections.Generic;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace RideEase.Models
{
    public class DbInitializer : DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            var vehicles = new List<Vehicle>
            {
                // 🚗 Cars
                new Vehicle { Name = "Alto", Type = "Car", Description = "Compact and economical", PricePerDay = 1500, ImageUrl = "/Images/alto.jpg", IsAvailable = true },
                new Vehicle { Name = "Prius", Type = "Car", Description = "Hybrid for smooth rides", PricePerDay = 3000, ImageUrl = "/Images/prius.jpg", IsAvailable = true },
                new Vehicle { Name = "Prado", Type = "Car", Description = "Luxury off-roader", PricePerDay = 7000, ImageUrl = "/Images/prado.jpg", IsAvailable = true },
                new Vehicle { Name = "Vezel", Type = "Car", Description = "Stylish crossover", PricePerDay = 4500, ImageUrl = "/Images/vezel.jpg", IsAvailable = true },
                new Vehicle { Name = "Axio", Type = "Car", Description = "Reliable sedan", PricePerDay = 3500, ImageUrl = "/Images/axio.jpg", IsAvailable = true },
                new Vehicle { Name = "BMW", Type = "Car", Description = "Premium performance", PricePerDay = 8000, ImageUrl = "/Images/bmw.jpg", IsAvailable = true },
                new Vehicle { Name = "Viz", Type = "Car", Description = "Comfortable compact car", PricePerDay = 2700, ImageUrl = "/Images/viz.jpg", IsAvailable = true },
                new Vehicle { Name = "Prius Alpha", Type = "Car", Description = "Extended hybrid version", PricePerDay = 3200, ImageUrl = "/Images/prius-alpha.jpg", IsAvailable = true },

                // 🚐 Vans
                new Vehicle { Name = "Hiace", Type = "Van", Description = "Spacious for groups", PricePerDay = 6000, ImageUrl = "/Images/hiace.jpg", IsAvailable = true },
                new Vehicle { Name = "Nissan Caravan", Type = "Van", Description = "Perfect for family trips", PricePerDay = 5800, ImageUrl = "/Images/caravan.jpg", IsAvailable = true },

                // 🛺 Tuk Tuks
                new Vehicle { Name = "Bajaj RE", Type = "Tuk Tuk", Description = "3-wheeler for quick city trips", PricePerDay = 1000, ImageUrl = "/Images/bajaj-re.jpg", IsAvailable = true },
                new Vehicle { Name = "TVS King", Type = "Tuk Tuk", Description = "Efficient and affordable", PricePerDay = 1100, ImageUrl = "/Images/tvs-king.jpg", IsAvailable = true },
                new Vehicle { Name = "Piaggio Ape", Type = "Tuk Tuk", Description = "Compact city runner", PricePerDay = 1050, ImageUrl = "/Images/piaggio-ape.jpg", IsAvailable = true },
                new Vehicle { Name = "Lanka Ashok Leyland", Type = "Tuk Tuk", Description = "Robust and reliable", PricePerDay = 1150, ImageUrl = "/Images/tuktuk4.jpg", IsAvailable = true },

                // 🛵 Scooters
                new Vehicle { Name = "Honda Dio", Type = "Scooter", Description = "Stylish and sporty", PricePerDay = 900, ImageUrl = "/Images/dio.jpg", IsAvailable = true },
                new Vehicle { Name = "Yamaha Ray ZR", Type = "Scooter", Description = "Urban scooter with edge", PricePerDay = 950, ImageUrl = "/Images/yamaha-ray-zr.jpg", IsAvailable = true },
                new Vehicle { Name = "TVS Ntorq", Type = "Scooter", Description = "Smart and connected", PricePerDay = 970, ImageUrl = "/Images/tvs-ntorq.jpg", IsAvailable = true },
                new Vehicle { Name = "Suzuki Access", Type = "Scooter", Description = "Efficient and reliable", PricePerDay = 920, ImageUrl = "/Images/suzuki-access.jpg", IsAvailable = true }
            };

            vehicles.ForEach(v => context.Vehicles.Add(v));
            context.SaveChanges();

            // ✅ Seed Admin user and role
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            if (!roleManager.RoleExists("Admin"))
                roleManager.Create(new IdentityRole("Admin"));

            var adminEmail = "admin@rideease.com";
            var adminPassword = "Admin@123";

            if (userManager.FindByEmail(adminEmail) == null)
            {
                var adminUser = new ApplicationUser { UserName = adminEmail, Email = adminEmail };
                var result = userManager.Create(adminUser, adminPassword);
                if (result.Succeeded)
                {
                    userManager.AddToRole(adminUser.Id, "Admin");
                }
            }

            base.Seed(context);
        }
    }
}
