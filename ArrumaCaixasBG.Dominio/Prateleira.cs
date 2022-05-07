using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArrumaCaixasBG.Dominio;
public record struct Prateleira(string Nome, decimal Altura, decimal Largura, decimal Profundidade)
{
    public decimal Volume => Altura * Largura * Profundidade;

    public override string ToString()
    {
        return $"({Altura,4}x{Largura,4}x{Profundidade,4}) {Nome}";
    }

}