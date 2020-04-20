using System;
using System.Globalization;
using MvvmCross.Converters;

namespace GamerHub.mobile.core.Infrastructure.Converters
{
    public class InvertBoolValueConverter : MvxValueConverter<bool?, bool>
    {
        protected override bool Convert(bool? value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.HasValue ? !value.Value : false;
        }
    }
}
