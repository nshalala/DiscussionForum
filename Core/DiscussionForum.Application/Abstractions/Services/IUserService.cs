using DiscussionForum.Application.DTOs.Community;
using DiscussionForum.Application.DTOs.User;
using DiscussionForum.Domain.Entities;

namespace DiscussionForum.Application.Abstractions.Services;

public interface IUserService
{
    Task<List<UserListDto>> GetAllUsersAsync(int skip, int take);
    Task<UserDetailDto> GetUserAsync(Guid id);
    Task<bool> RegisterUserAsync(RegisterUserDto model);
    Task<bool> UpdateUserAsync(UpdateUserDto model);
    Task UpdateRefreshToken(string refreshToken, User user, DateTime accessTokenExpires, int refreshTokenLifetime);
    Task<bool> DeleteUserAsync(Guid id);
}