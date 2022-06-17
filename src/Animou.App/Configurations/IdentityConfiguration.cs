using Animou.Data.Identity;
using Animou.Data.Identity.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Animou.App.Configurations
{
    public static class IdentityConfiguration
    {
        public static IServiceCollection AddIdentityConfiguration(this IServiceCollection services,
                                                                  IConfiguration configuration)
        {
            services.AddDbContext<IdentityContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                sql => sql.MigrationsHistoryTable("Migrations", "Identity")));

            services.Configure<IdentityOptions>(options => options.User.RequireUniqueEmail = true);

            services.AddDefaultIdentity<ApplicationUser>(options => 
                options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<IdentityContext>();

            return services;
        }
    }
}
