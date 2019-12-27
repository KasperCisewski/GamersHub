using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using GamersHub.Shared.Data.Enums;
using System;

namespace GamersHub.DbSeeder
{
    public class GameCategoryEnumConverter<T> : EnumConverter where T : struct
    {
        public GameCategoryEnumConverter() : base(typeof(T)) { }

        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            if (!Enum.TryParse(text, out GameCategory gameCategory))
            {
                throw new InvalidCastException($"Invalid value to EnumConverter. Type: {typeof(T)} Value: {text}");
            }

            return gameCategory;
        }
    }
}
