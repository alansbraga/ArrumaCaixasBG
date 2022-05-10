using ArrumaCaixasBG.Dominio.Interfaces;
using System.Linq;

namespace ArrumaCaixasBG.Dominio;

internal class OrganizadorCaixas : IOrganizadorCaixas
{
    private readonly IRepositorioCaixas repositorioCaixas;
    private readonly IRepositorioPrateleiras repositorioPrateleiras;
    private readonly IEnumerable<ISolucaoOrganizador> solucoes;

    public OrganizadorCaixas(
        IRepositorioCaixas repositorioCaixas,
        IRepositorioPrateleiras repositorioPrateleiras,
        IEnumerable<ISolucaoOrganizador> solucoes)
    {
        this.repositorioCaixas = repositorioCaixas;
        this.repositorioPrateleiras = repositorioPrateleiras;
        this.solucoes = solucoes;
    }

    public ResultadoOrganizacao Organizar()
    {
        var caixas = repositorioCaixas.LerTodos()
            .ToList();
        var prateleiras = repositorioPrateleiras.LerTodos()
            .ToList();
        var retorno = new List<Prateleira>();

        while (prateleiras.Any() && caixas.Any())
        {
            var prateleirasArrumadas = new List<Prateleira>();
            foreach (var prateleira in prateleiras)
            {
                prateleirasArrumadas.AddRange(solucoes.SelectMany(s => s.Arrumar(caixas, prateleira)));
            }
            var maisOtimizada = prateleirasArrumadas
                .OrderBy(p => p.VolumeNaoUtilizado)
                .First();
            retorno.Add(maisOtimizada);
            prateleiras.Remove(maisOtimizada);
            RemoverCaixasUsadas(maisOtimizada, caixas);
        }


        var resultado = new ResultadoOrganizacao(retorno, caixas)
        {
            Descricao = $"{DateTime.Now:yyyy-MM-dd hh-mm-ss}"
        };
        return resultado;
    }

    private static void RemoverCaixasUsadas(Prateleira maisOtimizada, List<Caixa> caixas)
    {
        foreach (var caixa in maisOtimizada.Caixas)
        {
            caixas.Remove(caixa);
        }
    }
}