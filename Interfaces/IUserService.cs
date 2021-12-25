using WeGout.Models;
using WeGout.Helpers;

namespace WeGout.Interfaces
{
    public interface IUserService    
    {
        Task<WGResponse<Paging<UserDto>>> GetUserList(Paging pagingParameters);
        Task<WGResponse<UserDto>> GetUserById(long id);
        Task<WGResponse> AddUser(UserRequest userRequest);
        Task<WGResponse> DeleteUserById(int id);
        Task<WGResponse<AuthenticateResponse>> Login(AuthenticateRequest model);

    }
}