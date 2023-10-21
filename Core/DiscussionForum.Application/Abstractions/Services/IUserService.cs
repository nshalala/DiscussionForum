using DiscussionForum.Application.DTOs.User;

namespace DiscussionForum.Application.Abstractions.Services;

public interface IUserService
{
    Task<List<UserListDto>> GetAllUsersAsync();
    Task<UserDetailDto> GetUserAsync(string id);
    Task<bool> CreateUserAsync(CreateUserDto model);
    Task<bool> UpdateUserAsync(UpdateUserDto model);
    Task<bool> DeleteUserAsync(string id);
}
