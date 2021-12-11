using Microsoft.AspNetCore.Mvc;
using WeGout.Models;
using WeGout.Entities;
using WeGout.Interfaces;
using WeGout.Helpers;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace WeGout.Services
{

    public class UserService : IUserService
    {


        private readonly ILogger<UserService> _logger;
        private readonly IMapper _mapper;
        private readonly WGContext _context;

        public UserService(ILogger<UserService> logger, WGContext context, IMapper mapper)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
        }

        public async Task<WGResponse<Paging<UserDto>>> GetUserList(Paging pagingParameters)
        {
            WGResponse<Paging<UserDto>> response = new WGResponse<Paging<UserDto>>();
            try
            {
                response.Data = await _context.User.Include(l => l.Gender).Include(l => l.ProfilePhoto).ToPagingAsync<User, UserDto>(pagingParameters, _mapper);
            }
            catch (Exception e)
            {

            }

            return response;
        }

        public async Task<WGResponse<UserDto>> GetUserById(int id)
        {
            WGResponse<UserDto> response = new WGResponse<UserDto>();
            try
            {
                var user = await _context.User.Include(l => l.Gender).Include(l => l.ProfilePhoto).Where(l => l.Id == id).FirstOrDefaultAsync();
                response.Data = _mapper.Map<UserDto>(user);
            }
            catch (Exception e)
            {

            }

            return response;
        }

        public async Task<WGResponse> AddUser(UserRequest userRequest)
        {
            WGResponse response = new WGResponse();
            try
            {
                await _context.AddAsync(_mapper.Map<User>(userRequest));
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