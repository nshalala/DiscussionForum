using DiscussionForum.Application.DTOs.Auth;
using DiscussionForum.Application.DTOs.Tokens;

namespace DiscussionForum.Application.Abstractions.Services;

public interface IAuthService
{
    Task<JsonWebToken> LoginAsync(LoginDto dto);
    Task<JsonWebToken> RefreshLoginAsync(string refreshToken);
}