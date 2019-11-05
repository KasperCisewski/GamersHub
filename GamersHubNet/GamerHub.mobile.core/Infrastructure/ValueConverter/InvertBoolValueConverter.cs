using MvvmCross.Converters;
using System;
using System.Globalization;

namespace GamerHub.mobile.core.Infrastructure.ValueConverter
{
    public class InvertBoolValueConverter : MvxValueConverter<bool?, bool>
    {
        protected override bool Convert(bool? value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.HasValue)
            {
                return !value.Value;
            }

            return false;
        }
    }
}
