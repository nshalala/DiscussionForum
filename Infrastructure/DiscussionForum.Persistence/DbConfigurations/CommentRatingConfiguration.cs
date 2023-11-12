using DiscussionForum.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DiscussionForum.Persistence.DbConfigurations;

public class CommentRatingConfiguration : IEntityTypeConfiguration<CommentRating>
{
    public void Configure(EntityTypeBuilder<CommentRating> builder)
    {
        builder.HasOne(cr => cr.User)
            .WithMany(u => u.CommentRatings)
            .HasForeignKey(cr => cr.UserId);
        builder.HasOne(cr => cr.Comment)
            .WithMany(c => c.CommentRatings)
            .HasForeignKey(cr => cr.CommentId);
    }
}