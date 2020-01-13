using CsvHelper.Configuration;
using GamersHub.Api.Domain;
using GamersHub.Shared.Data.Enums;
using System.Globalization;

namespace GamersHub.DbSeeder
{
    internal sealed class GameMap : ClassMap<Game>
    {
        public GameMap()
        {
            var en = new CultureInfo("en-US");

            Map(m => m.Name).Name("Titile");
            Map(m => m.GameCategory).Name("Genre").TypeConverter<GameCategoryEnumConverter<GameCategory>>();
            Map(m => m.ReleaseDate).Name("Realease Date")
                .TypeConverterOption.Format("dd MMMM yyyy")
                .TypeConverterOption.CultureInfo(new CultureInfo("en-us"));
            Map(m => m.Description).Name("Description");
            Map(m => m.VideoUrl).Name("YoTube URL");
        }
    }
}
