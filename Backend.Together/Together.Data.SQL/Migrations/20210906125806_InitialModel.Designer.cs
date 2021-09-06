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
    [Migration("20210906125806_InitialModel")]
    partial class InitialModel
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

                    b.Property<string>("CommentDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CommentLikes")
                        .HasColumnType("int");

                    b.Property<bool>("IsCommentDeleted")
                        .HasColumnType("bit");

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

                    b.Property<bool>("IsPostDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime>("PostDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("PostDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PostLikes")
                        .HasColumnType("int");

                    b.Property<int>("PostShares")
                        .HasColumnType("int");

                    b.HasKey("PostId");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("Together.Data.Models.ReplyModel", b =>
                {
                    b.Property<Guid>("ReplyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CommentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsReplyDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime>("ReplyDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ReplyDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ReplyLikes")
                        .HasColumnType("int");

                    b.HasKey("ReplyId");

                    b.HasIndex("CommentId");

                    b.ToTable("Replies");
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

            modelBuilder.Entity("Together.Data.Models.ReplyModel", b =>
                {
                    b.HasOne("Together.Data.Models.CommentModel", "Comment")
                        .WithMany("CommentReplies")
                        .HasForeignKey("CommentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Comment");
                });

            modelBuilder.Entity("Together.Data.Models.CommentModel", b =>
                {
                    b.Navigation("CommentReplies");
                });

            modelBuilder.Entity("Together.Data.Models.PostModel", b =>
                {
                    b.Navigation("PostComments");
                });
#pragma warning restore 612, 618
        }
    }
}
