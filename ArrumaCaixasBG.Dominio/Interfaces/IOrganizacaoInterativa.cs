namespace ArrumaCaixasBG.Dominio.Interfaces;

public interface IOrganizacaoInterativa
{
    IEnumerable<Caixa> CaixasDisponiveis();
    IEnumerable<Prateleira> PrateleirasVazias();
    IEnumerable<ISolucaoOrganizador> SolucoesDisponiveis();
    IEnumerable<Prateleira> PrateleirasOrganizadas();

    void Iniciar();
    Task<IEnumerable<ResultadoOrganizacao>> OrganizarAsync(IEnumerable<ISolucaoOrganizador> solucoesLocal,
        IEnumerable<Prateleira> prateleirasLocal,
        IEnumerable<Caixa> caixasLocal,
        CancellationToken cancellationToken);

    void UsarPrateleira(Prateleira prateleira);
}