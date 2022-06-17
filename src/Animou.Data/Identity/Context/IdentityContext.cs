using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Animou.Data.Identity.Context;

public class IdentityContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
{
    public IdentityContext(DbContextOptions<IdentityContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {

        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
        #region "Schema and Column names"

        builder.HasDefaultSchema("Identity");
        builder.Entity<ApplicationUser>(entity =>
        {
            entity.Property(u => u.Id).HasMaxLength(36);
            entity.Property(u => u.UserName).HasMaxLength(100);
            entity.Property(u => u.NormalizedUserName).HasMaxLength(100);
            entity.Property(u => u.Email).HasMaxLength(100);
            entity.Property(u => u.NormalizedEmail).HasMaxLength(100);
            entity.Property(u => u.Avatar).HasMaxLength(100);
            entity.ToTable("Users");
        });
        builder.Entity<IdentityRole<Guid>>(entity =>
        {
            entity.ToTable("Roles");
        });
        builder.Entity<IdentityUserRole<Guid>>(entity =>
        {
            entity.ToTable("UserRoles");
        });
        builder.Entity<IdentityUserClaim<Guid>>(entity =>
        {
            entity.ToTable("UserClaims");
        });
        builder.Entity<IdentityUserLogin<Guid>>(entity =>
        {
            entity.ToTable("UserLogins");
        });
        builder.Entity<IdentityRoleClaim<Guid>>(entity =>
        {
            entity.ToTable("RoleClaims");
        });
        builder.Entity<IdentityUserToken<Guid>>(entity =>
        {
            entity.ToTable("UserTokens");
        });
        #endregion


    }
}
