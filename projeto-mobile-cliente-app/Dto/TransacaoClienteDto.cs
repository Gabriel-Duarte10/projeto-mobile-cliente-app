using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projeto_mobile_cliente_app.Dto
{
    public class TransacaoClienteDto
    {
        public int Id { get; set; }
        public PostoDto Posto { get; set; }
        public StatusEnum Status { get; set; }
        public DateTime dataAgendada { get; set; }
        public double Valor { get; set; }
        public String CodigoTransacao { get; set; }
        public List<TransacaoItemDto> TransacaoItem { get; set; }
    }
    public class TransacaoItemDto
    {
        public int QtdAgendada { get; set; }
        public double Valor { get; set; }
        public LiquidoDto Liquido { get; set; }
        public DateTime CreatedAt { get; set; }
    }

}
