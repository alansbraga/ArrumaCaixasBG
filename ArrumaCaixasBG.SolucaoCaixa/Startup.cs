using ArrumaCaixasBG.Dominio.Interfaces;
using ArrumaCaixasBG.SolucaoCaixa;

namespace Microsoft.Extensions.DependencyInjection;

public static class Startup
{
    public static IServiceCollection AddSolucoesComoCaixa(this IServiceCollection services)
    {
        services.AddTransient<ISolucaoOrganizador, SolucaoOrganizadorBinPacking>();
        return services;
    }
}
