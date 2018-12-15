using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QuizITAPI.DB.Model;

namespace QuizITAPI.DB
{
    public class QuizItContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Quiz> Quizes { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<RoomUser> RoomUsers { get; set; }

        private const string ConnString = "Server=tcp:quizitdb.database.windows.net,1433;" +
            "Initial Catalog = QuizIT;" +
            "Persist Security Info=False;" +
            "User ID = quizitadmin;" +
            "Password=Password1234;" +
            "MultipleActiveResultSets=False;" +
            "Encrypt=True;" +
            "TrustServerCertificate=False;" +
            "Connection Timeout = 30;";


        public QuizItContext() : base(new DbContextOptionsBuilder().UseSqlServer(ConnString).Options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(e =>
            {
                e.HasIndex(u => u.EMail).IsUnique();
                e.HasMany<RoomUser>()
                    .WithOne(RoomUser => RoomUser.User)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Room>(e =>
            {
                e.HasMany<RoomUser>()
                    .WithOne(roomUser => roomUser.Room)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<RoomUser>(e =>
                {
                    e.HasKey(rU => new {rU.RoomId, rU.UserId});

                    e.HasOne(rU => rU.Room)
                        .WithMany(r => r.RoomUsers)
                        .OnDelete(DeleteBehavior.Cascade); ;
                }
                );

        }
    }
}
