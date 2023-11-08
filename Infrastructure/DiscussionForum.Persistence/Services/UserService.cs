using System.Security.Claims;
using AutoMapper;
using DiscussionForum.Application.Abstractions.Services;
using DiscussionForum.Application.DTOs.User;
using DiscussionForum.Application.Exceptions;
using DiscussionForum.Application.Repositories;
using DiscussionForum.Domain.Entities;
using DiscussionForum.Domain.Enums;
using DiscussionForum.Infrastructure.Operations;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace DiscussionForum.Persistence.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly IMapper _mapper;

    public UserService(IUserRepository userRepository, IHttpContextAccessor contextAccessor, IMapper mapper)
    {
        _userRepository = userRepository;
        _contextAccessor = contextAccessor;
        _mapper = mapper;
    }

    public async Task<List<UserListDto>> GetAllUsersAsync(int take, int skip = 0)
    {
        if (skip < 0 || take < 0)
            throw new ArgumentOutOfRangeException("Skip/Take values cannot be negative");
        return _mapper.Map<List<UserListDto>>(await _userRepository.GetAll(skip, take, false).ToListAsync());
    }

    public async Task<UserDetailDto> GetUserAsync(Guid id)
    {
        var user = await _userRepository.GetByIdAsync(id, false);
        if (user == null)
            throw new NotFoundException<User>();
        return _mapper.Map<UserDetailDto>(user);
    }

    public async Task<bool> RegisterUserAsync(RegisterUserDto model)
    {
        if (model.Password != model.PasswordConfirm)
            throw new NotMatchingPasswordsException();

        var hashedPassword = PasswordOperation.CalculateSha256Hash(model.Password);

        if (_userRepository.Table.Any(u => u.Username == model.Username))
            throw new UnavailableNameException();

        if (_userRepository.Table.Any(c => c.Email == model.Email))
            throw new UnavailableEmailException();

        var user = _mapper.Map<User>(model);

        user.HashedPassword = hashedPassword[0];
        user.Salt = hashedPassword[1];
        user.Role = ApplicationRole.User;

        var response = await _userRepository.AddAsync(user);
        await _userRepository.SaveAsync();
        return response;
    }

    public async Task<bool> UpdateUserAsync(UpdateUserDto model)
    {
        var userId = _contextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Sid)?.Value;
        var user = await _userRepository.GetSingleAsync(u => u.Id == Guid.Parse(userId));

        if (user == null)
            throw new NotFoundException<User>();

        if (!String.IsNullOrEmpty(model.Fullname))
            user.Fullname = model.Fullname;

        if (!String.IsNullOrEmpty(model.Username))
        {
            if (await _userRepository.IsExistAsync(u => u.Username == model.Username))
                throw new UnavailableNameException();
            user.Username = model.Username;
        }

        if (!String.IsNullOrEmpty(model.Email))
        {
            if (await _userRepository.IsExistAsync(u => u.Email == model.Email))
                throw new UnavailableEmailException();
            user.Email = model.Email;
        }

        if (model.CurrentPassword is not null)
        {
            if (model.NewPassword is not null)
            {
                if (model.NewPasswordConfirm is not null)
                {
                    var response =
                        PasswordOperation.VerifyPassword(model.CurrentPassword, user.Salt, user.HashedPassword);
                    if (!response)
                        throw new IncorrectPasswordException();
                    if(model.NewPassword == model.NewPasswordConfirm)
                    {
                        var hashAndSalt = PasswordOperation.CalculateSha256Hash(model.NewPassword);
                        user.HashedPassword = hashAndSalt[0];
                        user.Salt = hashAndSalt[1];
                    }
                    else
                        throw new NotMatchingPasswordsException();
                }
                else
                    throw new Exception("Password must be at least 8 characters length.");
            }
            else
                throw new Exception("Password must be at least 8 characters length.");
        }
        
        await _userRepository.SaveAsync();
        return true;
    }

    public async Task UpdateRefreshToken(string refreshToken, User user, DateTime accessTokenExpires,
        int refreshTokenLifetime)
    {
        if (user == null)
            throw new NotFoundException<User>();
        user.RefreshToken = refreshToken;
        user.RefreshTokenExpires = accessTokenExpires.AddMinutes(refreshTokenLifetime);
        _userRepository.Update(user);
        await _userRepository.SaveAsync();
    }

    public async Task<bool> DeleteUserAsync(Guid id)
    {
        var user = await _userRepository.GetSingleAsync(u => u.Id == id);
        if (user == null)
            throw new NotFoundException<User>();
        var response = _userRepository.Remove(user);
        await _userRepository.SaveAsync();
        return response;
    }
}