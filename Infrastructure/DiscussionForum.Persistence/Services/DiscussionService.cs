using System.Security.Claims;
using AutoMapper;
using DiscussionForum.Application.Abstractions.Services;
using DiscussionForum.Application.Abstractions.Storages;
using DiscussionForum.Application.DTOs.Discussion;
using DiscussionForum.Application.Exceptions;
using DiscussionForum.Application.Repositories;
using DiscussionForum.Application.Validators.DiscussionValidators;
using DiscussionForum.Domain.Entities;
using DiscussionForum.Domain.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace DiscussionForum.Persistence.Services;

public class DiscussionService : IDiscussionService
{
    private readonly IDiscussionRepository _discussionRepository;
    private readonly ICommunityRepository _communityRepository;
    private readonly ICommentRatingRepository _commentRatingRepository;
    private readonly IMapper _mapper;
    private readonly ILocalStorageService _storageService;

    public DiscussionService(IDiscussionRepository discussionRepository, IMapper mapper,
        ICommunityRepository communityRepository, ICommentRatingRepository commentRatingRepository, ILocalStorageService storageService)
    {
        _discussionRepository = discussionRepository;
        _mapper = mapper;
        _communityRepository = communityRepository;
        _commentRatingRepository = commentRatingRepository;
        _storageService = storageService;
    }

    public async Task<List<DiscussionListDto>> GetAllAsync(int skip, int take)
    {
        if (skip < 0 || take < 0) throw new ArgumentOutOfRangeException("skip or take cannot be negative");

        var discussions = await _discussionRepository
            .GetAll(skip, take, false, "Community", "Comments", "DiscussionRatings").ToListAsync();

        var dtos = _mapper.Map<List<DiscussionListDto>>(discussions);
        for (int i = 0; i < dtos.Count; i++)
        {
            dtos[i].CommentCount = discussions[i].Comments?.Count ?? 0;
            dtos[i].Rating = _calcRate(discussions[i].DiscussionRatings);
        }

        return dtos;
    }

    public async Task<List<DiscussionListDto>> GetAllOfCommunityAsync(Guid communityId, int skip, int take)
    {
        if (skip < 0 || take < 0) throw new ArgumentOutOfRangeException("skip or take cannot be negative");

        var discussions = await _discussionRepository
            .GetWhere(d => d.CommunityId == communityId, false, "Community", "Comments", "DiscussionRatings").Skip(skip)
            .Take(take).ToListAsync();
        var dtos = _mapper.Map<List<DiscussionListDto>>(discussions);
        for (int i = 0; i < dtos.Count; i++)
        {
            dtos[i].CommentCount = discussions[i].Comments?.Count ?? 0;
            dtos[i].Rating = _calcRate(discussions[i].DiscussionRatings);
        }

        return dtos;
    }

    public async Task<List<DiscussionListDto>> GetAllOfUserAsync(Guid userId, int skip, int take)
    {
        if (skip < 0 || take < 0) throw new ArgumentOutOfRangeException("skip or take cannot be negative");

        var discussions = await _discussionRepository
            .GetWhere(d => d.UserId == userId, false, "Comments", "DiscussionRatings").Skip(skip).Take(take)
            .ToListAsync();
        var dtos = _mapper.Map<List<DiscussionListDto>>(discussions);
        for (int i = 0; i < dtos.Count; i++)
        {
            dtos[i].CommentCount = discussions[i].Comments?.Count ?? 0;
            dtos[i].Rating = _calcRate(discussions[i].DiscussionRatings);
        }

        return dtos;
    }

    public async Task<DiscussionDetailDto> GetByIdAsync(Guid id)
    {
        var discussion =
            await _discussionRepository.GetByIdAsync(id, false, "User", "Community", "Comments", "DiscussionRatings");
        if (discussion == null)
            throw new NotFoundException<Discussion>();
        var dto = _mapper.Map<DiscussionDetailDto>(discussion);
        dto.Rating = _calcRate(discussion.DiscussionRatings);
        return dto;
    }

    public async Task<bool> CreateAsync(CreateDiscussionDto model)
    {
        var userId = _discussionRepository.GetUserId();

        var community = await _communityRepository.GetByIdAsync(model.CommunityId, true, "Members");

        if (community == null)
            throw new NotFoundException<Community>();

        if (community.Members.All(u => u.Id != userId))
            throw new Exception("Users can not create discussion in a community without joining.");

        var discussion = _mapper.Map<Discussion>(model);
        discussion.UserId = userId;
        discussion.CreatedAt = DateTime.UtcNow;
        if(model.Files != null)
            discussion.FilePaths = await _storageService.UploadAsync("assets/discussions/files", model.Files);

        var response = await _discussionRepository.AddAsync(discussion);
        await _discussionRepository.SaveAsync();
        return response;
    }

    public async Task<bool> UpdateAsync(UpdateDiscussionDto model)
    {
        var userId = _discussionRepository.GetUserId();

        var discussion = await _discussionRepository.GetWhere(d => d.Id == model.DiscussionId).Include(d => d.Community)
            .ThenInclude(c => c.AdminUsers).FirstOrDefaultAsync();

        if (discussion == null)
            throw new NotFoundException<Discussion>();

        if (discussion.UserId != userId && discussion.Community.AdminUsers.All(u => u.Id != userId))
            throw new UnauthorizedAccessException();

        _mapper.Map(model, discussion);

        if (model.Files != null)
            discussion.FilePaths = await _storageService.UploadAsync("assets/discussions/files", model.Files);
        
        await _discussionRepository.SaveAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var userId = _discussionRepository.GetUserId();

        var discussion = await _discussionRepository.GetByIdAsync(id);
        if (discussion == null)
            throw new NotFoundException<Discussion>();

        if (discussion.UserId != userId && discussion.Community.AdminUsers.All(u => u.Id != userId))
            throw new UnauthorizedAccessException();

        var temp = discussion.FilePaths;

        var response = _discussionRepository.Remove(discussion);
        if (response && temp != null)
        {
            foreach (var filePath in temp)
            {
                await _storageService.DeleteAsync(filePath);
            }
        }
        await _discussionRepository.SaveAsync();
        return response;
    }

    public async Task<bool> RateAsync(RateDiscussionDto model)
    {
        var userId = _discussionRepository.GetUserId();
        var discussion = await _discussionRepository.GetByIdAsync(model.DiscussionId, true, "DiscussionRatings");
        if (discussion == null) throw new NotFoundException<Discussion>();

        discussion.DiscussionRatings ??= new List<DiscussionRating>();

        var rating = await _commentRatingRepository.GetSingleAsync(r => r.UserId == userId);

        if (rating != null)
        {
            if (rating.Rate == model.Rate) _commentRatingRepository.Remove(rating);
            else rating.Rate = rating.Rate == Rates.Up ? Rates.Down : Rates.Up;
        }
        else
        {
            discussion.DiscussionRatings.Add(new DiscussionRating
            {
                Rate = model.Rate,
                UserId = userId,
                DiscussionId = model.DiscussionId
            });
        }

        await _discussionRepository.SaveAsync();
        return true;
    }

    private int _calcRate(IEnumerable<DiscussionRating>? ratings)
    {
        if (ratings == null) return 0;
        var rate = ratings.Sum(rating => rating.Rate == Rates.Up ? 1 : -1);
        return rate < 0 ? 0 : rate;
    }
}