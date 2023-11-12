using DiscussionForum.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DiscussionForum.Persistence.DbConfigurations;

public class DiscussionRatingConfiguration : IEntityTypeConfiguration<DiscussionRating>
{
    public void Configure(EntityTypeBuilder<DiscussionRating> builder)
    {
        builder.HasOne(dr => dr.User)
            .WithMany(u => u.DiscussionRatings)
            .HasForeignKey(dr => dr.UserId);
        builder.HasOne(dr => dr.Discussion)
            .WithMany(d => d.DiscussionRatings)
            .HasForeignKey(dr => dr.DiscussionId);
    }
}