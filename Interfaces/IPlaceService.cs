using WeGout.Models;
using WeGout.Helpers;

namespace WeGout.Interfaces
{
    public interface IPlaceService    
    {
        Task<WGResponse<Paging<PlaceDto>>> GetPlaceList(Paging pagingParameters);
        Task<WGResponse<PlaceDto>> GetPlaceById(int id);
        Task<WGResponse> AddPlace(PlaceRequest placeRequest);

    }
}