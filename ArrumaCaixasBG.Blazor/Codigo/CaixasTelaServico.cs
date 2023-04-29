using ArrumaCaixasBG.Dominio;
using Blazor3D.Cameras;
using Blazor3D.Core;
using Blazor3D.Helpers;
using Blazor3D.Lights;
using Blazor3D.Maths;

namespace ArrumaCaixasBG.Blazor.Codigo;

public class CaixasTelaServico
{
    private readonly Random rnd;

    public CaixasTelaServico()
    {
        rnd = new Random();
    }
    public DadosTela Transforma(Prateleira prateleira)
    {
        var retorno = new DadosTela {
            Camera = CriaCamera(prateleira),
            Caixas = CriaCaixas(prateleira),
            Adicionais = CriaAdicionais(prateleira)
        };
        return retorno;
    }

    private IEnumerable<Object3D> CriaAdicionais(Prateleira prateleira)
    {
        var retorno = new List<Object3D>();
        retorno.Add(new AmbientLight());
        retorno.Add(new PointLight()
        {
            Position = new Vector3
            {
                X = 100,
                Y = 300,
                Z = 0
            }
        });
        retorno.Add(new ArrowHelper()
        {
            Origin = new Vector3()
            {
                X = Uteis.ArrumaVertice(0m, prateleira.Largura),
                Y = (float) ((prateleira.Altura / 100m) + 1m),
                Z = (float) ((prateleira.Profundidade / 100m) + 1m)
            },
            Length = 3,
            Dir = new Vector3(0,0,-1)
        });
        return retorno;
    }

    private IEnumerable<CaixaBlazor> CriaCaixas(Prateleira prateleira)
    {
        return 
        (
            from caixa in prateleira.Caixas 
            let cor = $"#{rnd.Next(255):X2}{rnd.Next(255):X2}{rnd.Next(255):X2}" 
            select new CaixaBlazor(caixa, cor)
        )
        .ToList();
    }

    private Camera CriaCamera(Prateleira prateleira)
    {
        var posicao = new Vector3()
        {
            X = Uteis.ArrumaVertice(0m, prateleira.Largura),
            Y = Uteis.ArrumaVertice(0m, prateleira.Altura),
            Z = (float)((prateleira.Profundidade / 100m) + 3m)
        };
        var olharPara = new Vector3()
        {
            X = posicao.X,
            Y = posicao.Y,
            Z = 0
        };
        var camera = new PerspectiveCamera
        {
            Position = posicao,
            LookAt = olharPara
        };
        return camera;
    }
}

public class DadosTela
{
    public required Camera Camera { get; init; }
    public required IEnumerable<CaixaBlazor> Caixas { get; init; }
    public required IEnumerable<Object3D> Adicionais { get; init; }
}