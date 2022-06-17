namespace Animou.App.Configurations
{
    public static class MvcConfiguration
    {
        public static IServiceCollection AddMvcConfiguration(this IServiceCollection services)
        {
            services.AddControllersWithViews();

            return services;
        }
    }
}
