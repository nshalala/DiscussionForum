using System.Net.Mime;
using System.Security.Claims;
using AutoMapper;
using DiscussionForum.Application.Abstractions.Services;
using DiscussionForum.Application.DTOs.Comment;
using DiscussionForum.Application.Exceptions;
using DiscussionForum.Application.Repositories;
using DiscussionForum.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace DiscussionForum.Persistence.Services;

public class CommentService : ICommentService
{
    private readonly ICommentRepository _commentRepository;
    private readonly IDiscussionRepository _discussionRepository;
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly IMapper _mapper;

    public CommentService(ICommentRepository commentRepository, IMapper mapper,
        IDiscussionRepository discussionRepository, IHttpContextAccessor contextAccessor)
    {
        _commentRepository = commentRepository;
        _mapper = mapper;
        _discussionRepository = discussionRepository;
        _contextAccessor = contextAccessor;
    }

    public async Task<List<CommentListDto>> GetAllOfDiscussionAsync(Guid discussionId)
    {
        var query = _commentRepository.GetWhere(c => c.DiscussionId == discussionId, false);
        var comments = await query.Include(c => c.Children).ToListAsync();
        
        if (comments == null)
            throw new NotFoundException<Discussion>();

        var commentsList = _mapper.Map<List<CommentListDto>>(comments);
            
        return commentsList;
    }

    public async Task<List<CommentListDto>> GetAllOfUserAsync(Guid userId)
    {
        var query = _commentRepository.GetWhere(c => c.UserId == userId, false);
        var comments = await query.Include(c => c.Children).ToListAsync();

        if (comments == null)
            throw new NotFoundException<User>();

        return _mapper.Map<List<CommentListDto>>(comments);
    }

    public async Task<CommentDetailDto> GetByIdAsync(Guid id)
    {
        var comment = await _commentRepository.GetByIdAsync(id, false);
        if (comment == null)
            throw new NotFoundException<Comment>();
        return _mapper.Map<CommentDetailDto>(comment);
    }

    public async Task<bool> CreateAsync(CreateCommentDto model)
    {
        var userId = _contextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Sid)?.Value;
        if (userId == null)
            throw new Exception("Log in to comment");
        var discussion = await _discussionRepository.GetByIdAsync(model.DiscussionId);
        if (discussion == null)
            throw new NotFoundException<Discussion>();

        var comment = _mapper.Map<Comment>(model);
        comment.UserId = Guid.Parse(userId);
        
        var response = await _commentRepository.AddAsync(comment);
        await _commentRepository.SaveAsync();
        return response;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var comment = await _commentRepository.GetByIdAsync(id);
        if (comment == null)
            throw new NotFoundException<Comment>();

        _commentRepository.Remove(comment);
        await _commentRepository.SaveAsync();
        return true;
    }
}