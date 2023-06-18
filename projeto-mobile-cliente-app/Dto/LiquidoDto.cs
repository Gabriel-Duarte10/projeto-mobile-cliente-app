using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projeto_mobile_cliente_app.Dto
{
    public class LiquidoDto
    {
        public int? Id { get; set; }
        public string Nome { get; set; }
        public double ValorUnitario { get; set; }
        public bool IsSelected { get; set; }
    }
}
