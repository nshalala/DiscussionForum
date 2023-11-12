using DiscussionForum.Application.Abstractions.Services;
using DiscussionForum.Application.DTOs.Community;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DiscussionForum.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class CommunityController:ControllerBase
{
    private readonly ICommunityService _communityService;

    public CommunityController(ICommunityService communityService)
    {
        _communityService = communityService;
    }

    [HttpGet]
    [Route("GetAllCommunities")]
    [AllowAnonymous]
    public async Task<IActionResult> GetAllCommunities(int skip = 0, int take = 50)
    {
        var communities = await _communityService.GetAllAsync(skip, take);
        return Ok(communities);
    }

    [HttpGet]
    [Route("GetCommunityById")]
    [AllowAnonymous]

    public async Task<IActionResult> GetCommunityById(Guid id)
    {
        var community = await _communityService.GetByIdAsync(id);
        return Ok(community);
    }

    [HttpPost]
    [Route("CreateCommunity")]
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

    [HttpPost]
    [Route("JoinCommunity")]
    public async Task<IActionResult> JoinCommunity(Guid communityId)
    {
        return Ok(await _communityService.JoinCommunityAsync(communityId));
    }
    
    [HttpPost]
    [Route("LeaveCommunity")]
    public async Task<IActionResult> LeaveCommunity(Guid communityId)
    {
        return Ok(await _communityService.LeaveCommunityAsync(communityId));
    }
    
    [HttpGet]
    [Route("GetJoinedCommunities")]
    public async Task<IActionResult> GetJoinedCommunities()
    {
        return Ok(await _communityService.GetJoinedCommunities());
    }
    
    [HttpGet]
    [Route("GetCreatedCommunities")]
    public async Task<IActionResult> GetCreatedCommunities()
    {
        return Ok(await _communityService.GetCreatedCommunities());
    }
}