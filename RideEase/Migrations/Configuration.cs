namespace RideEase.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using RideEase.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<RideEase.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(RideEase.Models.ApplicationDbContext context)
        {
            if (!context.Vehicles.Any())
            {
                context.Vehicles.AddRange(new List<Vehicle>
        {
            new Vehicle { Name = "City Car", Type = "Car", Description = "Perfect for city drives.", PricePerDay = 5000, IsAvailable = true, ImageUrl = "/Images/car1.jpg" },
            new Vehicle { Name = "Family Van", Type = "Van", Description = "Spacious and reliable.", PricePerDay = 8000, IsAvailable = true, ImageUrl = "/Images/van1.jpg" },
            new Vehicle { Name = "Eco Tuk Tuk", Type = "Tuk Tuk", Description = "Fun city ride.", PricePerDay = 3000, IsAvailable = true, ImageUrl = "/Images/tuktuk1.jpg" },
            new Vehicle { Name = "Speedy Scooter", Type = "Scooter", Description = "Zip through traffic.", PricePerDay = 2000, IsAvailable = true, ImageUrl = "/Images/scooter1.jpg" }
        });

                context.SaveChanges();
            }
        }

    }
}
