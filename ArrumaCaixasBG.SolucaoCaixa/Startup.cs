using ArrumaCaixasBG.Dominio.Interfaces;
using ArrumaCaixasBG.SolucaoCaixa;
using Sharp3DBinPacking.Algorithms;
using Sharp3DBinPacking.Internal;

namespace Microsoft.Extensions.DependencyInjection;

public static class Startup
{
    public static IServiceCollection AddSolucoesComoCaixa(this IServiceCollection services)
    {
        services.AddTransient<ISolucaoOrganizador>(s => 
            new SolucaoOrganizadorBinPacking(parameter => new BinPackShelfAlgorithm(
                parameter,
                FreeRectChoiceHeuristic.RectBestAreaFit,
                GuillotineSplitHeuristic.SplitLongerLeftoverAxis,
                ShelfChoiceHeuristic.ShelfFirstFit), "Prateleira Melhor Area / Primeiro Encaixe"));
        services.AddTransient<ISolucaoOrganizador>(s =>
            new SolucaoOrganizadorBinPacking(
                parameter => new BinPackShelfAlgorithm(
                    parameter,
                    FreeRectChoiceHeuristic.RectBestAreaFit,
                    GuillotineSplitHeuristic.SplitLongerLeftoverAxis,
                    ShelfChoiceHeuristic.ShelfNextFit),
                "Prateleira Melhor area / Próximo Encaixe"));
        services.AddTransient<ISolucaoOrganizador>(s =>
            new SolucaoOrganizadorBinPacking(
                parameter => new BinPackGuillotineAlgorithm(
                    parameter,
                    FreeCuboidChoiceHeuristic.CuboidMinHeight,
                    GuillotineSplitHeuristic.SplitLongerLeftoverAxis),
                "Guilhotina Menor altura Maior Sobra"));
        services.AddTransient<ISolucaoOrganizador>(s =>
            new SolucaoOrganizadorBinPacking(
                parameter => new BinPackGuillotineAlgorithm(
                    parameter,
                    FreeCuboidChoiceHeuristic.CuboidMinHeight,
                    GuillotineSplitHeuristic.SplitShorterLeftoverAxis),
                "Guilhotina Menor altura Menor Sobra"
                ));
        
        return services;
    }
}
