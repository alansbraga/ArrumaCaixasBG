namespace ArrumaCaixasBG.Dominio.Interfaces;

public interface ISolucaoOrganizador
{
    IEnumerable<Prateleira> Arrumar(IEnumerable<Caixa> caixas, Prateleira prateleira);
}

