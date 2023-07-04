using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projeto_mobile_cliente_app.Requests
{
    public class TransacaoClienteRequest
    {
        public int? Id { get; set; }
        public DateTime DataAgendada { get; set; }
        public int IdCliente { get; set; }
        public int IdPosto { get; set; }
        public List<TransacaoItensRequest> TransacaoItens { get; set; }
    }
    public class TransacaoItensRequest
    {
        public int QtdAgendada { get; set; }
        public int? IdLiquido { get; set; }
    }
}
