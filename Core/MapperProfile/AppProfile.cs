using AutoMapper;
using Core.Dtos;
using Data.Entities;

namespace Core.MapperProfiles
{
    public class AppProfile : Profile
    {
        public AppProfile()
        {
            CreateMap<CarDto, Car>().ReverseMap();

            CreateMap<Request, RequestDto>()
                .ForMember(x => x.UserName, opt => opt.MapFrom(src => src.User.UserName));
                
        }
    }
}