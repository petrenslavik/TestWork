using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TestApp.Data.Entities;

namespace TestApp.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<UserTest> UserTests { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
          modelBuilder.Entity<Answer>(entity =>
            {
                entity.ToTable("Answers");

                entity.Property(e => e.Id).HasDefaultValueSql("newid()");

                entity.Property(e => e.Text)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.Answers)
                    .HasForeignKey(d => d.QuestionId);
            });

            modelBuilder.Entity<Question>(entity =>
            {
                entity.ToTable("Questions");

                entity.Property(e => e.Id).HasDefaultValueSql("newid()");

                entity.Property(e => e.Text)
                    .IsRequired()
                    .HasMaxLength(400);

                entity.HasOne(d => d.Test)
                    .WithMany(p => p.Questions)
                    .HasForeignKey(d => d.TestId);
            });

            modelBuilder.Entity<Test>(entity =>
            {
                entity.ToTable("Tests");

                entity.Property(e => e.Id).HasDefaultValueSql("newid()");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(1000);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Users");

                entity.Property(e => e.Id).HasDefaultValueSql("newid()");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(70);
            });

            modelBuilder.Entity<UserTest>(entity =>
            {
                entity.ToTable("User_Test");

                entity.Property(e => e.Id).HasDefaultValueSql("newid()");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.HasOne(d => d.Test)
                    .WithMany(p => p.UserTest)
                    .HasForeignKey(d => d.TestId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserTest)
                    .HasForeignKey(d => d.UserId);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
