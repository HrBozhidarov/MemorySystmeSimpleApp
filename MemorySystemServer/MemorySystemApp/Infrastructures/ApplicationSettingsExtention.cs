namespace MemorySystemApp.Infrastructures
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public static class ApplicationSettingsExtention
    {
        public static ApplicationSettings GetApplicationSettings(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var applicationSettingsSectionConfiguration = configuration.GetSection(nameof(ApplicationSettings));
            services.Configure<ApplicationSettings>(applicationSettingsSectionConfiguration);

            return applicationSettingsSectionConfiguration.Get<ApplicationSettings>();
        }
    }
}
