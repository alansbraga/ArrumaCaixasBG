using ArrumaCaixasBG.Dominio.Interfaces;
using ArrumaCaixasBG.Dominio;

namespace Microsoft.Extensions.DependencyInjection;

public static class Startup
{
    public static IServiceCollection AddArrumaCaixaBGDominio(this IServiceCollection services)
    {
        services.AddTransient<IOrganizadorCaixas, OrganizadorCaixas>();

        return services;
    }

    public static IServiceCollection AddArrumaCaixasInterativas(this IServiceCollection services)
    {
        services.AddScoped<IOrganizacaoInterativa, OrganizacaoInterativa>();
        return services;
    }
}