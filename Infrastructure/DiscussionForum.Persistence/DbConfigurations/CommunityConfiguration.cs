using DiscussionForum.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DiscussionForum.Persistence.DbConfigurations;

public class CommunityConfiguration:IEntityTypeConfiguration<Community>
{
    public void Configure(EntityTypeBuilder<Community> builder)
    {
        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(30);
        builder.HasMany(c => c.AdminUsers)
            .WithMany(u => u.CommunitiesAsAdmin);
        builder.HasMany(c => c.Members)
            .WithMany(u => u.CommunitiesAsMember);
        builder.HasMany(c => c.Discussions)
            .WithOne(d => d.Community)
            .HasForeignKey(d => d.CommunityId);
    }
}