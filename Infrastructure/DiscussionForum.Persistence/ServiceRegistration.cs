using DiscussionForum.Application.Abstractions.Services;
using DiscussionForum.Application.Abstractions.Storages;
using DiscussionForum.Application.Abstractions.Token;
using DiscussionForum.Application.Profiles;
using DiscussionForum.Application.Repositories;
using DiscussionForum.Domain.Entities;
using DiscussionForum.Infrastructure.Services.Storages;
using DiscussionForum.Infrastructure.Services.Token;
using DiscussionForum.Persistence.Contexts;
using DiscussionForum.Persistence.Repositories;
using DiscussionForum.Persistence.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DiscussionForum.Persistence;

public static class ServiceRegistration
{
    public static void AddPersistenceServices(this IServiceCollection services)
    {
        services.AddDbContext<DiscussionForumDbContext>(opt => { opt.UseNpgsql(Configuration.ConnectionString); });

        services.AddHttpContextAccessor();
        services.AddAutoMapper(typeof(UserMappingProfile).Assembly);

        services.AddScoped<ITokenHandler, TokenHandler>();
        services.AddScoped<ILocalStorageService, LocalStorageService>();

        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ICommunityService, CommunityService>();
        services.AddScoped<IDiscussionService, DiscussionService>();
        services.AddScoped<ICommentService, CommentService>();
        services.AddScoped<ICommentRatingRepository, CommentRatingRepository>();
        services.AddScoped<IDiscussionRatingRepository, DiscussionRatingRepository>();

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ICommunityRepository, CommunityRepository>();
        services.AddScoped<IDiscussionRepository, DiscussionRepository>();
        services.AddScoped<ICommentRepository, CommentRepository>();
    }
}