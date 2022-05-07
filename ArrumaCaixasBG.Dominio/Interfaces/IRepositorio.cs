using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArrumaCaixasBG.Dominio.Interfaces;

public interface IRepositorio<T>
{
    IEnumerable<T> LerTodos();
}

