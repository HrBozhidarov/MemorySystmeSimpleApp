namespace MemorySystemApp
{
    using System.Reflection;

    using MemorySystemApp.Data;
    using MemorySystemApp.Infrastructures;
    using MemorySystemApp.Infrastructures.AutomapperSettings;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<MemorySystemDbContext>(options => options
                .UseSqlServer(Configuration.GetConnectionString("DefaultConnection")))
                .AddIdentity()
                .JwtAuthentication(services.GetApplicationSettings(this.Configuration))
                .AddServices()
                .AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            AutoMapperConfig.RegisterMappings(typeof(Startup).GetTypeInfo().Assembly);

            app.UseRouting()
            .UseCors(opt => opt
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader())
            .UseAuthentication()
            .UseAuthorization()
            .UseEndpoints(endpoints => { endpoints.MapControllers(); })
            .ApplyMigration();
        }
    }
}
