using ArrumaCaixasBG.Dominio.Interfaces;
using ArrumaCaixasBG.SolucaoSardine;

namespace Microsoft.Extensions.DependencyInjection;

public static class Startup
{
    public static IServiceCollection AddSolucoesSardine(this IServiceCollection services)
    {
        services.AddTransient<ISolucaoOrganizador, SolucaoOrganizadorSardine>();
        return services;
    }
}