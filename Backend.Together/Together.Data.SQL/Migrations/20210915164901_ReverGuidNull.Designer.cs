﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Together.Data.SQL;

namespace Together.Data.SQL.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20210915164901_ReverGuidNull")]
    partial class ReverGuidNull
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.9")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Together.Data.Models.CommentModel", b =>
                {
                    b.Property<Guid>("CommentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CommentDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("CommentDeleted")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CommentDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CommentLikes")
                        .HasColumnType("int");

                    b.Property<Guid>("PostId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("CommentId");

                    b.HasIndex("PostId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("Together.Data.Models.PostModel", b =>
                {
                    b.Property<Guid>("PostId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("PostDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("PostDeleted")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PostDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PostLikes")
                        .HasColumnType("int");

                    b.Property<int?>("PostShares")
                        .HasColumnType("int");

                    b.Property<Guid>("UserProfileId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("UserProfileModelUserProfileId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("PostId");

                    b.HasIndex("UserProfileModelUserProfileId");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("Together.Data.Models.ReplyModel", b =>
                {
                    b.Property<Guid>("ReplyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CommentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("ReplyDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ReplyDeleted")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ReplyDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ReplyLikes")
                        .HasColumnType("int");

                    b.HasKey("ReplyId");

                    b.HasIndex("CommentId");

                    b.ToTable("Replies");
                });

            modelBuilder.Entity("Together.Data.Models.UserAuthenticationModel", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.HasIndex("Email")
                        .IsUnique()
                        .HasFilter("[Email] IS NOT NULL");

                    b.ToTable("UsersAuthInfos");
                });

            modelBuilder.Entity("Together.Data.Models.UserProfileModel", b =>
                {
                    b.Property<Guid>("UserProfileId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("BirthdayDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ProfileDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserFriendsNumber")
                        .HasColumnType("int");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("UserPostsNumber")
                        .HasColumnType("int");

                    b.Property<string>("UserProfileImgBlobLink")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserProfileId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("UsersProfiles");
                });

            modelBuilder.Entity("Together.Data.Models.CommentModel", b =>
                {
                    b.HasOne("Together.Data.Models.PostModel", "Post")
                        .WithMany("PostComments")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Post");
                });

            modelBuilder.Entity("Together.Data.Models.PostModel", b =>
                {
                    b.HasOne("Together.Data.Models.UserProfileModel", "UserProfileModel")
                        .WithMany("UserPosts")
                        .HasForeignKey("UserProfileModelUserProfileId");

                    b.Navigation("UserProfileModel");
                });

            modelBuilder.Entity("Together.Data.Models.ReplyModel", b =>
                {
                    b.HasOne("Together.Data.Models.CommentModel", "Comment")
                        .WithMany("CommentReplies")
                        .HasForeignKey("CommentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Comment");
                });

            modelBuilder.Entity("Together.Data.Models.UserProfileModel", b =>
                {
                    b.HasOne("Together.Data.Models.UserAuthenticationModel", "UserAuthenticationModel")
                        .WithOne("UserProfileModel")
                        .HasForeignKey("Together.Data.Models.UserProfileModel", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserAuthenticationModel");
                });

            modelBuilder.Entity("Together.Data.Models.CommentModel", b =>
                {
                    b.Navigation("CommentReplies");
                });

            modelBuilder.Entity("Together.Data.Models.PostModel", b =>
                {
                    b.Navigation("PostComments");
                });

            modelBuilder.Entity("Together.Data.Models.UserAuthenticationModel", b =>
                {
                    b.Navigation("UserProfileModel");
                });

            modelBuilder.Entity("Together.Data.Models.UserProfileModel", b =>
                {
                    b.Navigation("UserPosts");
                });
#pragma warning restore 612, 618
        }
    }
}
