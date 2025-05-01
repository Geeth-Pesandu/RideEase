using System.ComponentModel.DataAnnotations;

namespace RideEase.Models
{
    public class Vehicle
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Type { get; set; } // Car, Van, Tuk Tuk, Scooter

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public decimal PricePerDay { get; set; }

        public bool IsAvailable { get; set; }
    }
}
