using DiscussionForum.Application.DTOs.Discussion;
using DiscussionForum.Application.Validators.DiscussionValidators;
using DiscussionForum.Domain.Enums;

namespace DiscussionForum.Application.Abstractions.Services;

public interface IDiscussionService
{
    Task<List<DiscussionListDto>> GetAllAsync(int skip, int take);
    Task<List<DiscussionListDto>> GetAllOfCommunityAsync(Guid communityId, int skip, int take);
    Task<DiscussionDetailDto> GetByIdAsync(Guid id);
    Task<bool> CreateAsync(CreateDiscussionDto model);
    Task<bool> UpdateAsync(UpdateDiscussionDto model);
    Task<bool> DeleteAsync(Guid id);
    Task<bool> RateAsync(RateDiscussionDto model);
}