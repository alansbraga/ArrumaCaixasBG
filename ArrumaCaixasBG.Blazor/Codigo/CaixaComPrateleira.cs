using ArrumaCaixasBG.Dominio;

namespace ArrumaCaixasBG.Blazor.Codigo;

public class CaixaComPrateleira
{
    public CaixaArrumada Caixa { get; }
    public Prateleira Prateleira { get; }

    public CaixaComPrateleira(CaixaArrumada caixa, Prateleira prateleira)
    {
        Caixa = caixa;
        Prateleira = prateleira;
    }

    public override string ToString()
    {
        return $"{Caixa.Nome} - {Prateleira.Nome}";
    }
}