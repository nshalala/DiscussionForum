using DiscussionForum.Application.Abstractions.Services;
using DiscussionForum.Application.DTOs.User;
using DiscussionForum.Application.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DiscussionForum.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }
    [HttpGet("id")]
    public async Task<IActionResult> GetUser(string id)
    {
        var user = await _userService.GetUserAsync(id);
        return Ok(user);
    }

    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        var users = await _userService.GetAllUsersAsync();
        return Ok(users);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateUser(CreateUserDto model)
    {
        var response = await _userService.CreateUserAsync(model);
        return Ok(response);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateUser(UpdateUserDto model)
    {
        var response = await _userService.UpdateUserAsync(model);
        return Ok(response);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteUser(string id)
    {
        var response = await _userService.DeleteUserAsync(id);
        return Ok(response);
    }
}