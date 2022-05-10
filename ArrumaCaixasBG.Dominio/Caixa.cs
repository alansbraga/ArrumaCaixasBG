namespace ArrumaCaixasBG.Dominio;

public class Caixa : IEquatable<Caixa>
{
    public decimal Altura { get; set; }
    public decimal Largura { get; set; }
    public decimal Comprimento { get; set; }
    public decimal Peso { get; set; }
    public string Nome { get; set; } = string.Empty;
    public decimal Volume => Altura * Largura * Comprimento;

    public bool Equals(Caixa? other)
    {
        if (other is null)
            return false;

        return Nome == other.Nome;
    }

    public override string ToString()
    {
        return $"({Altura,6} x {Largura,6} x {Comprimento,6}) {Nome}";
    }
}
