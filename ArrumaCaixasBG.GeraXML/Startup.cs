using ArrumaCaixasBG.Dominio.Interfaces;
using ArrumaCaixasBG.GeraXML;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.Extensions.DependencyInjection;

public static class Startup
{
    public static IServiceCollection AddSolucoesXML(this IServiceCollection services)
    {
        services.AddTransient<ISolucaoOrganizador, SolucaoGerarXML>();
        return services;
    }
}
