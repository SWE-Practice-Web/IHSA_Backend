using AutoMapper;
using IHSA_Backend.Models;

namespace IHSA_Backend.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<RiderModel, RiderRequestModel>()
                .ReverseMap();
            CreateMap<EventModel, EventRequestModel>()
                .ReverseMap();
            CreateMap<SchoolModel, SchoolRequestModel>()
                .ReverseMap();
        }
    }
}
