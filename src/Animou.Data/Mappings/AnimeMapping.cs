using Animou.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Animou.Data.Mappings
{
    public class AnimeMapping : IEntityTypeConfiguration<Anime>
    {
        public void Configure(EntityTypeBuilder<Anime> builder)
        {
            builder.HasKey(a => a.Id);

            builder
                .HasMany(a => a.Comments)
                .WithOne(c => c.Anime)
                .HasForeignKey(c => c.Id);

            builder.ToTable("Animes");
        }
    }
}
