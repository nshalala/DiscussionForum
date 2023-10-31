using System.Security.Claims;
using AutoMapper;
using DiscussionForum.Application.Abstractions.Services;
using DiscussionForum.Application.Exceptions;
using DiscussionForum.Application.Repositories;
using DiscussionForum.Application.Validators.DiscussionValidators;
using DiscussionForum.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace DiscussionForum.Persistence.Services;

public class DiscussionService : IDiscussionService
{
    private readonly IDiscussionRepository _discussionRepository;
    private readonly ICommunityRepository _communityRepository;
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly IMapper _mapper;

    public DiscussionService(IDiscussionRepository discussionRepository, IMapper mapper,
        ICommunityRepository communityRepository, IHttpContextAccessor contextAccessor)
    {
        _discussionRepository = discussionRepository;
        _mapper = mapper;
        _communityRepository = communityRepository;
        _contextAccessor = contextAccessor;
    }

    public async Task<List<DiscussionListDto>> GetAllAsync(int skip, int take)
    {
        if (skip < 0 || take < 0) throw new ArgumentOutOfRangeException("skip or take cannot be negative");

        var discussions = _discussionRepository.GetAll(skip, take, false);
        return _mapper.Map<List<DiscussionListDto>>(await discussions.ToListAsync());
    }

    public async Task<List<DiscussionListDto>> GetAllOfCommunityAsync(Guid communityId, int skip, int take)
    {
        if (skip < 0 || take < 0) throw new ArgumentOutOfRangeException("skip or take cannot be negative");

        var discussions = _discussionRepository.GetWhere(d => d.CommunityId == communityId).Skip(skip).Take(take);
        return _mapper.Map<List<DiscussionListDto>>(await discussions.ToListAsync());
    }

    public async Task<DiscussionDetailDto> GetByIdAsync(Guid id)
    {
        var discussion = await _discussionRepository.GetByIdAsync(id);
        if (discussion == null)
            throw new NotFoundException<Discussion>();

        return _mapper.Map<DiscussionDetailDto>(discussion);
    }

    public async Task<bool> CreateAsync(CreateDiscussionDto model)
    {
        var userId = _contextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Sid)?.Value;
        if (userId == null)
            throw new Exception("You need to log in to create a discussion");

        if (!await _communityRepository.IsExistAsync(c => c.Id == model.CommunityId))
            throw new NotFoundException<Community>();

        var discussion = _mapper.Map<Discussion>(model);
        discussion.UserId = Guid.Parse(userId);

        var response = await _discussionRepository.AddAsync(discussion);
        await _discussionRepository.SaveAsync();
        return response;
    }

    public async Task<bool> UpdateAsync(UpdateDiscussionDto model)
    {
        var userId = _contextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Sid)?.Value;
        if (userId == null)
            throw new Exception("You need to log in first");

        if (Guid.Parse(userId) != model.UserId)
            throw new UnauthorizedAccessException();

        var discussion = await _discussionRepository.GetByIdAsync(model.Id);

        if (discussion == null)
            throw new NotFoundException<Discussion>();

        _mapper.Map(model, discussion);
        await _discussionRepository.SaveAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var userId = _contextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Sid)?.Value;
        if (userId == null)
            throw new Exception("You need to log in first");

        var discussion = await _discussionRepository.GetByIdAsync(id);
        if (discussion == null)
            throw new NotFoundException<Discussion>();

        if (discussion.UserId != Guid.Parse(userId))
            throw new UnauthorizedAccessException();

        var response = _discussionRepository.Remove(discussion);
        await _discussionRepository.SaveAsync();
        return response;
    }
}