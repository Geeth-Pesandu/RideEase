using System;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RideEase.Models;

namespace RideEase.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // ✅ BOOKINGS SECTION

        // GET: Admin/AllBookings
        public ActionResult AllBookings()
        {
            var bookings = db.Bookings
                .Include(b => b.Vehicle)
                .Include(b => b.User) // Make sure this is included
                .ToList();

            ViewBag.ApprovedCount = bookings.Count(b => b.Status == BookingStatus.Approved);
            ViewBag.PendingCount = bookings.Count(b => b.Status == BookingStatus.Pending);
            ViewBag.RejectedCount = bookings.Count(b => b.Status == BookingStatus.Rejected);

            return View(bookings);
        }

        [HttpPost]
        public ActionResult UpdateStatus(int id, string status)
        {
            var booking = db.Bookings.Find(id);
            if (booking == null) return HttpNotFound();

            if (Enum.TryParse(status, out BookingStatus parsedStatus))
            {
                booking.Status = parsedStatus;
                db.Entry(booking).State = EntityState.Modified;
                db.SaveChanges();
            }

            return RedirectToAction("AllBookings");
        }

        // ✅ VEHICLE MANAGEMENT SECTION

        public ActionResult Vehicles() => View(db.Vehicles.ToList());

        public ActionResult CreateVehicle() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateVehicle(Vehicle vehicle, HttpPostedFileBase ImageFile)
        {
            if (ModelState.IsValid)
            {
                if (ImageFile != null && ImageFile.ContentLength > 0)
                {
                    var originalFileName = System.IO.Path.GetFileName(ImageFile.FileName);
                    var safeFileName = originalFileName.Replace(" ", "-");
                    var imagePath = System.IO.Path.Combine(Server.MapPath("~/Images"), safeFileName);
                    ImageFile.SaveAs(imagePath);
                    vehicle.ImageUrl = "/Images/" + safeFileName;
                }

                db.Vehicles.Add(vehicle);
                db.SaveChanges();
                return RedirectToAction("Vehicles");
            }

            return View(vehicle);
        }

        public ActionResult EditVehicle(int id)
        {
            var vehicle = db.Vehicles.Find(id);
            if (vehicle == null) return HttpNotFound();
            return View(vehicle);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditVehicle(Vehicle vehicle)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vehicle).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Vehicles");
            }
            return View(vehicle);
        }

        public ActionResult DeleteVehicle(int id)
        {
            var vehicle = db.Vehicles.Find(id);
            if (vehicle == null) return HttpNotFound();
            return View(vehicle);
        }

        [HttpPost, ActionName("DeleteVehicle")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var vehicle = db.Vehicles.Find(id);
            db.Vehicles.Remove(vehicle);
            db.SaveChanges();
            return RedirectToAction("Vehicles");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                db.Dispose();
            base.Dispose(disposing);
        }
    }
}
