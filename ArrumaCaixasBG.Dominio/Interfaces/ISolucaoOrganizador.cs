namespace ArrumaCaixasBG.Dominio.Interfaces;

public interface ISolucaoOrganizador
{
    string Nome { get; }
    ResultadoOrganizacao Arrumar(IEnumerable<Caixa> caixas, Prateleira prateleira);
}

