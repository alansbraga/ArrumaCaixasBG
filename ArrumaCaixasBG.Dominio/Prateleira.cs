namespace ArrumaCaixasBG.Dominio;
public record struct Prateleira(string Nome, decimal Altura, decimal Largura, decimal Profundidade)
{
    public decimal Volume => Altura * Largura * Profundidade;

    public override string ToString()
    {
        return $"({Altura,6} x {Largura,6} x {Profundidade,6}) {Nome}";
    }

}