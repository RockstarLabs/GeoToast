using System.ComponentModel.DataAnnotations;

namespace GeoToast.Data.Models
{
    public class Property
    {
        public int Id { get; set; }

        [Required]
        public PropertyKind Kind { get; set; }

        [Required]
        public string Name { get; set; }

        public string Url { get; set; }

        [Required]
        public string UserId { get; set; }
    }
}