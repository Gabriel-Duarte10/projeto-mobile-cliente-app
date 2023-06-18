using projeto_mobile_cliente_app.Dto;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projeto_mobile_cliente_app.Libraries.Converters
{
    public class ExibirIconeCancelarAgendamento : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            TransacaoClienteDto transacao = (TransacaoClienteDto)value;
            if (transacao == null)
            {
                return false;
            }
            StatusEnum status = transacao.Status;
            if (status == StatusEnum.Pendente)
            {
                return true;
            }else
            {
              return false;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
