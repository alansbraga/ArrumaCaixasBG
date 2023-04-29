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
    private readonly BinPackAlgorithmFactory fabrica;
    private readonly string nome;
    public string Nome => nome;

    public SolucaoOrganizadorBinPacking(BinPackAlgorithmFactory fabrica, string nome)
    {
        this.fabrica = fabrica;
        this.nome = nome;
    }

    public ResultadoOrganizacao Arrumar(IEnumerable<Caixa> caixas, Prateleira prateleira)
    {
        var novasCaixas = caixas.Select(c => new Cuboid(c.Largura, c.Altura, c.Comprimento, c.Peso, c));
        var binPacker = new BinPacker(BinPackerVerifyOption.All, parameter => fabrica(parameter));
        var parameter = new BinPackParameter(
            prateleira.Profundidade, prateleira.Largura, prateleira.Altura,
            novasCaixas.Sum(a => a.Weight) * 4, true, novasCaixas);
        var result = binPacker.Pack(parameter);
        var arrumadas = TransformaPrateleiraComCaixa(prateleira, result.BestResult);
        var melhor = arrumadas.OrderBy(a => a.VolumeNaoUtilizado).First();
        return new ResultadoOrganizacao(Nome, new []{ melhor }, Enumerable.Empty<Caixa>());
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
                Profundidade = prateleira.Profundidade,
                Ordem = prateleira.Ordem
            };

            novaPrateleira.SubstituiCaixas(resultado.Select(c => new CaixaArrumada()
            {
                Altura = c.Depth,
                Comprimento = c.Width,
                Largura = c.Height,
                Nome = ((Caixa)c.Tag).Nome,
                Peso = c.Weight,
                X = c.Y,
                Y = c.Z,
                Z = c.X
            }));
            yield return novaPrateleira;
        }
    }
}