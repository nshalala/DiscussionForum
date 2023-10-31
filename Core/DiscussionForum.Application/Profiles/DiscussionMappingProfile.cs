using AutoMapper;
using DiscussionForum.Application.Validators.DiscussionValidators;
using DiscussionForum.Domain.Entities;

namespace DiscussionForum.Application.Profiles;

public class DiscussionMappingProfile : Profile
{
    public DiscussionMappingProfile()
    {
        CreateMap<CreateDiscussionDto, Discussion>();
        CreateMap<UpdateDiscussionDto, Discussion>();
        CreateMap<Discussion, DiscussionListDto>();
        CreateMap<Discussion, DiscussionDetailDto>();
    }
}