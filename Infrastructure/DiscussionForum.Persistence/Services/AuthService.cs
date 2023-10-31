using DiscussionForum.Application.Abstractions.Services;
using DiscussionForum.Application.Abstractions.Token;
using DiscussionForum.Application.DTOs.Auth;
using DiscussionForum.Application.DTOs.Tokens;
using DiscussionForum.Application.Exceptions;
using DiscussionForum.Application.Repositories;
using DiscussionForum.Domain.Entities;
using DiscussionForum.Infrastructure.Operations;
using Microsoft.Extensions.Configuration;

namespace DiscussionForum.Persistence.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenHandler _tokenHandler;
    private readonly IUserService _userService;
    private readonly IConfiguration _configuration;

    public AuthService(IUserRepository userRepository, ITokenHandler tokenHandler, IUserService userService, IConfiguration configuration)
    {
        _userRepository = userRepository;
        _tokenHandler = tokenHandler;
        _userService = userService;
        _configuration = configuration;
    }

    public async Task<JsonWebToken> LoginAsync(LoginDto dto)
    {
        User user = await _userRepository.GetSingleAsync(u =>
            u.Username == dto.UsernameOrEmail || u.Email == dto.UsernameOrEmail) ?? throw new NotFoundException<User>();

        bool isPasswordCorrect = PasswordOperation.VerifyPassword(dto.Password, user.Salt, user.HashedPassword);

        if (!isPasswordCorrect) throw new IncorrectPasswordException();

        JsonWebToken jsonWebToken = _tokenHandler.CreateAccessToken(user);
        await _userService.UpdateRefreshToken(jsonWebToken.RefreshToken, user, jsonWebToken.Expiration, int.Parse(_configuration["Token:RefreshTokenLifetime"]!));
        return jsonWebToken;
    }

    public async Task<JsonWebToken> RefreshLoginAsync(string refreshToken)
    {
        User? user = await _userRepository.GetSingleAsync(u => u.RefreshToken == refreshToken);

        if (user == null || user?.RefreshTokenExpires < DateTime.UtcNow)
            throw new Exception("User Not Found");

        JsonWebToken jsonWebToken = _tokenHandler.CreateAccessToken(user!);
        await _userService.UpdateRefreshToken(jsonWebToken.RefreshToken, user!, jsonWebToken.Expiration,
            int.Parse(_configuration["Token:RefreshTokenLifetime"]!));
        return jsonWebToken;
    }
}