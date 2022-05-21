using ArrumaCaixasBG.Dominio.Interfaces;
using ArrumaCaixasBG.SolucaoAPI;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.Extensions.DependencyInjection;

public static class Startup
{
    public static IServiceCollection AddSolucoesAPI(this IServiceCollection services)
    {
        services.AddTransient<ISolucaoOrganizador, SolucaoOrganizadorAPI>();
        return services;
    }
}