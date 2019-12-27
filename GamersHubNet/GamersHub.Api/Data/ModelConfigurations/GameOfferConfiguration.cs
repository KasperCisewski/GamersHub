using GamersHub.Api.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GamersHub.Api.Data.ModelConfigurations
{
    public class GameOfferConfiguration : IEntityTypeConfiguration<GameOffer>
    {
        public void Configure(EntityTypeBuilder<GameOffer> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Store);

            builder.HasOne(x => x.Game);

            builder.Property(x => x.Price)
                .IsRequired();

            builder.Property(x => x.OfferUrl)
                .IsRequired()
                .HasMaxLength(1000);
        }
    }
}
