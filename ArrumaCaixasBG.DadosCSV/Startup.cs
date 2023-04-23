using ArrumaCaixasBG.DadosCSV;
using ArrumaCaixasBG.Dominio.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection;

public static class Startup
{
    public static IServiceCollection AddDadosCSV(this IServiceCollection services, IConfiguration configuracao)
    {
        services.AddDadosCSVGenerico(configuracao);
        services.AddTransient<IOrigemDados, OrigemDadosArquivo>();
        return services;
    }

    internal static IServiceCollection AddDadosCSVGenerico(this IServiceCollection services, IConfiguration configuracao)
    {
        services.Configure<ConfiguracaoCSV>(configuracao.GetSection(nameof(ConfiguracaoCSV)));
        
        services.AddTransient<IRepositorioCaixas, RepositorioCaixas>();
        services.AddTransient<IRepositorioPrateleiras, RepositorioPrateleiras>();
        return services;
    }

    public static IServiceCollection AddDadosCSVWeb(this IServiceCollection services, IConfiguration configuracao)
    {
        services.AddDadosCSVGenerico(configuracao);
        services.AddScoped<IOrigemDados, OrigemdadosWeb>();
        return services;
    }

}
