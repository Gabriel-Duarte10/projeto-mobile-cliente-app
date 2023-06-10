using projeto_mobile_cliente_app.Dto;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projeto_mobile_cliente_app.Libraries.Converters
{
    public class ColorStatusBorderTransacao : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            TransacaoClienteDto transacao = (TransacaoClienteDto)value;
            if (transacao == null)
            {
                return Colors.Black;
            }
            StatusEnum status = transacao.Status;
            if (status == StatusEnum.Pendente)
            {
                return Color.FromArgb("#414955");
            }
            if (status == StatusEnum.Aprovado)
            {
                return Color.FromArgb("#3F8D32");
            }
            if (status == StatusEnum.Cancelado)
            {
                return Color.FromArgb("#DA7676");
            }
            return Color.FromArgb("#414955");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
