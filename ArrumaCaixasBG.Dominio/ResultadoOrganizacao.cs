namespace ArrumaCaixasBG.Dominio;

public class ResultadoOrganizacao
{
    private readonly IEnumerable<Prateleira> prateleiras;
    private readonly IEnumerable<Caixa> caixasSobrando;

    public ResultadoOrganizacao(IEnumerable<Prateleira> prateleiras, IEnumerable<Caixa> caixasSobrando)
    {
        this.prateleiras = prateleiras;
        this.caixasSobrando = caixasSobrando;
    }

    public string Descricao { get; set; } = string.Empty;

    public IEnumerable<Prateleira> Prateleiras => prateleiras.ToArray();
    public IEnumerable<Caixa> CaixasSobrando => caixasSobrando.ToArray();
}