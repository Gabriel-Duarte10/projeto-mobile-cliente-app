using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projeto_mobile_cliente_app.Libraries.Converters
{
    public class ExibirLiquidoSelecionadosEntry : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // If the value is null, return true
            if (value == null)
            {
                return true;
            }

            // If the value is a list, check if it's empty
            if (value is IList list)
            {
                return list.Count == 0;
            }

            // If the value is a string, check if it's empty
            if (value is string str)
            {
                return string.IsNullOrWhiteSpace(str);
            }

            // If the value is not null or an empty list or string, return false
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}