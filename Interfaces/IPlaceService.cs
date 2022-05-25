using WeGout.Entities;
using WeGout.Models;
using WeGout.Helpers;

namespace WeGout.Interfaces
{
    public interface IPlaceService    
    {
        Task<WGResponse<Paging<PlaceShortDef>>> GetPlaceList(Paging pagingParameters);
        Task<WGResponse<PlaceDto>> GetPlaceById(int id);
        Task<WGResponse> AddPlace(PlaceRequest placeRequest);
        Task<WGResponse> DeletePlaceById(int id);
        Task<WGResponse<List<PlaceShortDef>>> GetPlaceListViaIntersect(string wkt);
        Task<WGResponse<List<PlaceShortDef>>> GetOwnersPlaces(long userId);
        Task<WGResponse> UpdatePlace(PlaceDto placeDto);
        Task<WGResponse> AddToFavPlace(long placeId, long userId);
        Task<WGResponse> DeleteFavPlaceById(long placeId,long userId);


    }
}