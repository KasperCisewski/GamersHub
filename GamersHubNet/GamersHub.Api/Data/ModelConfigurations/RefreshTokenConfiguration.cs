using GamersHub.Api.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GamersHub.Api.Data.ModelConfigurations
{
    public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.HasKey(x => x.Token);

            builder.HasOne(x => x.User)
                .WithMany();

            builder.Property(x => x.Token)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.JwtId)
                .IsRequired();

            builder.Property(x => x.CreationDate)
                .IsRequired();

            builder.Property(x => x.ExpiryDate)
                .IsRequired();

            builder.Property(x => x.Used)
                .IsRequired()
                .HasDefaultValue(false);

            builder.Property(x => x.Invalidated)
                .IsRequired()
                .HasDefaultValue(false);

            builder.Property(x => x.UserId)
                .IsRequired();
        }
    }
}
