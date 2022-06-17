using Animou.Business.Models;
using Microsoft.EntityFrameworkCore;

namespace Animou.Data.Context
{
    public class AnimouContext : DbContext
    {
        public AnimouContext(DbContextOptions<AnimouContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Anime> Animes { get; set; }
        public DbSet<Comment> Comments { get; set;}
        public DbSet<Like> likes { get; set; }
        public DbSet<Deslike> Deslikes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetProperties().Where(p => p.ClrType == typeof(Guid)))) property.SetColumnType("varchar(36)");

            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetProperties().Where(p => p.ClrType == typeof(string)))) property.SetColumnType("varchar(100)");

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AnimouContext).Assembly);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

            base.OnModelCreating(modelBuilder);

            modelBuilder.HasDefaultSchema("Animou");
        }
    }
}
