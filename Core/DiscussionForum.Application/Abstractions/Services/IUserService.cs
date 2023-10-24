using DiscussionForum.Application.DTOs.User;
using DiscussionForum.Domain.Entities;

namespace DiscussionForum.Application.Abstractions.Services;

public interface IUserService
{
    Task<List<UserListDto>> GetAllUsersAsync();
    Task<UserDetailDto> GetUserAsync(string id);
    Task<bool> RegisterUserAsync(RegisterUserDto model);
    Task<bool> UpdateUserAsync(UpdateUserDto model);
    Task UpdateRefreshToken(string refreshToken, User user, DateTime accessTokenExpires, int refreshTokenLifetime);
    Task<bool> DeleteUserAsync(string id);
}
