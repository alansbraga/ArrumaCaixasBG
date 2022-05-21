using ArrumaCaixasBG.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArrumaCaixasBG.SolucaoAPI;
internal class ConverteAPIPrateleira
{
    private readonly IEnumerable<Caixa> caixas;
    private readonly Prateleira prateleira;
    private readonly List<CaixaJob> caixasJob;

    public ConverteAPIPrateleira(IEnumerable<Caixa> caixas, Prateleira prateleira)
    {
        this.caixas = caixas;
        this.prateleira = prateleira;
        caixasJob = new List<CaixaJob>();
    }

    public JsonJob ConverteParaAPI()
    {
        var retorno = new JsonJob
        {
            Configuration = new()
            {
                BestFit = true,
                ExhaustiveEPProne = true,
                Goal = OptimizationGoal.MAXUTILIZATION,
                HandleGravity = true,
                HandleRotatability = true,
                HandleStackability = true,
                Improvement = true,
                Tetris = true
            },

            Instance = new()
            {
                Containers = CriaContainers(),
                Pieces = CriaCaixas(),
                Name = Guid.NewGuid().ToString()
            }
        };

        return retorno;
    }

    private ICollection<JsonPiece> CriaCaixas()
    {
        var atual = 1;
        var retorno = new List<JsonPiece>();

        foreach (var caixa in caixas)
        {
            var caixaJob = new CaixaJob(atual, caixa);
            caixasJob.Add(caixaJob);
            retorno.Add(caixaJob.Peca);
            atual++;
        }
        return retorno;
    }

    private ICollection<JsonContainer> CriaContainers()
    {
        var container = new JsonContainer()
        {
            Height = (int)prateleira.Altura,
            Id = 1,
            Length = (int)prateleira.Profundidade,
            Width = (int)prateleira.Largura
        };
        
        return new List<JsonContainer>
        {
            container
        };
    }
}

internal class CaixaJob
{
    public CaixaJob(int id, Caixa caixa)
    {
        Id = id;
        Caixa = caixa;
        Peca = new JsonPiece()
        {
            Cubes = new[]
            {
                new JsonCube()
                {
                    Height = (int)Caixa.Altura,
                    Length = (int)Caixa.Comprimento,
                    Width = (int)Caixa.Largura
                }
            },
            Id = Id,
            Flags = new JsonFlag[] { new() } 
        };

    }

    public int Id { get; }
    public Caixa Caixa { get; }
    public JsonPiece Peca { get; }
}
