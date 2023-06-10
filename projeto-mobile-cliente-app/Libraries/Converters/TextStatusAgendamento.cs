using projeto_mobile_cliente_app.Dto;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projeto_mobile_cliente_app.Libraries.Converters
{
    public class TextStatusAgendamento : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            TransacaoClienteDto transacao = (TransacaoClienteDto)value;
            if (transacao == null)
            {
                return "";
            }
            StatusEnum status = transacao.Status;
            if (status == StatusEnum.Pendente)
            {
                return "Status: Pendente";
            }
            if (status == StatusEnum.Aprovado)
            {
                return "Status: Aprovado";
            }
            if (status == StatusEnum.Cancelado)
            {
                return "Status: Cancelado";
            }
            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
