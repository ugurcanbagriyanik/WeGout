using Microsoft.AspNetCore.Mvc;
using WeGout.Models;
using WeGout.Helpers;
using WeGout.Interfaces;

namespace WeGout.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {


        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;

        public UserController(ILogger<UserController> logger,IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpGet("GetUserList")]
        public async Task<WGResponse<Paging<UserDto>>> GetUserList([FromQuery] Paging pagingParameters)
        {
            return await _userService.GetUserList(pagingParameters);
        }

        [HttpGet("GetUserById/{id}")]
        public async Task<WGResponse<UserDto>> GetUserById(int id)
        {
            return await _userService.GetUserById(id);
        }

        [HttpPost("AddUser")]
        public async Task<WGResponse> AddUser([FromBody] UserRequest userRequest)
        {
            return await _userService.AddUser(userRequest);
        }
    }
}