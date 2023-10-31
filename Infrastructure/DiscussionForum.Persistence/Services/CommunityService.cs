using System.Security.Claims;
using AutoMapper;
using DiscussionForum.Application.Abstractions.Services;
using DiscussionForum.Application.DTOs.Community;
using DiscussionForum.Application.Exceptions;
using DiscussionForum.Application.Repositories;
using DiscussionForum.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace DiscussionForum.Persistence.Services;

public class CommunityService : ICommunityService
{
    private readonly ICommunityRepository _communityRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _contextAccessor;

    public CommunityService(ICommunityRepository communityRepository, IMapper mapper, IUserRepository userRepository,
        IHttpContextAccessor contextAccessor)
    {
        _communityRepository = communityRepository;
        _mapper = mapper;
        _userRepository = userRepository;
        _contextAccessor = contextAccessor;
    }

    public async Task<List<CommunityListDto>> GetAllAsync(int take, int skip = 0)
    {
        if (skip < 0 || take < 0) throw new ArgumentOutOfRangeException("skip or take cannot be negative");

        var communities = await _communityRepository.GetAll(skip, take, false).ToListAsync();
        return _mapper.Map<List<CommunityListDto>>(communities);
    }

    public async Task<CommunityDetailDto> GetByIdAsync(Guid id)
    {
        var community = await _communityRepository.GetByIdAsync(id);
        if (community == null) throw new NotFoundException<Community>();
        return _mapper.Map<CommunityDetailDto>(community);
    }

    public async Task<bool> CreateAsync(CreateCommunityDto model)
    {
        var adminId = _contextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Sid)?.Value;
        if (adminId == null)
            throw new Exception("You need to log in to create a community");

        if (!await _userRepository.IsExistAsync(u => u.Id == Guid.Parse(adminId)))
            throw new NotFoundException<User>();

        if (await _communityRepository.IsExistAsync(c => c.Name == model.Name))
            throw new UnavailableNameException();


        var community = _mapper.Map<Community>(model);
        community.AdminId = Guid.Parse(adminId);
        var response = await _communityRepository.AddAsync(community);
        await _communityRepository.SaveAsync();
        return response;
    }

    public async Task<bool> UpdateAsync(UpdateCommunityDto model)
    {
        var userId = GetUserId();

        var community = await _communityRepository.GetByIdAsync(model.Id);
        if (community == null) throw new NotFoundException<Community>();

        if (userId != community.AdminId)
            throw new UnauthorizedAccessException();

        if (await _communityRepository.IsExistAsync(c => c.Name == model.Name))
            throw new UnavailableNameException();
        var res = _mapper.Map(model, community);

        await _communityRepository.SaveAsync();
        return res != null;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var userId = GetUserId();
        var community = await _communityRepository.GetByIdAsync(id);
        if (community == null) throw new NotFoundException<Community>();

        if (userId != community.AdminId)
            throw new UnauthorizedAccessException();

        var res = _communityRepository.Remove(community);
        await _communityRepository.SaveAsync();
        return res;
    }

    public async Task<bool> JoinCommunity(Guid communityId)
    {
        var userId = GetUserId();
        var community = await _communityRepository.Table.Include(c => c.Members)
            .SingleOrDefaultAsync(c => c.Id == communityId);

        if (community == null)
            throw new NotFoundException<Community>();

        if (community.Members.Any(m => m.Id == userId))
            throw new Exception("user is already a member");

        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null)
            throw new NotFoundException<User>();
        
        community.Members.Add(user);
        await _communityRepository.SaveAsync();
        return true;
    }


    private Guid GetUserId()
    {
        var userId = _contextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Sid)?.Value;
        if (userId == null)
            throw new Exception("Log in");
        return Guid.Parse(userId);
    }
}