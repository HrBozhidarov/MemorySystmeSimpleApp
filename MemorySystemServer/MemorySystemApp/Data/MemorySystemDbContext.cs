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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<CategoryPicture>()
                .HasOne(e => e.Category)
                .WithMany(e => e.CategoryPictures)
                .HasForeignKey(e => e.CategoryId);

            builder.Entity<CategoryPicture>()
                .HasOne(e => e.Picture)
                .WithMany(e => e.CategoryPictures)
                .HasForeignKey(e => e.PictureId);

            base.OnModelCreating(builder);
        }
    }
}
