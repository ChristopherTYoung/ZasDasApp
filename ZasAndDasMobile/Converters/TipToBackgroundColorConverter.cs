using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZasAndDasMobile.Converters
{
    public class TipToBackgroundColorConverter : IValueConverter
    {
        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            string? selectedTip = value?.ToString();
            string? thisTip = parameter?.ToString();

            return selectedTip == thisTip ? Colors.DarkRed : Colors.LightGray;
        }

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}
