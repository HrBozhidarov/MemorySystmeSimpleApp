namespace MemorySystemApp.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class MemorySystemDbContext : IdentityDbContext
    {
        public MemorySystemDbContext(DbContextOptions<MemorySystemDbContext> options)
            : base(options)
        {
        }
    }
}
