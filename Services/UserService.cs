using Microsoft.AspNetCore.Mvc;
using WeGout.Models;
using WeGout.Entities;
using WeGout.Interfaces;
using WeGout.Helpers;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace WeGout.Services
{

    public class UserService : IUserService
    {


        private readonly ILogger<UserService> _logger;
        private readonly IMapper _mapper;
        private readonly WGContext _context;
        private readonly IConfiguration _configuration;

        public UserService(ILogger<UserService> logger, WGContext context, IMapper mapper,IConfiguration configuration)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
            _configuration=configuration;
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
                response.SetError(OperationMessages.DbError);
            }

            return response;
        }

        public async Task<WGResponse<UserDto>> GetUserById(long id)
        {
            WGResponse<UserDto> response = new WGResponse<UserDto>();
            try
            {
                var user = await _context.User.Include(l => l.Gender).Include(l => l.ProfilePhoto).Where(l => l.Id == id).FirstOrDefaultAsync();
                response.Data = _mapper.Map<UserDto>(user);
            }
            catch (Exception e)
            {
                response.SetError(OperationMessages.DbError);
            }

            return response;
        }    
        
        public async Task<WGResponse<UserDto>> GetUserProfile(long id)
        {
            WGResponse<UserDto> response = new WGResponse<UserDto>();
            try
            {
                var user = await _context.User.Include(l => l.Gender).Include(l => l.ProfilePhoto).Where(l => l.Id == id).FirstOrDefaultAsync();
                response.Data = _mapper.Map<UserDto>(user);
                var places=await _context.Owner.Include(l => l.Place).Where(l => l.UserId == id)
                    .Select(l => l.Place).ToListAsync();
                var favPlaces=await _context.FavPlaces.Include(l => l.Place).Where(l => l.UserId == id)
                    .Select(l => l.Place).ToListAsync();
                response.Data.Places=_mapper.Map<List<PlaceShortDef>>(places);
                response.Data.FavPlaces=_mapper.Map<List<PlaceShortDef>>(favPlaces);
                response.SetSuccess();
            }
            catch (Exception e)
            {
                response.SetError(OperationMessages.DbError);
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

        public async Task<WGResponse> DeleteUserById(int id)
        {
            WGResponse response = new WGResponse();
            try
            {
                var user = await _context.User.Where(l => l.Id == id).FirstOrDefaultAsync();
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
        
        public async Task<WGResponse> UpdateUser(UserDto userDto)
        {
            WGResponse response = new WGResponse();
            try
            {
                var user = await _context.User.Where(l => l.Id == userDto.Id).FirstOrDefaultAsync();
                if (user != null)
                {
                    user.Name = userDto.Name;
                    user.Surname = userDto.Surname;
                    user.Email = userDto.Email;
                    user.PhoneNumber = userDto.PhoneNumber;
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

        public async Task<WGResponse<AuthenticateResponse>> Login(AuthenticateRequest model)
        {
            WGResponse<AuthenticateResponse> response = new WGResponse<AuthenticateResponse>();
            var userEnt = await _context.User.FirstOrDefaultAsync(x => x.Email == model.Email && x.Password == model.Password);

            if (userEnt == null)
            {
                response.SetError(OperationMessages.AuthenticateError);
            }
            else
            {
                var user = _mapper.Map<User, UserDto>(userEnt);
                var token = generateJwtToken(user);
                response.Data = new AuthenticateResponse(user, token);
                response.SetSuccess();
            }

            return response;
        }

        private string generateJwtToken(UserDto user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("JWT:Secret"));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}