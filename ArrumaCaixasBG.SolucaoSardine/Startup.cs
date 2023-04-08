using ArrumaCaixasBG.Dominio.Interfaces;
using ArrumaCaixasBG.SolucaoSardine;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection;

public static class Startup
{
    public static IServiceCollection AddSolucoesSardine(this IServiceCollection services, IConfiguration configuracao)
    {
        services.Configure<ConfiguracaoXML>(configuracao.GetSection(nameof(ConfiguracaoXML)));
        services.AddTransient<ISolucaoOrganizador, SolucaoOrganizadorSardine>();
        return services;
    }
}