using AutoMapper;
using GeoToast.Data.Models;

namespace GeoToast.Models.Mappings
{
    public class NotificationProfile : Profile
    {
        public NotificationProfile()
        {
            CreateMap<Notification, NotificationReadModel>()
                .ForMember(m => m.Location, o => o.MapFrom(x => x));

            CreateMap<Notification, LocationModel>();
            
            CreateMap<NotificationCreateModel, Notification>()
                .ForMember(m => m.Latitude, o => o.MapFrom(s => s.Location.Latitude))
                .ForMember(m => m.Longitude, o => o.MapFrom(s => s.Location.Longitude));
        }
    }
}