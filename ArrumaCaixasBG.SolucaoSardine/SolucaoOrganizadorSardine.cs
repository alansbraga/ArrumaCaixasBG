﻿using ArrumaCaixasBG.Dominio;
using ArrumaCaixasBG.Dominio.Interfaces;
using SC.Heuristics.PrimalHeuristic;
using SC.Linear;
using SC.ObjectModel;
using SC.ObjectModel.Configuration;
using SC.ObjectModel.Elements;
using SC.ObjectModel.Interfaces;
using Microsoft.Extensions.Options;

namespace ArrumaCaixasBG.SolucaoSardine;
internal class SolucaoOrganizadorSardine : ISolucaoOrganizador
{
    private readonly Dictionary<int, Caixa> caixaComId = new();
    //private readonly IOptions<ConfiguracaoXML> configuracao;
    private readonly MethodType tipoMetodo;
    private readonly Func<Instance, Configuration, IMethod> fabrica;

    public SolucaoOrganizadorSardine(/*IOptions<ConfiguracaoXML> configuracao,*/
        string nome,
        MethodType tipoMetodo, Func<Instance, Configuration, IMethod> fabrica)
    {
        //this.configuracao = configuracao;
        this.Nome = nome;
        this.tipoMetodo = tipoMetodo;
        this.fabrica = fabrica;
    }

    public string Nome { get; }

    public ResultadoOrganizacao Arrumar(IEnumerable<Caixa> caixas, Prateleira prateleira)
    {
        var retorno = new List<Prateleira>();
        var metodos = CrarMetodos(caixas, prateleira);

        foreach (var metodo in metodos)
        {
            _ = metodo.Run();
            if (metodo.HasSolution)
            {
                ConverteSolucao(prateleira, retorno, metodo.Solution);
                //var caminho = Path.Combine(configuracao.Value.PastaDestino, 
                //    @$"{prateleira.Nome}-{DateTime.Now:yyyy-MM-dd-hh-mm-ss-fff}.xinst");
                //metodo.Solution.InstanceLinked.WriteXML(caminho);
            }
                
        }

        return new ResultadoOrganizacao(Nome, retorno, Enumerable.Empty<Caixa>());
    }

    private void ConverteSolucao(Prateleira prateleira, List<Prateleira> retorno, COSolution solution)
    {
        if (solution is null)
            return;

        

        foreach (var prateleiraSol in solution.InstanceLinked.Containers.OrderByDescending(c => solution.ContainerContent[c.VolatileID].Sum(p => p.Original.Components.Sum(com => com.Volume))))
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
            foreach (var piece in solution.ContainerContent[prateleiraSol.VolatileID])
            {
                var caixaOriginal = caixaComId[piece.ID];
                var orientation = solution.Orientations[piece.VolatileID];
                var componente = piece[orientation].Components.First();
                var posicao = solution.Positions[piece.VolatileID];
                var novaCaixa = new CaixaArrumada
                {
                    X = (decimal)(posicao.Y),
                    Y = (decimal)(posicao.Z),
                    Z = (decimal)(posicao.X),
                    Nome = caixaOriginal.Nome,
                    Comprimento = (decimal)componente.Length,
                    Altura = (decimal)componente.Height,
                    Largura = (decimal)componente.Width,
                    Peso = caixaOriginal.Peso
                };
                listaArrumada.Add(novaCaixa);
            }
            prateleiraAtual.SubstituiCaixas(listaArrumada);
            retorno.Add(prateleiraAtual);
        }

    }


    private IEnumerable<IMethod> CrarMetodos(IEnumerable<Caixa> caixas, Prateleira prateleira)
    {
        var lista = new List<IMethod>();

        void CriaMetodo(MethodType tipoMetodoLocal, Func<Instance, Configuration, IMethod> instancia)
        {
            var cfg = new Configuration(tipoMetodoLocal, true)
            {
                //SolverToUse = solver
            };
            var instance = CrarInstance(caixas, prateleira);
            var metodo = instancia(instance, cfg);
            lista.Add(metodo);

        }
        CriaMetodo(tipoMetodo, fabrica);

        return lista;
    }

    private Instance CrarInstance(IEnumerable<Caixa> caixas, Prateleira prateleira)
    {
        var instance = new Instance
        {
            Name = "Tudo"
        };
        instance.Containers.Add(new Container()
        {
            Mesh = new MeshCube()
            {
                Length = (int)prateleira.Profundidade,
                Height = (int)prateleira.Altura,
                Width = (int)prateleira.Largura,
            },
            ID = 1
        });

        caixaComId.Clear();
        var seq = 1;
        foreach (var caixa in caixas.OrderBy(c => c.Nome))
        {
            caixaComId.Add(seq, caixa);

            var peca = new VariablePiece()
            {
                Original = new()
                {
                    Components = new()
                },
                ID = seq,
            };
            peca.Original.Components.Add(new MeshCube
            {
                Length = (int)caixa.Comprimento,
                Height = (int)caixa.Altura,
                Width = (int)caixa.Largura,
            });

            instance.Pieces.Add(peca);
            seq++;
        }

        foreach (var c in instance.Containers)
        {
            c.Seal();
        }
        foreach (var c in instance.Pieces)
        {
            c.Seal();
        }

        return instance;
    }
}
