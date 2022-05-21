using ArrumaCaixasBG.Dominio;
using ArrumaCaixasBG.Dominio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArrumaCaixasBG.SolucaoAPI;
internal class SolucaoOrganizadorAPI : ISolucaoOrganizador
{
    public IEnumerable<Prateleira> Arrumar(IEnumerable<Caixa> caixas, Prateleira prateleira)
    {
        var conversor = new ConverteAPIPrateleira(caixas, prateleira);
        var envio = conversor.ConverteParaAPI();
        var resultado = AcessaAPIEspera.Calcular(envio);
        throw new Exception();
    }
}
