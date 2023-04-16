using ArrumaCaixasBG.Dominio;
using Blazor3D.Core;
using Blazor3D.Geometires;
using Blazor3D.Materials;
using Blazor3D.Objects;

namespace ArrumaCaixasBG.Blazor.Codigo;

public class CaixaBlazor
{
    private readonly CaixaArrumada caixa;

    public CaixaArrumada Caixa => caixa;
    public string Nome => Objeto3D.Name;

    public string Cor { get; set; }

    public CaixaBlazor(CaixaArrumada caixa, string cor)
    {
        this.Cor = cor;
        this.caixa = caixa;
        this.Objeto3D = CriaObjeto();
    }

    public Object3D Objeto3D { get; set; }

    private Object3D CriaObjeto()
    {
        var material = new MeshStandardMaterial
        {
            Color = Cor
        };

        return new Mesh
        {
            Geometry = new BoxGeometry
            {
                Width = (float)(caixa.Largura / 100m),
                Height = (float)(caixa.Altura / 100m),
                Depth = (float)(caixa.Comprimento / 100m)
            },
            Material = material,
            Position = Uteis.ArrumaPosicao(caixa),
            Name = caixa.Nome
        };
    }
}