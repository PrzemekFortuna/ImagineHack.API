﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using QuizITAPI.DB;

namespace QuizITAPI.Migrations
{
    [DbContext(typeof(QuizItContext))]
    [Migration("20181216030502_AddedDelBeh5")]
    partial class AddedDelBeh5
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("QuizITAPI.DB.Model.Answer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Content")
                        .IsRequired();

                    b.Property<bool>("IsCorrect");

                    b.Property<int>("QuestionId");

                    b.HasKey("Id");

                    b.HasIndex("QuestionId");

                    b.ToTable("Answers");
                });

            modelBuilder.Entity("QuizITAPI.DB.Model.Question", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Content")
                        .IsRequired();

                    b.Property<int>("QuizId");

                    b.Property<int>("Type");

                    b.Property<string>("UrlImage");

                    b.HasKey("Id");

                    b.HasIndex("QuizId");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("QuizITAPI.DB.Model.Quiz", b =>
                {
                    b.Property<int>("QuizId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Access");

                    b.Property<int>("AuthorId");

                    b.Property<string>("Description");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("QuizId");

                    b.HasIndex("AuthorId");

                    b.ToTable("Quizes");
                });

            modelBuilder.Entity("QuizITAPI.DB.Model.Room", b =>
                {
                    b.Property<int>("RoomId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("MaxUsersCount");

                    b.Property<string>("Name");

                    b.Property<int>("QuizId");

                    b.HasKey("RoomId");

                    b.HasIndex("QuizId");

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("QuizITAPI.DB.Model.RoomUser", b =>
                {
                    b.Property<int>("RoomId");

                    b.Property<int>("UserId");

                    b.Property<bool>("IsHost");

                    b.HasKey("RoomId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("RoomUsers");
                });

            modelBuilder.Entity("QuizITAPI.DB.Model.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("EMail")
                        .IsRequired();

                    b.Property<string>("Password")
                        .IsRequired();

                    b.Property<string>("Token");

                    b.HasKey("UserId");

                    b.HasIndex("EMail")
                        .IsUnique();

                    b.ToTable("Users");

                    b.HasData(
                        new { UserId = 1, EMail = "testEmail@gmail.com", Password = "Password" }
                    );
                });

            modelBuilder.Entity("QuizITAPI.DB.Model.Answer", b =>
                {
                    b.HasOne("QuizITAPI.DB.Model.Question", "Question")
                        .WithMany("Answers")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("QuizITAPI.DB.Model.Question", b =>
                {
                    b.HasOne("QuizITAPI.DB.Model.Quiz", "Quiz")
                        .WithMany("Questions")
                        .HasForeignKey("QuizId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("QuizITAPI.DB.Model.Quiz", b =>
                {
                    b.HasOne("QuizITAPI.DB.Model.User", "Author")
                        .WithMany("Quizes")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("QuizITAPI.DB.Model.Room", b =>
                {
                    b.HasOne("QuizITAPI.DB.Model.Quiz", "Quiz")
                        .WithMany("Rooms")
                        .HasForeignKey("QuizId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("QuizITAPI.DB.Model.RoomUser", b =>
                {
                    b.HasOne("QuizITAPI.DB.Model.Room", "Room")
                        .WithMany("RoomUsers")
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("QuizITAPI.DB.Model.User", "User")
                        .WithMany("RoomUsers")
                        .HasForeignKey("UserId");
                });
#pragma warning restore 612, 618
        }
    }
}