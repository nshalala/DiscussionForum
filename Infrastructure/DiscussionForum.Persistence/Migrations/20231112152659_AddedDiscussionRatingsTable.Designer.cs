﻿// <auto-generated />
using System;
using DiscussionForum.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DiscussionForum.Persistence.Migrations
{
    [DbContext(typeof(DiscussionForumDbContext))]
    [Migration("20231112152659_AddedDiscussionRatingsTable")]
    partial class AddedDiscussionRatingsTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CommunityUser", b =>
                {
                    b.Property<Guid>("AdminUsersId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("CommunitiesAsAdminId")
                        .HasColumnType("uuid");

                    b.HasKey("AdminUsersId", "CommunitiesAsAdminId");

                    b.HasIndex("CommunitiesAsAdminId");

                    b.ToTable("CommunityUser");
                });

            modelBuilder.Entity("CommunityUser1", b =>
                {
                    b.Property<Guid>("CommunitiesAsMemberId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("MembersId")
                        .HasColumnType("uuid");

                    b.HasKey("CommunitiesAsMemberId", "MembersId");

                    b.HasIndex("MembersId");

                    b.ToTable("CommunityUser1");
                });

            modelBuilder.Entity("DiscussionForum.Domain.Entities.Comment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("DiscussionId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("ParentId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("DiscussionId");

                    b.HasIndex("ParentId");

                    b.HasIndex("UserId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("DiscussionForum.Domain.Entities.CommentRating", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CommentId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("CommentId");

                    b.HasIndex("UserId");

                    b.ToTable("CommentRating");
                });

            modelBuilder.Entity("DiscussionForum.Domain.Entities.Community", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)");

                    b.HasKey("Id");

                    b.ToTable("Communities");
                });

            modelBuilder.Entity("DiscussionForum.Domain.Entities.Discussion", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CommunityId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("CommunityId");

                    b.HasIndex("UserId");

                    b.ToTable("Discussions");
                });

            modelBuilder.Entity("DiscussionForum.Domain.Entities.DiscussionRating", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("DiscussionId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("DiscussionId");

                    b.HasIndex("UserId");

                    b.ToTable("DiscussionRatings");
                });

            modelBuilder.Entity("DiscussionForum.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("character varying(150)");

                    b.Property<string>("Fullname")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("HashedPassword")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("text");

                    b.Property<DateTime?>("RefreshTokenExpires")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Role")
                        .HasColumnType("integer");

                    b.Property<string>("Salt")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("CommunityUser", b =>
                {
                    b.HasOne("DiscussionForum.Domain.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("AdminUsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DiscussionForum.Domain.Entities.Community", null)
                        .WithMany()
                        .HasForeignKey("CommunitiesAsAdminId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CommunityUser1", b =>
                {
                    b.HasOne("DiscussionForum.Domain.Entities.Community", null)
                        .WithMany()
                        .HasForeignKey("CommunitiesAsMemberId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DiscussionForum.Domain.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("MembersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DiscussionForum.Domain.Entities.Comment", b =>
                {
                    b.HasOne("DiscussionForum.Domain.Entities.Discussion", "Discussion")
                        .WithMany("Comments")
                        .HasForeignKey("DiscussionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DiscussionForum.Domain.Entities.Comment", "Parent")
                        .WithMany("Children")
                        .HasForeignKey("ParentId");

                    b.HasOne("DiscussionForum.Domain.Entities.User", "User")
                        .WithMany("Comments")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Discussion");

                    b.Navigation("Parent");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DiscussionForum.Domain.Entities.CommentRating", b =>
                {
                    b.HasOne("DiscussionForum.Domain.Entities.Comment", "Comment")
                        .WithMany("CommentRatings")
                        .HasForeignKey("CommentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DiscussionForum.Domain.Entities.User", "User")
                        .WithMany("CommentRatings")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Comment");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DiscussionForum.Domain.Entities.Discussion", b =>
                {
                    b.HasOne("DiscussionForum.Domain.Entities.Community", "Community")
                        .WithMany("Discussions")
                        .HasForeignKey("CommunityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DiscussionForum.Domain.Entities.User", "User")
                        .WithMany("Discussions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Community");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DiscussionForum.Domain.Entities.DiscussionRating", b =>
                {
                    b.HasOne("DiscussionForum.Domain.Entities.Discussion", "Discussion")
                        .WithMany("DiscussionRatings")
                        .HasForeignKey("DiscussionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DiscussionForum.Domain.Entities.User", "User")
                        .WithMany("DiscussionRatings")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Discussion");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DiscussionForum.Domain.Entities.Comment", b =>
                {
                    b.Navigation("Children");

                    b.Navigation("CommentRatings");
                });

            modelBuilder.Entity("DiscussionForum.Domain.Entities.Community", b =>
                {
                    b.Navigation("Discussions");
                });

            modelBuilder.Entity("DiscussionForum.Domain.Entities.Discussion", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("DiscussionRatings");
                });

            modelBuilder.Entity("DiscussionForum.Domain.Entities.User", b =>
                {
                    b.Navigation("CommentRatings");

                    b.Navigation("Comments");

                    b.Navigation("DiscussionRatings");

                    b.Navigation("Discussions");
                });
#pragma warning restore 612, 618
        }
    }
}
