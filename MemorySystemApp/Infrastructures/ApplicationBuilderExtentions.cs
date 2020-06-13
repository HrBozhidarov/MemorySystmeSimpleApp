using MemorySystemApp.Data;

using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace MemorySystemApp.Infrastructures
{
    public static class ApplicationBuilderExtentions
    {
        public static void ApplyMigration(this IApplicationBuilder applicationBuilder)
        {
            using var services = applicationBuilder.ApplicationServices.CreateScope();

            var dbContext = services.ServiceProvider.GetService<MemorySystemDbContext>();

            dbContext.Database.Migrate();
        }
    }
}
