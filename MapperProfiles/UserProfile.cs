using AutoMapper;
using WeGout.Models;
using WeGout.Entities;
namespace WeGout.MapperProfiles
{
    public class UserProfile : Profile
    {

        public UserProfile(){

            CreateMap<User,UserDto>()
            .ForMember(destination => destination.ProfilePhoto,operations => operations.MapFrom(source => source.ProfilePhoto!=null ? source.ProfilePhoto.Path:string.Empty))
            .ForMember(destination => destination.Gender,operations=>operations.MapFrom(source => source.Gender.Name));

            CreateMap<UserRequest,User>();

        }

    }
}