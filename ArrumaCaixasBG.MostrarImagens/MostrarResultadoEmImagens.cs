using ArrumaCaixasBG.Dominio;
using ArrumaCaixasBG.Dominio.Interfaces;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Xml.Linq;

namespace ArrumaCaixasBG.MostrarImagens;

internal class MostrarResultadoEmImagens : IMostrarResultado
{
    string pastaSalvar = @"c:\tmp\ArrumaBG\ImagemPrateleira";
    Color[] cinzas = new Color[]
    {
        Color.Gray,
        Color.DarkGray,
        Color.DarkSlateGray,
        Color.DimGray,
        Color.LightGray,
        Color.LightSlateGray,
        Color.SlateGray
    };

    public void Mostrar(ResultadoOrganizacao resultado)
    {
        File.WriteAllText($@"{pastaSalvar}\arrumacao.json", JsonSerializer.Serialize(resultado));
        //Directory.Delete(pastaSalvar, true);
        //Directory.CreateDirectory(pastaSalvar);
        var cinzaAtual = 0;
        var lista = new List<CaixaCorPrateleira>();
        var rnd = new Random();
        foreach (var prateleira in resultado.Prateleiras)
        {
            using var imagem = new Bitmap((int)prateleira.Largura + 2, (int)prateleira.Altura + 2);
            using var imagemCinza = new Bitmap((int)prateleira.Largura + 2, (int)prateleira.Altura + 2);
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
                    var percVolume = (prateleira.VolumeNaoUtilizado / prateleira.Volume) * 100;
                    percVolume = Math.Round(100 - percVolume, 2);
                    lista.Add(new CaixaCorPrateleira(prateleira.Nome, caixaAgrupada.Key, caixa.Nome, cor, percVolume));
                    for (var x = (int)caixa.X; x < (int)(caixa.X + caixa.Largura); x++)
                    {
                        for (var y = (int)caixa.Y; y < (int)(caixa.Y + caixa.Altura); y++)
                        {


                            var yImagem = yMax - y;
                            
                            imagem.SetPixel(x, yImagem, cor);
                            imagemCinza.SetPixel(x, yImagem, cinzas[cinzaAtual]);
                        }
                    }
                    cinzaAtual++;
                    if (cinzaAtual >= cinzas.Count())
                        cinzaAtual = 0;
                }
                imagem.Save($@"{pastaSalvar}\{prateleira.Nome}-{caixaAgrupada.Key}.bmp");
                CopiaImagem(imagem, imagemCinza);
            }

        }
        CriaHtml(resultado, lista);


    }

    private void CopiaImagem(Bitmap imagem, Bitmap imagemCinza)
    {
        for (int x = 0; x < imagemCinza.Width; x++)
        {
            for (int y = 0; y < imagem.Height; y++)
            {
                imagem.SetPixel(x, y, imagemCinza.GetPixel(x, y));
            }
        }
    }

    private void CriaHtml(ResultadoOrganizacao resultado, List<CaixaCorPrateleira> lista)
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


        if (resultado.CaixasSobrando.Any())
        {
            sb.AppendLine("<h1>Caixas Sobrando</h1>");

            sb.AppendLine("<ul>");
            foreach (var caixa in resultado.CaixasSobrando)
            {
                sb.Append("<li>");
                sb.AppendLine(caixa.Nome);
            }
            sb.AppendLine("</ul>");
        }

        var prateleiras = lista.GroupBy(l => new { l.Prateleira, l.PercentualUsado });

        foreach (var prateleira in prateleiras)
        {
            sb.AppendLine($"<h1>{prateleira.Key.Prateleira}</h1>");

            var percVolume = prateleira.Key.PercentualUsado;
            sb.AppendLine($"<p>Volume Utilizado: {percVolume} %</p>");

            var passos = prateleira.GroupBy(p => p.Posicao)
                .OrderBy(p => p.Key);
            var numPasso = 0;
            foreach (var passo in passos)
            {
                numPasso++;
                sb.AppendLine($"<h3>{numPasso}</h3>");
                sb.AppendLine("<div class=\"passo\">");
                sb.AppendLine($"<div class=\"imagem\">");
                sb.AppendLine($"<img src=\"{prateleira.Key.Prateleira}-{passo.Key}.bmp\" />");
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
        File.WriteAllText($@"{pastaSalvar}\arrumacao.html", sb.ToString());
        File.WriteAllText($@"{pastaSalvar}\arrumacao.json", JsonSerializer.Serialize(resultado));
    }

    private static string HexConverter(Color c)
    {
        return "#" + c.R.ToString("X2") + c.G.ToString("X2") + c.B.ToString("X2");
    }
}

internal record CaixaCorPrateleira(string Prateleira, int Posicao, string Caixa, Color Cor, decimal PercentualUsado);
