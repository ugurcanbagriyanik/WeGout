using Microsoft.AspNetCore.Mvc;
using WeGout.Models;
using WeGout.Entities;
using WeGout.Interfaces;
using WeGout.Helpers;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace WeGout.Services
{

    public class PlaceService : IPlaceService
    {


        private readonly ILogger<PlaceService> _logger;
        private readonly IMapper _mapper;
        private readonly WGContext _context;

        public PlaceService(ILogger<PlaceService> logger, WGContext context, IMapper mapper)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
        }

        public async Task<WGResponse<Paging<PlaceDto>>> GetPlaceList(Paging pagingParameters)
        {
            WGResponse<Paging<PlaceDto>> response = new WGResponse<Paging<PlaceDto>>();
            try
            {
                response.Data = await _context.Place.Include(l => l.BannerPhoto).ToPagingAsync<Place, PlaceDto>(pagingParameters, _mapper);
            }
            catch (Exception e)
            {

            }

            return response;
        }

        public async Task<WGResponse<PlaceDto>> GetPlaceById(int id)
        {
            WGResponse<PlaceDto> response = new WGResponse<PlaceDto>();
            try
            {
                var user = await _context.Place.Include(l => l.BannerPhoto).Where(l => l.Id == id).FirstOrDefaultAsync();
                response.Data = _mapper.Map<PlaceDto>(user);
            }
            catch (Exception e)
            {

            }

            return response;
        }

        public async Task<WGResponse> AddPlace(PlaceRequest placeRequest)
        {
            WGResponse response = new WGResponse();
            try
            {
                await _context.AddAsync(_mapper.Map<Place>(placeRequest));
                await _context.SaveChangesAsync();
                response.SetSuccess();
            }
            catch (Exception e)
            {
                response.SetError(OperationMessages.DbError);
            }

            return response;
        }
    }
}