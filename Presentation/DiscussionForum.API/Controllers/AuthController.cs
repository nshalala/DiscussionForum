using DiscussionForum.Application.Abstractions.Services;
using DiscussionForum.Application.DTOs.Auth;
using Microsoft.AspNetCore.Mvc;

namespace DiscussionForum.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost]
    public async Task<IActionResult> Login([FromForm] LoginDto dto)
    {
        return Ok(await _authService.LoginAsync(dto));
    }
}