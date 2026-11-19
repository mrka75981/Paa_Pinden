using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Shared.Models;
using System.Reflection.Emit;

namespace PaaPindenAPI.Data
{
    public class PaaPindenContext : IdentityDbContext<User>
    {
        public DbSet<ContentPicture> ContentPictures { get; set; }
        public DbSet<ContentVideo> ContentVideos { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Content> Contents { get; set; }
        public DbSet<KnittingPost> KnittingPosts { get; set; }

        public PaaPindenContext(DbContextOptions<PaaPindenContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            base.OnModelCreating(modelbuilder);

            List<IdentityUser> users = new List<IdentityUser>()
            {
                new User()
                {
                    Id = "84cd148c-e055-4d01-8a32-d06e7489e2a3",
                    UserName = "test",
                    NormalizedUserName = "TEST",
                    Email = "test@test.test",
                    NormalizedEmail = "TEST@TEST.TEST",
                    EmailConfirmed = true,
                    PasswordHash = "AQAAAAIAAYagAAAAEDU5ss3/xPgY9SmYXgjbzqrnwgKE4cQpHkYP+zu0cfUKVes0+fsTo/Y1micgg+lFrQ==",
                    SecurityStamp = "E6PKSVRQKLTX6JM23QOOG7NSEMZAT47O",
                    ConcurrencyStamp = "451190ea-b605-4320-980a-6b863f169b73",
                    PhoneNumber = null,
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnd = null,
                    LockoutEnabled = true,
                    AccessFailedCount = 0
                },
            };

            modelbuilder.Entity<User>().HasData(users);

            modelbuilder.Entity<KnittingPost>();

            modelbuilder.Entity<Content>()
                .HasOne<KnittingPost>()
                .WithOne(k => k.KnittingPostContent)
                .HasForeignKey<Content>(c => c.KnittingPostId)
                .OnDelete(DeleteBehavior.Cascade);

            modelbuilder.Entity<ContentPicture>().HasKey(c => c.Path);

            modelbuilder.Entity<ContentPicture>()
                .HasOne<Content>()
                .WithMany(c => c.Pictures)
                .HasForeignKey(c => c.ContentId)
                .OnDelete(DeleteBehavior.Cascade);

            modelbuilder.Entity<ContentVideo>().HasKey(c => c.Path);

            modelbuilder.Entity<ContentVideo>()
                .HasOne<Content>()
                .WithMany(c => c.Videos)
                .HasForeignKey(c => c.ContentId)
                .OnDelete(DeleteBehavior.Cascade);

            modelbuilder.Entity<Comment>()
                .HasOne<KnittingPost>()
                .WithMany(k => k.Comments)
                .HasForeignKey(c => c.CommentKnittingPostId)
                .OnDelete(DeleteBehavior.Cascade);

            modelbuilder.Entity<Comment>()
           .HasOne<User>()
           .WithMany(u => u.Comments)
           .HasForeignKey(u => u.UserId)
           .OnDelete(DeleteBehavior.NoAction);
        }
    }
}