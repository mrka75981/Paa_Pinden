using Microsoft.EntityFrameworkCore;
using PaaPindenAPI.Models;

namespace PaaPindenAPI.Data
{
    public class PaaPindenContext : DbContext
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
            modelbuilder.Entity<KnittingPost>();

            modelbuilder.Entity<Content>()
                .HasOne<KnittingPost>();

            modelbuilder.Entity<ContentPicture>().HasKey(c => c.Path);

            modelbuilder.Entity<ContentPicture>()
                .HasOne<Content>()
                .WithMany(c => c.Pictures)
                .HasForeignKey(c => c.ContentId);

            modelbuilder.Entity<ContentVideo>().HasKey(c => c.Path);

            modelbuilder.Entity<ContentVideo>()
                .HasOne<Content>()
                .WithMany(c => c.Videos)
                .HasForeignKey(c => c.ContentId);

            modelbuilder.Entity<Comment>()
                .HasOne<KnittingPost>()
                .WithMany(k => k.Comments);
        }
    }
}
