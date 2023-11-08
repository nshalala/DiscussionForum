using DiscussionForum.Application.Abstractions.Services;
using DiscussionForum.Application.DTOs.Community;
using Microsoft.AspNetCore.Mvc;

namespace DiscussionForum.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CommunityController:ControllerBase
{
    private readonly ICommunityService _communityService;

    public CommunityController(ICommunityService communityService)
    {
        _communityService = communityService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCommunities(int skip, int take)
    {
        var communities = await _communityService.GetAllAsync(skip, take);
        return Ok(communities);
    }

    [HttpGet("id")]
    public async Task<IActionResult> GetCommunity(Guid id)
    {
        var community = await _communityService.GetByIdAsync(id);
        return Ok(community);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCommunity([FromBody] CreateCommunityDto dto)
    {
        var response = await _communityService.CreateAsync(dto);
        return Ok(response);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateCommunity([FromBody] UpdateCommunityDto dto)
    {
        var response = await _communityService.UpdateAsync(dto);
        return Ok(response);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteCommunity([FromBody] Guid id)
    {
        var response = await _communityService.DeleteAsync(id);
        return Ok(response);
    }

    // [HttpPost]
    // public async Task<IActionResult> JoinCommunity(Guid communityId)
    // {
    //     return Ok(await _communityService.JoinCommunityAsync(communityId));
    // }
    //
    // [HttpPost]
    // public async Task<IActionResult> LeaveCommunity(Guid communityId)
    // {
    //     return Ok(await _communityService.LeaveCommunityAsync(communityId));
    // }
}