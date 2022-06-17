using Animou.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Animou.Data.Mappings
{
    public class CommentMapping : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasKey(c => c.Id);

            builder
                .HasMany(c => c.Likes)
                .WithOne(l => l.Comment)
                .HasForeignKey(l => l.Id);

            builder
                .HasMany(c => c.Deslikes)
                .WithOne(d => d.Comment)
                .HasForeignKey(d => d.Id);

            builder
                .Property(c => c.Text)
                .IsRequired()
                .HasColumnType("nvarchar(450)");

            builder.ToTable("Comments");
        }
    }
}
