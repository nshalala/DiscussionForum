using AutoMapper;
using DiscussionForum.Application.DTOs.Community;
using DiscussionForum.Application.DTOs.User;
using DiscussionForum.Domain.Entities;

namespace DiscussionForum.Application.Profiles;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<User, UserListDto>();
        CreateMap<User, UserDetailDto>();
        CreateMap<User, AdminUsersListDto>();
        CreateMap<RegisterUserDto, User>();
    }
}