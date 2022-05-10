﻿
using ArrumaCaixasBG.Dominio.Interfaces;
using ArrumaCaixasBG.MostrarImagens;

namespace Microsoft.Extensions.DependencyInjection;

public static class Startup
{
    public static IServiceCollection AddArrumaCaixaBGMostrarEmImagens(this IServiceCollection services)
    {
        services.AddTransient<IMostrarResultado, MostrarResultadoEmImagens>();
     

        return services;
    }
}
