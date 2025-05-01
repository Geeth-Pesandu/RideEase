using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RideEase.Models
{


    public enum BookingStatus
    {
        Pending,
        Approved,
        Rejected
    }
    public class Booking
    {
        public int Id { get; set; }

        [Required]
        public int VehicleId { get; set; }

        [ForeignKey("VehicleId")]
        public virtual Vehicle Vehicle { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        public string CustomerName { get; set; }

        public string CustomerEmail { get; set; }

        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

        public BookingStatus Status { get; set; } = BookingStatus.Pending;
    }
}
