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

        public QuizItContext(DbContextOptions dbContextOptions) :base(dbContextOptions)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Room>(e =>
            //{
            //    e.HasOne(q => q.Quiz)
            //        .WithMany(r => r.Rooms)
            //        .OnDelete(DeleteBehavior.Cascade);
            //});'

            modelBuilder.Entity<User>(e =>
            {
                e.HasIndex(u => u.EMail).IsUnique();
                e.HasData(new User()
                {
                    UserId = 1,
                    EMail = "testEmail@gmail.com",
                    Password = "Password"
                });
                e.HasMany(c => c.RoomUsers)
                    .WithOne(d => d.User)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Room>(e =>
            {
                e.HasMany(c => c.RoomUsers)
                    .WithOne(d => d.Room)
                    .OnDelete(DeleteBehavior.Cascade);
            });


            modelBuilder.Entity<RoomUser>(e =>
                {
                    e.HasKey(rU => new { rU.RoomId, rU.UserId });
                    e.HasOne(rU => rU.User).WithMany(c => c.RoomUsers).OnDelete(DeleteBehavior.ClientSetNull);
                }
                );

        }
    }
}
