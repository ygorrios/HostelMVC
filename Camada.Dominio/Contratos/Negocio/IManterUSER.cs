﻿using Library.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Camada.Dominio.Contratos.Negocio
{
    public interface IManterUSER
    {
        Message Salvar(Dominio.Entidades.User user);

        Message DeleteUSERByID(int id);

    }
}
