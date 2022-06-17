﻿// <auto-generated />
using System;
using Animou.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Animou.Data.Migrations
{
    [DbContext(typeof(AnimouContext))]
    [Migration("20220609171936_TestingAnimou")]
    partial class TestingAnimou
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("Animou")
                .HasAnnotation("ProductVersion", "6.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Animou.Business.Models.Anime", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("varchar(36)");

                    b.Property<int>("AnimeId")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(36)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Animes", "Animou");
                });

            modelBuilder.Entity("Animou.Business.Models.Comment", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(36)");

                    b.Property<string>("AnimeId")
                        .IsRequired()
                        .HasColumnType("varchar(36)");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(36)");

                    b.HasKey("Id");

                    b.ToTable("Comments", "Animou");
                });

            modelBuilder.Entity("Animou.Business.Models.Deslike", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(36)");

                    b.Property<string>("CommentId")
                        .IsRequired()
                        .HasColumnType("varchar(36)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(36)");

                    b.HasKey("Id");

                    b.ToTable("Deslikes", "Animou");
                });

            modelBuilder.Entity("Animou.Business.Models.Like", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(36)");

                    b.Property<string>("CommentId")
                        .IsRequired()
                        .HasColumnType("varchar(36)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(36)");

                    b.HasKey("Id");

                    b.ToTable("likes", "Animou");
                });

            modelBuilder.Entity("Animou.Business.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("varchar(36)");

                    b.Property<string>("Avatar")
                        .HasColumnType("varchar(100)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("Watched")
                        .HasColumnType("int");

                    b.Property<int>("Watching")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Users", "Animou");
                });

            modelBuilder.Entity("Animou.Business.Models.Anime", b =>
                {
                    b.HasOne("Animou.Business.Models.User", "User")
                        .WithMany("Animes")
                        .HasForeignKey("UserId")
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Animou.Business.Models.Comment", b =>
                {
                    b.HasOne("Animou.Business.Models.Anime", "Anime")
                        .WithMany("Comments")
                        .HasForeignKey("Id")
                        .IsRequired();

                    b.HasOne("Animou.Business.Models.User", "User")
                        .WithMany("Comments")
                        .HasForeignKey("Id")
                        .IsRequired();

                    b.Navigation("Anime");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Animou.Business.Models.Deslike", b =>
                {
                    b.HasOne("Animou.Business.Models.Comment", "Comment")
                        .WithMany("Deslikes")
                        .HasForeignKey("Id")
                        .IsRequired();

                    b.HasOne("Animou.Business.Models.User", "User")
                        .WithMany("Deslikes")
                        .HasForeignKey("Id")
                        .IsRequired();

                    b.Navigation("Comment");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Animou.Business.Models.Like", b =>
                {
                    b.HasOne("Animou.Business.Models.Comment", "Comment")
                        .WithMany("Likes")
                        .HasForeignKey("Id")
                        .IsRequired();

                    b.HasOne("Animou.Business.Models.User", "User")
                        .WithMany("Likes")
                        .HasForeignKey("Id")
                        .IsRequired();

                    b.Navigation("Comment");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Animou.Business.Models.Anime", b =>
                {
                    b.Navigation("Comments");
                });

            modelBuilder.Entity("Animou.Business.Models.Comment", b =>
                {
                    b.Navigation("Deslikes");

                    b.Navigation("Likes");
                });

            modelBuilder.Entity("Animou.Business.Models.User", b =>
                {
                    b.Navigation("Animes");

                    b.Navigation("Comments");

                    b.Navigation("Deslikes");

                    b.Navigation("Likes");
                });
#pragma warning restore 612, 618
        }
    }
}
