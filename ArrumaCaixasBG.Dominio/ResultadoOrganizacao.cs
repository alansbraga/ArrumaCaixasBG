namespace ArrumaCaixasBG.Dominio;

public readonly record struct ResultadoOrganizacao
{
    private readonly IEnumerable<Prateleira> prateleiras;
    private readonly IEnumerable<Caixa> caixasSobrando;

    public ResultadoOrganizacao(string descricao, IEnumerable<Prateleira> prateleiras, IEnumerable<Caixa> caixasSobrando)
    {
        this.prateleiras = prateleiras;
        this.caixasSobrando = caixasSobrando;
        this.Descricao = descricao;
    }

    public string Descricao { get; } = string.Empty;

    public IEnumerable<Prateleira> Prateleiras => prateleiras.ToArray();
    public IEnumerable<Caixa> CaixasSobrando => caixasSobrando.ToArray();
    public IEnumerable<Prateleira> PrateleirasVazias()
    {
        return prateleiras
            .Where(p => !p.Caixas.Any())
            .ToArray();
    }
}