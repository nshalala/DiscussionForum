using DiscussionForum.Application.Abstractions.Services;
using DiscussionForum.Application.DTOs.User;
using DiscussionForum.Application.Repositories;
using Microsoft.AspNetCore.Authorization;
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
    public async Task<IActionResult> GetUser(Guid id)
    {
        var user = await _userService.GetUserAsync(id);
        return Ok(user);
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetUsers(int skip = 0, int take = 50)
    {
        var users = await _userService.GetAllUsersAsync(skip, take);
        return Ok(users);
    }

    [HttpPost]
    public async Task<IActionResult> RegisterUser(RegisterUserDto model)
    {
        var response = await _userService.RegisterUserAsync(model);
        return Ok(response);
    }

    [HttpPut]
    [Authorize]
    public async Task<IActionResult> UpdateUser([FromForm] UpdateUserDto model)
    {
        var response = await _userService.UpdateUserAsync(model);
        return Ok(response);
    }

    [HttpDelete]
    [Authorize]
    public async Task<IActionResult> DeleteUser(Guid id)
    {
        var response = await _userService.DeleteUserAsync(id);
        return Ok(response);
    }
}