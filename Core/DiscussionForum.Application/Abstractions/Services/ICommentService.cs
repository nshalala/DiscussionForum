using DiscussionForum.Application.DTOs.Comment;

namespace DiscussionForum.Application.Abstractions.Services;

public interface ICommentService
{
    Task<List<CommentListDto>> GetAllOfDiscussionAsync(Guid discussionId);
    Task<List<CommentListDto>> GetAllOfUserAsync(Guid userId);
    Task<CommentDetailDto> GetByIdAsync(Guid id);
    Task<bool> CreateAsync(CreateCommentDto model);
    Task<bool> DeleteAsync(Guid id);
}