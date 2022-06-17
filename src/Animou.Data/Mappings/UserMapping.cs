using Animou.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Animou.Data.Mappings
{
    public class UserMapping : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);

            builder
                .HasMany(u => u.Comments)
                .WithOne(c => c.User)
                .HasForeignKey(c => c.Id);

            builder
                .HasMany(u => u.Likes)
                .WithOne(l => l.User)
                .HasForeignKey(l => l.Id);

            builder
                .HasMany(u => u.Deslikes)
                .WithOne(d => d.User)
                .HasForeignKey(d => d.Id);

            builder
                .HasMany(u => u.Animes)
                .WithOne(a => a.User)
                .HasForeignKey(a => a.UserId);

            builder.Property(u => u.Username).HasColumnType("nvarchar(100)");
            builder.Property(u => u.Avatar).HasColumnType("varchar(100)");

            builder.ToTable("Users");
        }
    }
}
