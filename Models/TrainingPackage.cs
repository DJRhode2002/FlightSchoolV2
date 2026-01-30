using System.ComponentModel.DataAnnotations;

namespace FlightSchoolV2.Models
{
    public class TrainingPackage
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; } // e.g., "Private Pilot License"
        [Required]
        public string Description { get; set; } // What's included
        [Required]
        public decimal Price { get; set; }
        public string ImageUrl { get; set; } // Optional: image of the aircraft
    }
}