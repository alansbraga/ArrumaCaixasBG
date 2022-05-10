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
        var rnd = new Random();
        foreach (var prateleira in resultado.Prateleiras)
        {
            using var imagem = new Bitmap((int)prateleira.Largura + 2, (int)prateleira.Altura + 2);
            var yMax = (int)(imagem.Height - 1);
            foreach (var caixaAgrupada in prateleira.Caixas.GroupBy(c => (int)c.Z))
            {

                foreach (var caixa in caixaAgrupada)
                {
                    var cor = Color.FromArgb(rnd.Next(255), rnd.Next(255), rnd.Next(255));
                    for (var x = (int)caixa.X; x < (int)(caixa.X + caixa.Largura); x++)
                    {
                        for (var y = (int)caixa.Y; y < (int)(caixa.Y + caixa.Altura); y++)
                        {
                            var zImagem = yMax - y;
                            imagem.SetPixel(x, zImagem, cor);
                        }
                    }
                }

                imagem.Save($@"c:\tmp\ImagemPrateleira\{prateleira.Nome}-{caixaAgrupada.Key}.bmp");
            }

        }


    }
}