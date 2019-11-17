using GamersHub.Api.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GamersHub.Api.Data.ModelConfigurations
{
    public class ScreenshotConfiguration : IEntityTypeConfiguration<GameImage>
    {
        public void Configure(EntityTypeBuilder<GameImage> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.FileName)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.Data)
                .IsRequired();

            builder.Property(x => x.Length)
                .IsRequired();

            builder.Property(x => x.Width)
                .IsRequired();

            builder.Property(x => x.Height)
                .IsRequired();

            builder.Property(x => x.ContentType)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}
