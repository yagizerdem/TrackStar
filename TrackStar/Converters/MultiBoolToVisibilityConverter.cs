using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace TrackStar.Converters
{
    public class MultiBoolToVisibilityConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                if (values == null || values.Length == 0)
                    return Visibility.Collapsed;

                bool[] bools = values.Select(v =>
                {
                    // Handle WPF's internal NamedObject
                    if (v != null && v.GetType().FullName == "MS.Internal.NamedObject")
                        return false;

                    // Handle standard cases
                    if (v is bool b) return b;
                    if (v == null) return false;
                    if (v is string s) return !string.IsNullOrWhiteSpace(s);

                    try
                    {
                        return System.Convert.ToBoolean(v, CultureInfo.InvariantCulture);
                    }
                    catch
                    {
                        return false;
                    }
                }).ToArray();

                string op = (parameter as string ?? "AND").Trim().ToUpperInvariant();
                bool result = op switch
                {
                    "OR" => bools.Any(b => b),
                    "XOR" => bools.Count(b => b) == 1,
                    "NAND" => !bools.All(b => b),
                    _ => bools.All(b => b) // AND
                };

                return result ? Visibility.Visible : Visibility.Collapsed;
            }
            catch
            {
                return Visibility.Collapsed;
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
            => throw new NotSupportedException();
    }
}
