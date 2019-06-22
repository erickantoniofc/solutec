using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Solutec.Converters
{
    public class UserTypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int num = -1;
            if (int.TryParse(value?.ToString(), out num))
            {
                switch (num)
                {
                    case 1:
                        return "Administrador";
                    case 2:
                        return "Atencion al cliente";
                    case 3:
                        return "Tecnico";
                    default:
                        return "Unknown";
                }
            }
            
            return value;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //switch ((string)value)
            //{
            //    case "Administrador":
            //        return 1;
            //    case "Atencion al cliente":
            //        return 2;
            //    case "Tecnico":
            //        return 3;
            //}
            //return 1;
            throw new NotImplementedException();
        }
        
    }
}
