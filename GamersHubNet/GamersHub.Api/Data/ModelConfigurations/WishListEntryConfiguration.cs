using GamersHub.Api.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GamersHub.Api.Data.ModelConfigurations
{
    public class WishListEntryConfiguration : IEntityTypeConfiguration<WishListEntry>
    {
        public void Configure(EntityTypeBuilder<WishListEntry> builder)
        {
            builder.HasKey(we => new { we.GameId, we.UserId });

            builder.HasOne(we => we.Game)
                .WithMany(g => g.WishlistEntries)
                .HasForeignKey(ug => ug.GameId);

            builder.HasOne(we => we.User)
                .WithMany(g => g.WishList)
                .HasForeignKey(ug => ug.UserId);
        }
    }
}
