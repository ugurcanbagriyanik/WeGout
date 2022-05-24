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

        public async Task<WGResponse<Paging<PlaceShortDef>>> GetPlaceList(Paging pagingParameters)
        {
            WGResponse<Paging<PlaceShortDef>> response = new WGResponse<Paging<PlaceShortDef>>();
            try
            {
                response.Data = await _context.Place.Include(l => l.BannerPhoto).ToPagingAsync<Place, PlaceShortDef>(pagingParameters, _mapper);
                response.SetSuccess(OperationMessages.Success);
            }
            catch (Exception e)
            {
                response.SetError(OperationMessages.DbError);
            }

            return response;
        }        
        public async Task<WGResponse<List<PlaceShortDef>>> GetPlaceListViaIntersect(string wkt)
        {
            WGResponse<List<PlaceShortDef>> response = new WGResponse<List<PlaceShortDef>>();
            try
            {
                var sqlScript = "select p.\"Id\",p.\"Name\",st_astext(\"Location\") as LocationWkt,"+
                                "\"Category\",FS.\"Path\" BannerPhoto from \"Place\" p " +
                                "inner join \"FileStorage\" FS on FS.\"Id\" = p.\"BannerPhotoId\" "+
                                "where st_intersects(\"Location\",st_geomfromtext('" +wkt+"'"+
                                ",4326))";
                // response.Data = await _context.Place.FromSqlRaw(sqlScript).ToListAsync();
                // response.Data = _context.ExecSQL<PlaceShortDef>(sqlScript);
                response.Data = await _context.Database.SqlQueryAsync<List<PlaceShortDef>>(sqlScript);
                response.SetSuccess(OperationMessages.Success);
            }
            catch (Exception e)
            {
                response.SetError(OperationMessages.DbError);
            }

            return response;
        }
        

        public async Task<WGResponse<PlaceDto>> GetPlaceById(int id)
        {
            WGResponse<PlaceDto> response = new WGResponse<PlaceDto>();
            try
            {
                var place = await _context.Place.Include(l => l.BannerPhoto).Where(l => l.Id == id).FirstOrDefaultAsync();
                response.Data = _mapper.Map<PlaceDto>(place);
            }
            catch (Exception e)
            {

            }

            return response;
        }

        public async Task<WGResponse<List<PlaceShortDef>>> GetOwnersPlaces(long userId)
        {
            WGResponse<List<PlaceShortDef>> response = new WGResponse<List<PlaceShortDef>>();
            try
            {
                var places = await _context.Owner.Include(l => l.Place).Where(l => l.UserId == userId)
                    .Select(l => l.Place).ToListAsync();
                response.Data = _mapper.Map<List<PlaceShortDef>>(places);
                response.SetSuccess();
            }
            catch (Exception e)
            {
                response.SetError(OperationMessages.DbError);
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

        public async Task<WGResponse> DeletePlaceById(int id)
        {
            WGResponse response = new WGResponse();
            try
            {
                var user = await _context.Place.Where(l => l.Id == id).FirstOrDefaultAsync();
                if (user != null)
                {
                    _context.Remove(user);
                    response.SetSuccess();
                }
                else
                {
                    response.SetError(OperationMessages.DbItemNotFound);
                }
            }
            catch (Exception e)
            {
                response.SetError(OperationMessages.DbError);
            }

            return response;
        }
        
        public async Task<WGResponse> UpdatePlace(PlaceDto placeDto)
        {
            WGResponse response = new WGResponse();
            try
            {
                var place = await _context.Place.Where(l => l.Id == placeDto.Id).FirstOrDefaultAsync();
                if (place != null)
                {
                    place.Name = placeDto.Name;
                    place.Address = placeDto.Address;
                    place.Category = placeDto.Category;
                    place.PhoneNumber = placeDto.PhoneNumber;
                    await _context.SaveChangesAsync();
                    
                    response.SetSuccess();
                }
                else
                {
                    response.SetError(OperationMessages.DbItemNotFound);
                }
            }
            catch (Exception e)
            {
                response.SetError(OperationMessages.DbError);
            }

            return response;
        }
    }
}