using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Solutec.Converters
{
    public class TypeAdvancesConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int num = -1;
            if (int.TryParse(value?.ToString(), out num))
            {
                switch (num)
                {
                    case 1:
                        return "25 %";
                    case 2:
                        return "75 %";
                    case 3:
                        return "100 %";
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
