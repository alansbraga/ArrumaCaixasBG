using ArrumaCaixasBG.Dominio.Interfaces;

namespace ArrumaCaixasBG.Dominio;

public class OrganizacaoInterativa : IOrganizacaoInterativa
{
    private readonly IRepositorioCaixas repositorioCaixas;
    private readonly IRepositorioPrateleiras repositorioPrateleiras;
    private readonly IEnumerable<ISolucaoOrganizador> solucoes;
    private readonly List<Caixa> caixas = new();
    private readonly List<Prateleira> prateleiras = new();
  

    public OrganizacaoInterativa(
        IRepositorioCaixas repositorioCaixas,
        IRepositorioPrateleiras repositorioPrateleiras,
        IEnumerable<ISolucaoOrganizador> solucoes)
    {
        this.repositorioCaixas = repositorioCaixas;
        this.repositorioPrateleiras = repositorioPrateleiras;
        this.solucoes = solucoes;
    }
    public IEnumerable<Caixa> CaixasDisponiveis()
    {
        return caixas.ToArray();
    }

    public IEnumerable<Prateleira> PrateleirasVazias()
    {
        return prateleiras.ToArray();
    }

    public IEnumerable<ISolucaoOrganizador> SolucoesDisponiveis()
    {
        return solucoes.ToArray();
    }

    public void Iniciar()
    {
        caixas.Clear();
        caixas.AddRange(repositorioCaixas.LerTodos());
        prateleiras.Clear();
        prateleiras.AddRange(repositorioPrateleiras.LerTodos());

    }

    public async Task<IEnumerable<ResultadoOrganizacao>> OrganizarAsync(
        IEnumerable<ISolucaoOrganizador> solucoesLocal,
        IEnumerable<Prateleira> prateleirasLocal,
        IEnumerable<Caixa> caixasLocal,
        Action<string> progresso,
        CancellationToken cancellationToken)
    {
        var retorno = new List<ResultadoOrganizacao>();
        var tasks = new List<Task<ResultadoOrganizacao>();
        foreach (var prateleira in prateleirasLocal)
        {
            if (cancellationToken.IsCancellationRequested)
                break;
            foreach (var organizador in solucoesLocal)
            {
                if (cancellationToken.IsCancellationRequested)
                    break;
                tasks.Add(Task.Run(() =>
                {
                    return organizador.Arrumar(caixasLocal.ToArray(), prateleira);
                }));
            }
        }

        await Task.WhenAll(tasks);
        retorno.AddRange(tasks.Select(t => t.Result));
        
        return retorno;
    }
}