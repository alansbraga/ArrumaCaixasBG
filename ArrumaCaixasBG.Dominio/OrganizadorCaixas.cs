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
        var caixas = repositorioCaixas.LerTodos();
        var prateleiras = repositorioPrateleiras.LerTodos();
        var prateleirasArrumadas = solucoes.SelectMany(s => s.Arrumar(caixas, prateleiras));
        var agrupadas = prateleirasArrumadas
            .GroupBy(p => p.Nome);
        var retorno = new List<Prateleira>();
        foreach (var porPrateleira in agrupadas)
        {
            var maisOtimizada = porPrateleira
                .OrderBy(p => p.VolumeNaoUtilizado)
                .First();
            retorno.Add(maisOtimizada);
        }
        var resultado = new ResultadoOrganizacao(retorno)
        {
            Descricao = $"{DateTime.Now:yyyy-MM-dd hh-mm-ss}"
        };
        return resultado;
    }
}