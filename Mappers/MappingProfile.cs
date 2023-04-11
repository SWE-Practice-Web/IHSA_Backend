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
            CreateMap<EventElementOrderModel, EventElementOrderRequestModel>()
                .ReverseMap();
            CreateMap<EventElementOrderModel, EventElementOrderResponseModel>()
                .ReverseMap();
            CreateMap<EventPairModel, EventPairRequestModel>()
                .ReverseMap();
            CreateMap<SchoolModel, SchoolRequestModel>()
                .ReverseMap();
            CreateMap<SchoolModel, SchoolResponseModel>()
                .ReverseMap();
        }
    }
}
