using AutoMapper;
using WeGout.Models;
using WeGout.Entities;
using WeGout.Helpers;
namespace WeGout.MapperProfiles
{
    public class PlaceProfile : Profile
    {

        public PlaceProfile()
        {

            CreateMap<Place, PlaceDto>()
            .ForMember(destination => destination.BannerPhoto, operations => operations.MapFrom(source => source.BannerPhoto != null ? source.BannerPhoto.Path : string.Empty))
            .ForMember(destination => destination.LocationWkt, operations => operations.MapFrom(source => source.Location.AsText()));

            CreateMap<PlaceRequest, Place>()
            .ForMember(destination => destination.Location, operations => operations.MapFrom(source => source.LocationWkt.ToGeometry()));

        }

    }
}