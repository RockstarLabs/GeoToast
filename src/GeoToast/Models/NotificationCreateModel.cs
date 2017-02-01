using System;

namespace GeoToast.Models
{
    public class NotificationCreateModel
    {
        public DateTime EndDate { get; set; }

        public LocationModel Location { get; set; }
        
        public DateTime StartDate { get; set; }
 
    }
}