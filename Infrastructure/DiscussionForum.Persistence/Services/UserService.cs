using System.Security.Claims;
using AutoMapper;
using DiscussionForum.Application.Abstractions.Services;
using DiscussionForum.Application.DTOs.User;
using DiscussionForum.Application.Repositories;
using DiscussionForum.Domain.Entities;
using DiscussionForum.Domain.Exceptions;
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

    public async Task<List<UserListDto>> GetAllUsersAsync()
    {
        return _mapper.Map<List<UserListDto>>(await _userRepository.GetAll().ToListAsync());
    }

    public async Task<UserDetailDto> GetUserAsync(string id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null)
            throw new UserNotFoundException();
        return _mapper.Map<UserDetailDto>(user);
    }

    public async Task<bool> CreateUserAsync(CreateUserDto model)
    {
        if (model.Password != model.PasswordConfirm)
            throw new Exception("Passwords does not match");
        
        var hashedPassword = PasswordOperation.CalculateSha256Hash(model.Password);

        if (_userRepository.Table.Any(u => u.Username == model.Username))
            throw new Exception("This username is not available.");
        
        if (_userRepository.Table.Any(c => c.Email == model.Email))
            throw new Exception("This email is on another account.");
        
        var user = new User()
        {
            Username = model.Username,
            Email = model.Email,
            HashedPassword = hashedPassword[0],
            Salt = hashedPassword[1]
        };

        if (model.Fullname is not null)
            user.Fullname = model.Fullname;
        
        var response = await _userRepository.AddAsync(user);
        await _userRepository.SaveAsync();
        return response;
    }

    public async Task<bool> UpdateUserAsync(UpdateUserDto model)
    {
        var userId = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var user = await _userRepository.GetSingleAsync(u => u.Id == Guid.Parse(userId));
        
        if (user == null)
            throw new UserNotFoundException();

        if (model.Fullname is not null)
            user.Fullname = model.Fullname;
        
        if (model.Username is not null)
            user.Username = model.Username;

        if (model.Email is not null)
            user.Email = model.Email;

        if (model.CurrentPassword is not null)
        {
            if (model.NewPassword is not null)
            {
                if (model.NewPasswordConfirm is not null)
                {
                    var response = PasswordOperation.VerifyPassword(model.CurrentPassword, user.Salt, user.HashedPassword);
                    if (response)
                    {
                        var hashAndSalt = PasswordOperation.CalculateSha256Hash(model.NewPassword);
                        user.HashedPassword = hashAndSalt[0];
                        user.Salt = hashAndSalt[1];
                    }
                    else
                        throw new Exception("Your password is wrong.");
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

    public async Task<bool> DeleteUserAsync(string id)
    {
        var user = await _userRepository.GetSingleAsync(u => u.Id == Guid.Parse(id));
        if (user == null)
            throw new UserNotFoundException();

        var response = _userRepository.Remove(user);
        await _userRepository.SaveAsync();
        return response;
    }
}