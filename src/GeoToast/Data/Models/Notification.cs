using System;
using System.ComponentModel.DataAnnotations;

namespace GeoToast.Data.Models
{
    public class Notification
    {
        [Required]
        public DateTime EndDate { get; set; }

        public int Id { get; set; }

        [Required]
        public float Latitude { get; set; }

        [Required]
        public float Longitude { get; set; }

        [Required]
        public Property Property { get; set; }

        [Required]
        public DateTime StartDate { get; set; }
    }
}