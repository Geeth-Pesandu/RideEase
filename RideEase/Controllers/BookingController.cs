using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using RideEase.Models;

namespace RideEase.Controllers
{
    [Authorize] // 🔒 Only logged-in users can access this controller
    public class BookingController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Booking
        [Authorize]
        public ActionResult Index(string status)
        {
            string currentUserId = User.Identity.GetUserId();

            var bookings = db.Bookings
                .Include(b => b.Vehicle)
                .Where(b => b.UserId == currentUserId);

            // ✅ Filter by status if selected
            if (!string.IsNullOrEmpty(status) && Enum.TryParse(status, out BookingStatus parsedStatus))
            {
                bookings = bookings.Where(b => b.Status == parsedStatus);
            }

            // Send the selected status to the view for the dropdown
            ViewBag.SelectedStatus = status;

            ViewBag.ApprovedCount = bookings.Count(b => b.Status == BookingStatus.Approved);
            ViewBag.PendingCount = bookings.Count(b => b.Status == BookingStatus.Pending);
            ViewBag.RejectedCount = bookings.Count(b => b.Status == BookingStatus.Rejected);

            return View(bookings.ToList());
        }


        // GET: Booking/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Booking booking = db.Bookings.Include(b => b.Vehicle).FirstOrDefault(b => b.Id == id);
            if (booking == null || booking.UserId != User.Identity.GetUserId())
                return HttpNotFound();

            return View(booking);
        }

        // GET: Booking/Create
        public ActionResult Create(int? vehicleId)
        {
            ViewBag.VehicleId = new SelectList(db.Vehicles, "Id", "Name", vehicleId);
            return View();
        }

        // POST: Booking/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "VehicleId,StartDate,EndDate,CustomerName,CustomerEmail")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                // 🔗 Link the booking to the logged-in user
                booking.UserId = User.Identity.GetUserId();

                db.Bookings.Add(booking);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.VehicleId = new SelectList(db.Vehicles, "Id", "Name", booking.VehicleId);
            return View(booking);
        }

        // GET: Booking/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Booking booking = db.Bookings.Find(id);

            // Prevent editing other users' bookings
            if (booking == null || booking.UserId != User.Identity.GetUserId())
                return HttpNotFound();

            ViewBag.VehicleId = new SelectList(db.Vehicles, "Id", "Name", booking.VehicleId);
            return View(booking);
        }

        // POST: Booking/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,VehicleId,StartDate,EndDate,CustomerName,CustomerEmail")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                var existingBooking = db.Bookings.Find(booking.Id);

                if (existingBooking == null || existingBooking.UserId != User.Identity.GetUserId())
                    return HttpNotFound();

                // Update fields manually to avoid binding UserId
                existingBooking.VehicleId = booking.VehicleId;
                existingBooking.StartDate = booking.StartDate;
                existingBooking.EndDate = booking.EndDate;
                existingBooking.CustomerName = booking.CustomerName;
                existingBooking.CustomerEmail = booking.CustomerEmail;

                db.Entry(existingBooking).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.VehicleId = new SelectList(db.Vehicles, "Id", "Name", booking.VehicleId);
            return View(booking);
        }

        // GET: Booking/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Booking booking = db.Bookings.Include(b => b.Vehicle).FirstOrDefault(b => b.Id == id);

            if (booking == null || booking.UserId != User.Identity.GetUserId())
                return HttpNotFound();

            return View(booking);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cancel(int id)
        {
            var booking = db.Bookings.Find(id);
            if (booking == null || booking.UserId != User.Identity.GetUserId())
                return HttpNotFound();

            db.Bookings.Remove(booking); // 💡 Optional: set Status = BookingStatus.Cancelled instead
            db.SaveChanges();

            return RedirectToAction("Index");
        }


        // POST: Booking/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Booking booking = db.Bookings.Find(id);

            if (booking == null || booking.UserId != User.Identity.GetUserId())
                return HttpNotFound();

            db.Bookings.Remove(booking);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                db.Dispose();

            base.Dispose(disposing);
        }
    }
}
