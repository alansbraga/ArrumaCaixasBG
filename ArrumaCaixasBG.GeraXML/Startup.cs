using ArrumaCaixasBG.Dominio.Interfaces;
using ArrumaCaixasBG.GeraXML;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.Extensions.DependencyInjection;

public static class Startup
{
    public static IServiceCollection AddSolucoesXML(this IServiceCollection services, IConfiguration configuracao)
    {
        services.Configure<ConfiguracaoXML>(configuracao.GetSection(nameof(ConfiguracaoXML)));
        services.AddTransient<ISolucaoOrganizador, SolucaoGerarXML>();
        return services;
    }
}
