using System;
using System.Linq;
using System.Web.Mvc;
using RideEase.Models;

namespace RideEase.Controllers
{
    public class VehicleController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Fleet(string type, decimal? minPrice, decimal? maxPrice, int page = 1)
        {
            int pageSize = 8;
            int maxPages = 3;

            var vehicles = db.Vehicles.AsQueryable();

            if (!string.IsNullOrEmpty(type))
                vehicles = vehicles.Where(v => v.Type == type);

            if (minPrice.HasValue)
                vehicles = vehicles.Where(v => v.PricePerDay >= minPrice.Value);

            if (maxPrice.HasValue)
                vehicles = vehicles.Where(v => v.PricePerDay <= maxPrice.Value);

            ViewBag.Types = db.Vehicles.Select(v => v.Type).Distinct().ToList();
            ViewBag.CurrentType = type;
            ViewBag.MinPrice = minPrice;
            ViewBag.MaxPrice = maxPrice;
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = Math.Min((int)Math.Ceiling((double)vehicles.Count() / pageSize), maxPages);

            var pagedVehicles = vehicles
                .OrderBy(v => v.Name)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return View(pagedVehicles);
        }



        protected override void Dispose(bool disposing)
        {
            if (disposing)
                db.Dispose();
            base.Dispose(disposing);
        }
    }
}
