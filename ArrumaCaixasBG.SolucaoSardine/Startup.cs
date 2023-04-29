using ArrumaCaixasBG.Dominio.Interfaces;
using ArrumaCaixasBG.SolucaoSardine;
using Microsoft.Extensions.Configuration;
using SC.Heuristics.PrimalHeuristic;
using SC.ObjectModel;

namespace Microsoft.Extensions.DependencyInjection;

public static class Startup
{
    public static IServiceCollection AddSolucoesSardine(this IServiceCollection services, IConfiguration configuracao)
    {
        services.Configure<ConfiguracaoXML>(configuracao.GetSection(nameof(ConfiguracaoXML)));
        services.AddTransient<ISolucaoOrganizador>(s =>
            new SolucaoOrganizadorSardine(
                "Sardine Ponto Extremo",
                MethodType.ExtremePointInsertion, 
                (instance, cfg) => new ExtremePointInsertionHeuristic(instance, cfg)));
        services.AddTransient<ISolucaoOrganizador>(s =>
            new SolucaoOrganizadorSardine(
                "Sardine Empurrar",
                MethodType.PushInsertion, 
                (instance, cfg) => new PushInsertion(instance, cfg)));
        services.AddTransient<ISolucaoOrganizador>(s =>
            new SolucaoOrganizadorSardine(
                "Sardine Desfragmentação",
                MethodType.SpaceDefragmentation, 
                (instance, cfg) => new SpaceDefragmentationHeuristic(instance, cfg)));
        
        return services;
    }
}