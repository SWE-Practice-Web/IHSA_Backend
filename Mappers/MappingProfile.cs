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
            CreateMap<RiderModel, RiderResponseModel>()
                .ReverseMap();
            CreateMap<EventModel, EventRequestModel>()
                .ReverseMap();
            CreateMap<EventModel, EventResponseModel>()
                .ReverseMap();
            CreateMap<SchoolModel, SchoolRequestModel>()
                .ReverseMap();
            CreateMap<SchoolModel, SchoolResponseModel>()
                .ReverseMap();
        }
    }
}
