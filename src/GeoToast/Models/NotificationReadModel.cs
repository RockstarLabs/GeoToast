using System;

namespace GeoToast.Models
{
    public class NotificationReadModel
    {
        public DateTime EndDate { get; set; }

        public int Id { get; set; }

        public LocationModel Location { get; set; }

        public DateTime StartDate { get; set; }
    }
}