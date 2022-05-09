using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArrumaCaixasBG.Dominio;

public class ResultadoArrumacao
{
    private readonly List<Caixa> caixasSobrando;
    private readonly List<Prateleira> prateleirasArrumadas;

    public ResultadoArrumacao()
    {
        caixasSobrando = new();
        prateleirasArrumadas = new();
    }

    public IEnumerable<Caixa> CaixasSobranco 
    {
        get => caixasSobrando.ToArray();
        set 
        {
            caixasSobrando.Clear();
            caixasSobrando.AddRange(value);
        }
    }

    public IEnumerable<Prateleira> PrateleirasArrumadas {
        get => prateleirasArrumadas.ToArray();
        set {
            prateleirasArrumadas.Clear();
            prateleirasArrumadas.AddRange(value);
        }
    }

}