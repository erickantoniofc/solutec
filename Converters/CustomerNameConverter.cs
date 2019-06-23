using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Solutec.Converters
{
    public class CustomerNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int id = -1;
            if (int.TryParse(value?.ToString(), out id))
            {
                var context = new Solutec.Models.solutecEntities();
                var model = context.customers.FirstOrDefault(i => i.id_customer == id);
                return model?.full_name;
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
