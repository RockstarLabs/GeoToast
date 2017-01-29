using AutoMapper;
using GeoToast.Data.Models;

namespace GeoToast.Models.Mappings
{
    public class PropertyProfile : Profile
    {
        public PropertyProfile()
        {
            CreateMap<Property, PropertyReadModel>();
            CreateMap<PropertyCreateModel, Property>();
        }
    }
}