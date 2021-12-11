using WeGout.Models;
using WeGout.Helpers;

namespace WeGout.Interfaces
{
    public interface IUserService    
    {
        Task<WGResponse<Paging<UserDto>>> GetUserList(Paging pagingParameters);
        Task<WGResponse<UserDto>> GetUserById(int id);
        Task<WGResponse> AddUser(UserRequest userRequest);

    }
}