using AutoMapper;
using GeoToast.Data.Models;

namespace GeoToast.Models.Mappings
{
    public class WebsiteProfile : Profile
    {
        public WebsiteProfile()
        {
            CreateMap<Website, WebsiteReadModel>();
            CreateMap<WebsiteCreateModel, Website>();
        }
    }
}