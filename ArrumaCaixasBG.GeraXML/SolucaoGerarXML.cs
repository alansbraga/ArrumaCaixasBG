using ArrumaCaixasBG.Dominio;
using ArrumaCaixasBG.Dominio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ArrumaCaixasBG.GeraXML;
internal class SolucaoGerarXML : ISolucaoOrganizador
{
    internal string local = @"C:\tmp\ArrumaBG\ImagemPrateleira";
    internal readonly Dictionary<int, Caixa> caixaComId = new();

    public IEnumerable<Prateleira> Arrumar(IEnumerable<Caixa> caixas, Prateleira prateleira)
    {
        var instance = new Instance
        {
            Containers = new Containers(),
            Version = "1.1",
            Name = "Tudo"
        };
        instance.Containers.Container = new List<Container>
        {

        };
        instance.Containers.Container.Add(new Container()
        {
            Cube = new Cube()
            {
                Length = (int)prateleira.Profundidade,
                Height = (int)prateleira.Altura,
                Width = (int)prateleira.Largura,
                Point = new()
            },
            ID = 1
        });

        instance.Pieces = new()
        {
            Piece = new List<Piece>(),

        };

        caixaComId.Clear();
        var seq = 1;
        foreach (var caixa in caixas.OrderBy(c => c.Nome))
        {
            caixaComId.Add(seq, caixa);
            instance.Pieces.Piece.Add(new Piece()
            {
                Components = new()
                {
                    Cubes = new()
                    {
                        Cube = new Cube()
                        {
                            Length = (int)caixa.Comprimento,
                            Height = (int)caixa.Altura,
                            Width = (int)caixa.Largura,
                            Point = new()
                        }
                    }
                },
                ID = seq,
                ForbiddenOrientations = "",
                Material = "Default",
                Stackable = "True"
            });
            seq++;
        }

        var serializer = new XmlSerializer(typeof(Instance));
        using var stream = new MemoryStream();
        serializer.Serialize(stream, instance);
        var arquivoBase = Path.Combine(local, "Tudo");
        var arquivoAntes = arquivoBase + ".xinst";
        var arquivoDepois = arquivoBase + "-Organizado.xinst";
        File.WriteAllBytes(arquivoAntes, stream.ToArray());
        if (File.Exists(arquivoDepois))
            File.Delete(arquivoDepois);
        while (!File.Exists(arquivoDepois))
        {
            Thread.Sleep(1000);
        }

        

        return CriaPrateleiras(prateleira, arquivoDepois);
    }

    private IEnumerable<Prateleira> CriaPrateleiras(Prateleira prateleira, string arquivoDepois)
    {
        var retorno = new List<Prateleira>();
        var serializer = new XmlSerializer(typeof(Instance));
        using var stream = new MemoryStream(File.ReadAllBytes(arquivoDepois));

        if (serializer.Deserialize(stream) is not Instance instancia)
            return retorno;

        foreach (var prateleiraSol in instancia.Solutions.Solution.Bins.Bin)
        {
            var prateleiraAtual = new Prateleira
            {
                Altura = prateleira.Altura,
                Largura = prateleira.Largura,
                Nome = prateleira.Nome,
                Ordem = prateleira.Ordem,
                Profundidade = prateleira.Profundidade
            };
            var listaArrumada = new List<CaixaArrumada>();
            foreach (var item in prateleiraSol.Item)
            {
                var caixaOriginal = caixaComId[item.ID];
                var novaCaixa = new CaixaArrumada
                {
                    X = item.Point.X,
                    Y = item.Point.Y,
                    Z = item.Point.Z,
                    Nome = caixaOriginal.Nome,

                };
                listaArrumada.Add(novaCaixa);
            }
            prateleiraAtual.SubstituiCaixas(listaArrumada); 
            retorno.Add(prateleiraAtual);
        }
        


        return retorno;
    }
}
