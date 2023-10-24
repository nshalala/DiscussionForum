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
        builder.HasOne(c => c.Admin)
            .WithMany(u => u.CommunitiesAsAdmin)
            .HasForeignKey(c => c.AdminId);
        builder.HasMany(c => c.Members)
            .WithMany(u => u.CommunitiesAsMember);
    }
}