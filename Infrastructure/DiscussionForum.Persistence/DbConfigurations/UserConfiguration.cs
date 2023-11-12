using DiscussionForum.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DiscussionForum.Persistence.DbConfigurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(u => u.Username).HasMaxLength(30);
        builder.Property(u => u.Email).HasMaxLength(150);
        builder.Property(u => u.Fullname).HasMaxLength(100);
        
        builder.HasMany(u => u.DiscussionRatings)
            .WithOne(dr => dr.User)
            .HasForeignKey(dr => dr.UserId);
        builder.HasMany(u => u.CommentRatings)
            .WithOne(cr => cr.User)
            .HasForeignKey(cr => cr.UserId);
        builder.HasMany(u => u.CommunitiesAsAdmin)
            .WithMany(c => c.AdminUsers);
        builder.HasMany(u => u.CommunitiesAsMember)
            .WithMany(c => c.Members);
        builder.HasMany(u => u.Discussions)
            .WithOne(d => d.User)
            .HasForeignKey(d => d.UserId);
        builder.HasMany(u => u.Comments)
            .WithOne(c => c.User)
            .HasForeignKey(c => c.UserId);
    }
}