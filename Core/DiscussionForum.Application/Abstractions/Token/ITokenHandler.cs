using DiscussionForum.Application.DTOs.Tokens;
using DiscussionForum.Domain.Entities;

namespace DiscussionForum.Application.Abstractions.Token;

public interface ITokenHandler
{
    JsonWebToken CreateAccessToken(User user);
    string CreateRefreshToken();
}