using Microsoft.AspNetCore.Mvc;
using WeGout.Models;
using WeGout.Helpers;
using WeGout.Interfaces;

namespace WeGout.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PlaceController : ControllerBase
    {


        private readonly ILogger<PlaceController> _logger;
        private readonly IPlaceService _placeService;

        public PlaceController(ILogger<PlaceController> logger,IPlaceService placeService)
        {
            _logger = logger;
            _placeService = placeService;
        }

        [LoginRequired]
        [HttpGet("GetPlaceList")]
        public async Task<WGResponse<Paging<PlaceDto>>> GetPlaceList([FromQuery] Paging pagingParameters)
        {
            return await _placeService.GetPlaceList(pagingParameters);
        }

        [LoginRequired]
        [HttpGet("GetPlaceById/{id}")]
        public async Task<WGResponse<PlaceDto>> GetPlaceById(int id)
        {
            return await _placeService.GetPlaceById(id);
        }

        [AdminRequired]
        [HttpPost("AddPlace")]
        public async Task<WGResponse> AddPlace([FromBody] PlaceRequest placeRequest)
        {
            return await _placeService.AddPlace(placeRequest);
        }

        [AdminRequired]
        [HttpGet("DeletePlaceById/{id}")]
        public async Task<WGResponse> DeletePlaceById(int id)
        {
            return await _placeService.DeletePlaceById(id);
        }
    }
}