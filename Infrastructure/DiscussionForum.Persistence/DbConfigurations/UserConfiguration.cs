using DiscussionForum.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DiscussionForum.Persistence.DbConfigurations;

public class UserConfiguration:IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(u => u.Username).HasMaxLength(30);
        builder.Property(u => u.Email).HasMaxLength(150);
        builder.Property(u => u.Fullname).HasMaxLength(100);
        builder.HasMany(u => u.CommunitiesAsAdmin)
            .WithOne(c => c.Admin)
            .HasForeignKey(c => c.AdminId);
        builder.HasMany(u => u.CommunitiesAsMember)
            .WithMany(c => c.Members);
    }
}