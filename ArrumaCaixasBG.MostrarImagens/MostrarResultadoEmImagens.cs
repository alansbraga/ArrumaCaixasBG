using ArrumaCaixasBG.Dominio;
using ArrumaCaixasBG.Dominio.Interfaces;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArrumaCaixasBG.MostrarImagens;

internal class MostrarResultadoEmImagens : IMostrarResultado
{
    public void Mostrar(ResultadoOrganizacao resultado)
    {
        var lista = new List<CaixaCorPrateleira>();
        var rnd = new Random();
        foreach (var prateleira in resultado.Prateleiras)
        {
            using var imagem = new Bitmap((int)prateleira.Largura + 2, (int)prateleira.Altura + 2);
            var yMax = (int)(imagem.Height - 1);
            var caixasAgrupadas = prateleira
                .Caixas
                .GroupBy(c => (int)c.Z)
                .OrderBy(c => c.Key);
            foreach (var caixaAgrupada in caixasAgrupadas)
            {

                foreach (var caixa in caixaAgrupada.OrderBy(c => c.X).ThenBy(c => c.Y))
                {
                    var cor = Color.FromArgb(rnd.Next(255), rnd.Next(255), rnd.Next(255));
                    lista.Add(new CaixaCorPrateleira(prateleira.Nome, caixaAgrupada.Key, caixa.Nome, cor));
                    for (var x = (int)caixa.X; x < (int)(caixa.X + caixa.Largura); x++)
                    {
                        for (var y = (int)caixa.Y; y < (int)(caixa.Y + caixa.Altura); y++)
                        {
                            var yImagem = yMax - y;
                            imagem.SetPixel(x, yImagem, cor);
                        }
                    }
                }
                imagem.Save($@"c:\tmp\ArrumaBG\ImagemPrateleira\{prateleira.Nome}-{caixaAgrupada.Key}.bmp");

                for (int x = 0; x < imagem.Width; x++)
                {
                    for (int y = 0; y < imagem.Height; y++)
                    {
                        var c = imagem.GetPixel(x, y);
                        var novoC = Color.FromArgb((int)(c.R * 0.299), (int)(c.G * 0.587), (int)(c.B * 0.114));
                        imagem.SetPixel(x, y, novoC);
                    }
                }
            }

        }
        CriaHtml(lista);


    }

    private void CriaHtml(List<CaixaCorPrateleira> lista)
    {
        var sb = new StringBuilder(@"<!DOCTYPE html><html><head>
<style>
.box {
  height: 20px;
  width: 20px;
  margin-bottom: 15px;
  border: 1px solid black;
}
.passo {
  display: flex;
}

.imagem {
  flex: 50%;
}
.lista {
  flex: 50%;
}
</style>

        </head><body>");

        var prateleiras = lista.GroupBy(l => l.Prateleira);

        foreach (var prateleira in prateleiras)
        {
            sb.AppendLine($"<h1>{prateleira.Key}</h1>");

            var passos = prateleira.GroupBy(p => p.Posicao)
                .OrderBy(p => p.Key);

            foreach (var passo in passos)
            {
                sb.AppendLine($"<h3>{passo.Key}</h3>");
                sb.AppendLine("<div class=\"passo\">");
                sb.AppendLine($"<div class=\"imagem\">");
                sb.AppendLine($"<img src=\"{prateleira.Key}-{passo.Key}.bmp\" />");
                sb.AppendLine("</div>");
                sb.AppendLine($"<div class=\"lista\">");
                //sb.AppendLine("<ul>");
                foreach (var item in passo.Select(p => p))
                {
                    sb.Append($"<div style=\"display: flex;\"><div class='box' style=\"background-color: {HexConverter(item.Cor)};\"></div> ");
                    sb.AppendLine(item.Caixa);
                    sb.AppendLine("</div><br>");
                }
                //sb.AppendLine("</ul>");
                sb.AppendLine("</div>");
                sb.AppendLine("</div>");
            }

        }

        sb.AppendLine("</body></html>");
        File.WriteAllText($@"c:\tmp\ArrumaBG\ImagemPrateleira\arrumacao.html", sb.ToString());
    }

    private static string HexConverter(Color c)
    {
        return "#" + c.R.ToString("X2") + c.G.ToString("X2") + c.B.ToString("X2");
    }
}

internal record CaixaCorPrateleira(string Prateleira, int Posicao, string Caixa, Color Cor);
