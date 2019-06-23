using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Solutec.Converters
{
    public class CustomerTypeValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int num = -1;
            if (int.TryParse(value?.ToString(), out num))
            {
                switch (num)
                {
                    case 1:
                        return "Directo";
                    case 2:
                        return "Tercerizado";
                    default:
                        return "Unknown";
                }
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
