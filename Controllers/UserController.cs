using Microsoft.AspNetCore.Mvc;
using WeGout.Models;
using WeGout.Helpers;
using WeGout.Interfaces;

namespace WeGout.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {


        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;

        public UserController(ILogger<UserController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [LoginRequired]
        [HttpGet("GetUserList")]
        public async Task<WGResponse<Paging<UserDto>>> GetUserList([FromQuery] Paging pagingParameters)
        {
            return await _userService.GetUserList(pagingParameters);
        }

        [LoginRequired]
        [HttpGet("GetUserById/{id}")]
        public async Task<WGResponse<UserDto>> GetUserById(long id)
        {
            return await _userService.GetUserById(id);
        }
        
        [LoginRequired]
        [HttpGet("GetUserProfile")]
        public async Task<WGResponse<UserDto>> GetUserProfile()
        {
            var id = (HttpContext.Items["User"] as UserDto).Id;
            return await _userService.GetUserProfile(id);
        }

        // [AdminRequired]
        [HttpPost("AddUser")]
        public async Task<WGResponse> AddUser([FromBody] UserRequest userRequest)
        {
            return await _userService.AddUser(userRequest);
        }
        
        [LoginRequired]
        [HttpPost("UpdateUser")]
        public async Task<WGResponse> UpdateUser([FromBody] UserDto userDto)
        {
            return await _userService.UpdateUser(userDto);
        }


        [LoginRequired]
        [HttpGet("DeleteUserById/{id}")]
        public async Task<WGResponse> DeleteUserById(int id)
        {
            return await _userService.DeleteUserById(id);
        }


        [HttpPost("Login")]
        public async Task<WGResponse<AuthenticateResponse>> Login(AuthenticateRequest model)
        {
            return await _userService.Login(model);
        }
    }
}