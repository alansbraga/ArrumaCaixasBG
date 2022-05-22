using ArrumaCaixasBG.Dominio;
using ArrumaCaixasBG.Dominio.Interfaces;
using SC.Heuristics.PrimalHeuristic;
using SC.Linear;
using SC.ObjectModel;
using SC.ObjectModel.Configuration;
using SC.ObjectModel.Elements;
using SC.ObjectModel.Interfaces;

namespace ArrumaCaixasBG.SolucaoSardine;
internal class SolucaoOrganizadorSardine : ISolucaoOrganizador
{
    internal readonly Dictionary<int, Caixa> caixaComId = new();
    public IEnumerable<Prateleira> Arrumar(IEnumerable<Caixa> caixas, Prateleira prateleira)
    {
        var retorno = new List<Prateleira>();
        var metodos = CrarMetodos(caixas, prateleira);

        foreach (var metodo in metodos)
        {
            _ = metodo.Run();
            if (metodo.HasSolution)
            {
                ConverteSolucao(prateleira, retorno, metodo.Solution);
                metodo.Solution.InstanceLinked.WriteXML(@$"C:\tmp\ArrumaBG\xmls\{prateleira.Nome}-{DateTime.Now:yyyy-MM-dd-hh-mm-ss-fff}.xinst");
            }
                
        }

        return retorno;
    }

    private void ConverteSolucao(Prateleira prateleira, List<Prateleira> retorno, COSolution solution)
    {
        if (solution == null)
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

        void CriaMetodo(MethodType tipoMetodo, Func<Instance, Configuration, IMethod> instancia)
        {
            var cfg = new Configuration(tipoMetodo, true)
            {
                //SolverToUse = solver
            };
            var instance = CrarInstance(caixas, prateleira);
            var metodo = instancia(instance, cfg);
            lista.Add(metodo);

        }

        //foreach (var solver in new[] { Solvers.Gurobi, Solvers.Cplex })
        //{
            CriaMetodo(MethodType.ExtremePointInsertion, 
                (instance, cfg) => new ExtremePointInsertionHeuristic(instance, cfg));
            //CriaMetodo(MethodType.ALNS, solver,
            //    (instance, cfg) => new ALNS(instance, cfg));

            CriaMetodo(MethodType.PushInsertion, 
                (instance, cfg) => new PushInsertion(instance, cfg));
            CriaMetodo(MethodType.SpaceDefragmentation, 
                (instance, cfg) => new SpaceDefragmentationHeuristic(instance, cfg));
        //}


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
