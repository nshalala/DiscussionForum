using DiscussionForum.Application.Abstractions.Services;
using DiscussionForum.Application.DTOs.Comment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DiscussionForum.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CommentController : ControllerBase
{
    private readonly ICommentService _commentService;

    public CommentController(ICommentService commentService)
    {
        _commentService = commentService;
    }

    [HttpGet("id")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var comment = await _commentService.GetByIdAsync(id);
        return Ok(comment);
    }

    [HttpGet("discussionId")]
    public async Task<IActionResult> GetAllOfDiscussion(Guid discussionId)
    {
        var comments = await _commentService.GetAllOfDiscussionAsync(discussionId);
        return Ok(comments);
    }

    [HttpGet("userId")]
    public async Task<IActionResult> GetAllOfUser(Guid userId)
    {
        var comments = await _commentService.GetAllOfUserAsync(userId);
        return Ok(comments);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateComment([FromBody] CreateCommentDto dto)
    {
        await _commentService.CreateAsync(dto);
        return Ok();
    }

    [HttpDelete]
    [Authorize]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _commentService.DeleteAsync(id);
        return Ok();
    }
}
