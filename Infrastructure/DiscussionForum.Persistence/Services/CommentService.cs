using AutoMapper;
using DiscussionForum.Application.Abstractions.Services;
using DiscussionForum.Application.DTOs.Comment;
using DiscussionForum.Application.Exceptions;
using DiscussionForum.Application.Repositories;
using DiscussionForum.Domain.Entities;
using DiscussionForum.Domain.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace DiscussionForum.Persistence.Services;

public class CommentService : ICommentService
{
    private readonly ICommentRepository _commentRepository;
    private readonly IDiscussionRepository _discussionRepository;
    private readonly IMapper _mapper;

    public CommentService(ICommentRepository commentRepository, IMapper mapper,
        IDiscussionRepository discussionRepository, IHttpContextAccessor contextAccessor)
    {
        _commentRepository = commentRepository;
        _mapper = mapper;
        _discussionRepository = discussionRepository;
    }

    public async Task<List<CommentListDto>> GetAllOfDiscussionAsync(Guid discussionId)
    {
        var comments = await _commentRepository.GetWhere(c => c.DiscussionId == discussionId, false, "Children", "CommentRatings").ToListAsync();
        if (comments == null)
            throw new NotFoundException<Discussion>();
        var dtos = _mapper.Map<List<CommentListDto>>(comments);
        for (int i = 0; i < dtos.Count; i++)
        {
            dtos[i].Rating = _calcRate(comments[i].CommentRatings);
        }
        return dtos;
    }

    public async Task<List<CommentListDto>> GetAllOfUserAsync(Guid userId)
    {
        var comments = await _commentRepository.GetWhere(c => c.UserId == userId, false, "User", "Comment", "CommentRatings")
            .ToListAsync();
        if (comments == null)
            throw new NotFoundException<User>();
        var dtos = _mapper.Map<List<CommentListDto>>(comments);
        for (int i = 0; i < dtos.Count; i++)
        {
            dtos[i].Rating = _calcRate(comments[i].CommentRatings);
        }
        return dtos;
    }

    public async Task<CommentDetailDto> GetByIdAsync(Guid id)
    {
        var comment = await _commentRepository.GetByIdAsync(id, false, "User", "Comment", "CommentRatings");
        if (comment == null)
            throw new NotFoundException<Comment>();
        var dto = _mapper.Map<CommentDetailDto>(comment);
        dto.Rating = _calcRate(comment.CommentRatings);
        return dto;
    }

    public async Task<bool> CreateAsync(CreateCommentDto model)
    {
        var userId = _commentRepository.GetUserId();
        var discussion = await _discussionRepository.GetByIdAsync(model.DiscussionId);
        if (discussion == null)
            throw new NotFoundException<Discussion>();

        var comment = _mapper.Map<Comment>(model);
        comment.UserId = userId;

        var response = await _commentRepository.AddAsync(comment);
        await _commentRepository.SaveAsync();
        return response;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var userId = _commentRepository.GetUserId();

        var comment = await _commentRepository.GetByIdAsync(id);
        if (comment == null)
            throw new NotFoundException<Comment>();

        var discussion =
            await _discussionRepository.GetByIdAsync(comment.DiscussionId, true, "Community", "AdminUsers");
        if (userId != comment.UserId && userId != discussion.UserId &&
            discussion.Community.AdminUsers.All(u => u.Id != userId)) throw new UnauthorizedAccessException();

        _commentRepository.Remove(comment);
        await _commentRepository.SaveAsync();
        return true;
    }

    public async Task<bool> RateAsync(RateCommentDto model)
    {
        var userId = _commentRepository.GetUserId();
        var comment = await _commentRepository.GetByIdAsync(model.CommentId, true, "CommentRatings");
        comment.CommentRatings ??= new List<CommentRating>();
        comment.CommentRatings.Add(new CommentRating
        {
            Rate = model.Rate,
            UserId = userId
        });
        await _commentRepository.SaveAsync();
        return true;
    }

    private int _calcRate(IEnumerable<CommentRating>? ratings)
    {
        if (ratings == null) return 0;
        var rate = ratings.Sum(rating => rating.Rate == Rates.Up ? 1 : -1);
        return rate < 0 ? 0 : rate;
    }
}