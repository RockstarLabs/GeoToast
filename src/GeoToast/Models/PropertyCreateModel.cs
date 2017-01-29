using GeoToast.Data.Models;

namespace GeoToast.Models
{
    public class PropertyCreateModel
    {
        public PropertyKind Kind { get; set; }

        public string Name { get; set; }

        public string Url { get; set; }
    }
}