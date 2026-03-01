using System.Globalization;

namespace DayCare.Converters;

/// <summary>
/// Returns Success color when true (paid), Error color when false (unpaid).
/// </summary>
public class BoolToColorConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        bool isPaid = value is bool b && b;
        return isPaid
            ? Application.Current!.Resources["Success"]
            : Application.Current!.Resources["Error"];
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => throw new NotImplementedException();
}
