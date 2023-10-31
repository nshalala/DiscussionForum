using AutoMapper;
using DiscussionForum.Application.DTOs.Comment;
using DiscussionForum.Domain.Entities;

namespace DiscussionForum.Application.Profiles;

public class CommentMappingProfile : Profile
{
    public CommentMappingProfile()
    {
        CreateMap<CreateCommentDto, Comment>();
        CreateMap<Comment, CommentListDto>();
        CreateMap<Comment, CommentDetailDto>();
    }
}