using DiscussionForum.Application.DTOs.Community;

namespace DiscussionForum.Application.Abstractions.Services;

public interface ICommunityService
{
    Task<List<CommunityListDto>> GetAllAsync(int skip, int take);
    Task<CommunityDetailDto> GetByIdAsync(Guid id);
    Task<bool> CreateAsync(CreateCommunityDto model);
    Task<bool> UpdateAsync(UpdateCommunityDto model);
    Task<bool> DeleteAsync(Guid id);

    Task<bool> JoinCommunityAsync(Guid communityId);
    Task<bool> LeaveCommunityAsync(Guid communityId);

    Task<List<CommunityListDto>> GetJoinedCommunities();
    Task<List<CommunityListDto>> GetCreatedCommunities();
}