using Animou.App.API;
using Animou.App.ViewModels;
using Animou.Business.Interfaces;
using Animou.Data.Context;
using Animou.Data.Repository;

namespace Animou.App.Configurations
{
    public static class DIConfiguration
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<AnimouContext>();
            services.AddScoped<UserViewModel>();
            services.AddScoped<AnimeViewModel>();
            services.AddScoped<ApiViewModel>();
            services.AddScoped<ApiAnimeViewModel>();
            services.AddHttpClient<ApiRepository>();
            services.AddTransient<Api>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAnimeRepository, AnimeRepository>();

            return services;
        }
    }
}
