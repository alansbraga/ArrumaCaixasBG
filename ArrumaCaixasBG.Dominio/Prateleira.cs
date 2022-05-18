using System;

namespace ArrumaCaixasBG.Dominio;
public class Prateleira : IEquatable<Prateleira>
{
    private readonly List<CaixaArrumada> caixas;

    public string Nome { get; set; } = string.Empty;
    public decimal Altura { get; set; }
    public decimal Largura { get; set; }
    public int Ordem { get; set; }

    public decimal Profundidade { get; set; }

    public Prateleira()
    {
        caixas = new List<CaixaArrumada>();
    }

    public decimal Volume => Altura * Largura * Profundidade;
    public decimal VolumeNaoUtilizado => Volume - caixas.Sum(c => c.Volume);
    public IEnumerable<CaixaArrumada> Caixas => caixas.ToArray();

    public void SubstituiCaixas(IEnumerable<CaixaArrumada> novasCaixas)
    {
        caixas.Clear();
        caixas.AddRange(novasCaixas);
    }
    public override string ToString()
    {
        return $"({Altura,6} x {Largura,6} x {Profundidade,6}) {Nome}";
    }

    public bool Equals(Prateleira? other)
    {
        if (other is null)
            return false;

        return Nome == other.Nome;
    }

    public override int GetHashCode()
    {
        return Nome.GetHashCode();
    }
}