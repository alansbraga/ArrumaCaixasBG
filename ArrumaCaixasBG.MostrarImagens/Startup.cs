
using Microsoft.Extensions.Configuration;
using ArrumaCaixasBG.Dominio.Interfaces;
using ArrumaCaixasBG.MostrarImagens;

namespace Microsoft.Extensions.DependencyInjection;

public static class Startup
{
    public static IServiceCollection AddArrumaCaixaBGMostrarEmImagens(this IServiceCollection services, IConfiguration configuracao)
    {
        services.Configure<ConfiguracaoImagem>(configuracao.GetSection(nameof(ConfiguracaoImagem)));
        services.AddTransient<IMostrarResultado, MostrarResultadoEmImagens>();

        return services;
    }
}
