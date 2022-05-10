using ArrumaCaixasBG.Dominio;
using ArrumaCaixasBG.Dominio.Interfaces;
using Sharp3DBinPacking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArrumaCaixasBG.SolucaoCaixa;

internal class SolucaoOrganizadorBinPacking : ISolucaoOrganizador
{
    public IEnumerable<Prateleira> Arrumar(IEnumerable<Caixa> caixas, Prateleira prateleira)
    {
        var novasCaixas = caixas.Select(c => new Cuboid(c.Largura, c.Altura, c.Comprimento, c.Peso, c));
        var binPacker = BinPacker.GetDefault(BinPackerVerifyOption.All);
        var parameter = new BinPackParameter(
            prateleira.Largura, prateleira.Altura, prateleira.Profundidade,
            novasCaixas.Sum(a => a.Weight) * 4, true, novasCaixas);
        var result = binPacker.Pack(parameter);
        return TransformaPrateleiraComCaixa(prateleira, result.BestResult);
    }

    private static IEnumerable<Prateleira> TransformaPrateleiraComCaixa(Prateleira prateleira, IList<IList<Cuboid>> resultados)
    {
        foreach (var resultado in resultados)
        {
            var novaPrateleira = new Prateleira()
            {
                Altura = prateleira.Altura,
                Largura = prateleira.Largura,
                Nome = prateleira.Nome,
                Profundidade = prateleira.Profundidade
            };

            novaPrateleira.SubstituiCaixas(resultado.Select(c => new Caixa()
            {
                Altura = c.Height,
                Comprimento = c.Depth,
                Largura = c.Width,
                Nome = ((Caixa)c.Tag).Nome,
                Peso = c.Weight
            }));
            yield return novaPrateleira;
        }
    }
}