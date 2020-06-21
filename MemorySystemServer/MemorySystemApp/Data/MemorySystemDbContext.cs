namespace MemorySystemApp.Data
{
    using MemorySystemApp.Data.Models;

    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class MemorySystemDbContext : IdentityDbContext<User>
    {
        public MemorySystemDbContext(DbContextOptions<MemorySystemDbContext> options)
            : base(options)
        {
        }

        public DbSet<Picture> Pictures { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<CategoryPicture> CategoryPictures { get; set; }

        public DbSet<Like> Likes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<CategoryPicture>().HasKey(e => new { e.CategoryId, e.PictureId });

            builder.Entity<CategoryPicture>()
                .HasOne(e => e.Category)
                .WithMany(e => e.CategoryPictures)
                .HasForeignKey(e => e.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<CategoryPicture>()
                .HasOne(e => e.Picture)
                .WithMany(e => e.CategoryPictures)
                .HasForeignKey(e => e.PictureId)
                .OnDelete(DeleteBehavior.Restrict); ;

            builder.Entity<Like>().HasKey(e => new { e.UserId, e.PictureId });

            builder.Entity<Like>()
                .HasOne(e => e.User)
                .WithMany(e => e.Likes)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Like>()
                .HasOne(e => e.Picture)
                .WithMany(e => e.Likes)
                .HasForeignKey(e => e.PictureId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Comment>()
                .HasOne(e => e.Owner)
                .WithMany(e => e.Comments)
                .HasForeignKey(e => e.OwnerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Comment>()
                .HasOne(e => e.Picture)
                .WithMany(e => e.Comments)
                .HasForeignKey(e => e.PictureId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Picture>()
                .HasOne(e => e.Owner)
                .WithMany(e => e.Pictures)
                .HasForeignKey(e => e.OwnerId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }
    }
}
