using AutoMapper;
using DiscussionForum.Application.DTOs.Community;
using DiscussionForum.Domain.Entities;

namespace DiscussionForum.Application.Profiles;

public class CommunityMappingProfile : Profile
{
    public CommunityMappingProfile()
    {
        CreateMap<Community, CommunityDetailDto>();
        CreateMap<Community, CommunityListDto>();
        CreateMap<CreateCommunityDto, Community>();
        CreateMap<UpdateCommunityDto, Community>();
    }
}