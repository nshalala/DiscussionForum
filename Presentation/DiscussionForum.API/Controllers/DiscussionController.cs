using DiscussionForum.Application.Abstractions.Services;
using DiscussionForum.Application.DTOs.Discussion;
using DiscussionForum.Application.Validators.DiscussionValidators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DiscussionForum.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DiscussionController:ControllerBase
{
    private readonly IDiscussionService _discussionService;

    public DiscussionController(IDiscussionService discussionService)
    {
        _discussionService = discussionService;
    }

    [HttpGet("id")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var discussions = await _discussionService.GetByIdAsync(id);
        return Ok(discussions);
    }

    [HttpGet("communityId")]
    public async Task<IActionResult> GetAllOfCommunity(Guid communityId, int skip, int take)
    {
        var discussions = await _discussionService.GetAllOfCommunityAsync(communityId, skip, take);
        return Ok(discussions);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll(int skip, int take)
    {
        var discussions = await _discussionService.GetAllAsync(skip, take);
        return Ok(discussions);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Create([FromBody] CreateDiscussionDto dto)
    {
        await _discussionService.CreateAsync(dto);
        return Ok();
    }

    [HttpPut]
    [Authorize]
    public async Task<IActionResult> Update([FromBody] UpdateDiscussionDto dto)
    {
        await _discussionService.UpdateAsync(dto);
        return Ok();
    }

    [HttpDelete("id")]
    [Authorize]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _discussionService.DeleteAsync(id);
        return Ok();
    }
}