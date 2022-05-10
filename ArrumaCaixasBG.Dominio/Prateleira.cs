using System;

namespace ArrumaCaixasBG.Dominio;
public class Prateleira : IEquatable<Prateleira>
{
    private readonly List<Caixa> caixas;

    public string Nome { get; set; } = string.Empty;
    public decimal Altura { get; set; }
    public decimal Largura { get; set; }

    public decimal Profundidade { get; set; }

    public Prateleira()
    {
        caixas = new List<Caixa>();
    }

    public decimal Volume => Altura * Largura * Profundidade;
    public decimal VolumeNaoUtilizado => Volume - caixas.Sum(c => c.Volume);
    public IEnumerable<Caixa> Caixas => caixas.ToArray();

    public void SubstituiCaixas(IEnumerable<Caixa> novasCaixas)
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
}