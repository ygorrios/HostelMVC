﻿using Library.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Camada.Dominio.Contratos.Negocio
{
    public interface IManterCALC
    {
        Message Salvar(Dominio.Entidades.Calc CALC);

        Message DeleteCalcByIDMoney_Count(int id);

    }
}
