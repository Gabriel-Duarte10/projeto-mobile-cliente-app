﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projeto_mobile_cliente_app.Dto
{
    public class LoginDto
    {
        public AdministradorDto Administrador { get; set; }
        public ClienteDto Cliente { get; set; }
        public FuncionarioPostoDto FuncionarioPosto { get; set; }
        public DonoPostoDto DonoPosto { get; set; }
    }
}
