using GeoToast.Data.Models;

namespace GeoToast.Models
{
    public class PropertyReadModel
    {
        public int Id { get; set; }
        
        public PropertyKind Kind { get; set; }

        public string Name { get; set; }

        public string Url { get; set; }

    }
}