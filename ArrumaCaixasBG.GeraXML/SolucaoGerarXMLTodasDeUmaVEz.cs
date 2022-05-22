using ArrumaCaixasBG.Dominio;
using ArrumaCaixasBG.Dominio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ArrumaCaixasBG.GeraXML;
internal class SolucaoGerarXMLTodasDeUmaVEz : ISolucaoOrganizador
{
    internal string local = @"C:\tmp\ArrumaBG\ImagemPrateleira";
    internal readonly HashSet<Prateleira> prateleiras = new();

    public IEnumerable<Prateleira> Arrumar(IEnumerable<Caixa> caixas, Prateleira prateleira)
    {
        prateleiras.Add(new Prateleira()
        {
            Profundidade = prateleira.Profundidade,
            Altura = prateleira.Altura,
            Largura = prateleira.Largura,
            Nome = prateleira.Nome
        });
        var instance = new Instance
        {
            Containers = new Containers(),
            Version = "1.1",
            Name = "Tudo"
        };
        instance.Containers.Container = new List<Container>
        {

        };
        var seqPrat = 1;
        foreach (var prat in prateleiras)
        {
            instance.Containers.Container.Add(new Container()
            {
                Cube = new Cube()
                {
                    Length = (int)prat.Profundidade,
                    Height = (int)prat.Altura,
                    Width = (int)prat.Largura,
                    Point = new()
                },
                ID = seqPrat
            });
            seqPrat++;
        }

        instance.Pieces = new()
        {
            Piece = new List<Piece>(),

        };

        var seq = 1;
        foreach (var caixa in caixas.OrderBy(c => c.Nome))
        {
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
        File.WriteAllBytes(Path.Combine(local, $"Tudo.xinst"), stream.ToArray());
        
        return new[] { prateleira };
    }
}
