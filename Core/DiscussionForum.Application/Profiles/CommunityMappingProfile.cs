using AutoMapper;
using DiscussionForum.Application.DTOs.Community;
using DiscussionForum.Domain.Entities;

namespace DiscussionForum.Application.Profiles;

public class CommunityMappingProfile : Profile
{
    public CommunityMappingProfile()
    {
        CreateMap<Community, CommunityDetailDto>()
            .ForMember(dto => dto.MemberCount, src => src.MapFrom(c => c.Members!.Count));
        CreateMap<Community, CommunityListDto>()
            .ForMember(dto => dto.MemberCount, src => src.MapFrom(c => c.Members!.Count));
        CreateMap<CreateCommunityDto, Community>();
        CreateMap<UpdateCommunityDto, Community>();
    }
}