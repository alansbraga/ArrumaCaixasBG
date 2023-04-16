using ArrumaCaixasBG.Dominio;
using System.Text.Json;
using Blazor3D.Maths;
using System.Buffers.Text;

namespace ArrumaCaixasBG.Blazor.Codigo;
public static class Uteis
{
    private const string jsonString = """""
    {
      "Nome": "Exemplo",
      "Altura": 295,
      "Largura": 424,
      "Ordem": 5000,
      "Profundidade": 420,
      "Volume": 52533600,
      "VolumeNaoUtilizado": 3498595,
      "Caixas": [
        {
          "X": 0,
          "Y": 0,
          "Z": 0,
          "Altura": 75,
          "Largura": 297,
          "Comprimento": 297,
          "Peso": 2,
          "Nome": "Robinson Cruso\u00E9: Aventuras na Ilha Amaldi\u00E7oada",
          "Volume": 6615675
        },
        {
          "X": 0,
          "Y": 75,
          "Z": 0,
          "Altura": 75,
          "Largura": 297,
          "Comprimento": 297,
          "Peso": 1.67,
          "Nome": "Ganges",
          "Volume": 6615675
        },
        {
          "X": 0,
          "Y": 150,
          "Z": 0,
          "Altura": 75,
          "Largura": 297,
          "Comprimento": 297,
          "Peso": 1.4,
          "Nome": "Catan: O Jogo (2015)",
          "Volume": 6615675
        },
        {
          "X": 0,
          "Y": 225,
          "Z": 0,
          "Altura": 70,
          "Largura": 363,
          "Comprimento": 300,
          "Peso": 3.76,
          "Nome": "Gaia Project",
          "Volume": 7623000
        },
        {
          "X": 0,
          "Y": 0,
          "Z": 300,
          "Altura": 295,
          "Largura": 295,
          "Comprimento": 73,
          "Peso": 1.08,
          "Nome": "Colt Express",
          "Volume": 6352825
        },
        {
          "X": 297,
          "Y": 0,
          "Z": 0,
          "Altura": 215,
          "Largura": 84,
          "Comprimento": 344,
          "Peso": 1.2,
          "Nome": "Ascension: Deckbuilding Game ",
          "Volume": 6212640
        },
        {
          "X": 0,
          "Y": 0,
          "Z": 373,
          "Altura": 263,
          "Largura": 310,
          "Comprimento": 47,
          "Peso": 0.5,
          "Nome": "Dead Men Tell No Tales (Brazilian first edition) (2016)",
          "Volume": 3831910
        },
        {
          "X": 381,
          "Y": 0,
          "Z": 0,
          "Altura": 149,
          "Largura": 41,
          "Comprimento": 205,
          "Peso": 0.64,
          "Nome": "Cart\u00F3grafos",
          "Volume": 1252345
        },
        {
          "X": 381,
          "Y": 0,
          "Z": 205,
          "Altura": 205,
          "Largura": 41,
          "Comprimento": 150,
          "Peso": 0.23,
          "Nome": "Catan 6 Jog",
          "Volume": 1260750
        },
        {
          "X": 381,
          "Y": 149,
          "Z": 0,
          "Altura": 110,
          "Largura": 43,
          "Comprimento": 202,
          "Peso": 0.37,
          "Nome": "Three Cheers for Master (2015)",
          "Volume": 955460
        },
        {
          "X": 381,
          "Y": 205,
          "Z": 202,
          "Altura": 72,
          "Largura": 33,
          "Comprimento": 105,
          "Peso": 0.1,
          "Nome": "Bandido",
          "Volume": 249480
        },
        {
          "X": 310,
          "Y": 0,
          "Z": 355,
          "Altura": 72,
          "Largura": 102,
          "Comprimento": 35,
          "Peso": 0.2,
          "Nome": "Taco Gato Cabra Queijo Pizza",
          "Volume": 257040
        },
        {
          "X": 310,
          "Y": 72,
          "Z": 355,
          "Altura": 154,
          "Largura": 108,
          "Comprimento": 40,
          "Peso": 0.4,
          "Nome": "Bohnanza: Por um Punhado de Feij\u00F5es!",
          "Volume": 665280
        },
        {
          "X": 310,
          "Y": 226,
          "Z": 307,
          "Altura": 35,
          "Largura": 73,
          "Comprimento": 102,
          "Peso": 0.166,
          "Nome": "Oh My Goods!: Revolta em Longsdale ",
          "Volume": 260610
        },
        {
          "X": 310,
          "Y": 261,
          "Z": 307,
          "Altura": 30,
          "Largura": 101,
          "Comprimento": 88,
          "Peso": 0.1,
          "Nome": "Telma",
          "Volume": 266640
        }
      ]
    }
    """"";
    public static Prateleira Prateleira()
    {
       var retorno = JsonSerializer.Deserialize<Prateleira>(jsonString);
       return retorno;
    }

    public static Arrumacao? CriaArrumacao(string conteudo)
    {
        var retorno = JsonSerializer.Deserialize<Arrumacao>(conteudo);
        return retorno;
    }

    public static Vector3 ArrumaPosicao(CaixaArrumada caixa)
    {
        return new Vector3
        {
            X = ArrumaVertice(caixa.X, caixa.Largura),
            Y = ArrumaVertice(caixa.Y, caixa.Altura),
            Z = ArrumaVertice(caixa.Z, caixa.Comprimento)
        };

    }

    public static float ArrumaVertice(decimal valor, decimal tamanho)
    {
        var novo = valor / 100;
        var novoTamanho = tamanho /2 / 100;
        novo += novoTamanho;
        return (float)novo;
    }

}

public class Arrumacao
{
    public string Nome { get; set; }
    public IEnumerable<Prateleira> Prateleiras { get; set; }
}