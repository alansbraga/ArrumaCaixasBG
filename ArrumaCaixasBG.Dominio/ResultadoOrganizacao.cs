namespace ArrumaCaixasBG.Dominio;

public class ResultadoOrganizacao
{
    private readonly IEnumerable<Prateleira> prateleiras;

    public ResultadoOrganizacao(IEnumerable<Prateleira> prateleiras)
    {
        this.prateleiras = prateleiras;
    }

    public string Descricao { get; set; } = string.Empty;

    public IEnumerable<Prateleira> Prateleiras => prateleiras.ToArray();
}