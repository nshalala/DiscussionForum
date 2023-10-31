using DiscussionForum.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DiscussionForum.Persistence.DbConfigurations;

public class DiscussionConfiguration : IEntityTypeConfiguration<Discussion>
{
    public void Configure(EntityTypeBuilder<Discussion> builder)
    {
        builder.Property(d => d.Title)
            .IsRequired()
            .HasMaxLength(200);
        builder.Property(d => d.Description)
            .HasMaxLength(500);
        builder.Property(d => d.Rating)
            .HasDefaultValue(0);
        builder.HasOne(d => d.Community)
            .WithMany(c => c.Discussions)
            .HasForeignKey(d => d.CommunityId);
        builder.HasOne(d => d.User)
            .WithMany(u => u.Discussions)
            .HasForeignKey(d => d.UserId);
        builder.HasMany(d => d.Comments)
            .WithOne(c => c.Discussion)
            .HasForeignKey(c => c.DiscussionId);
    }
}