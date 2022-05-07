using ArrumaCaixasBG;
using ArrumaCaixasBG.MenuInicial;
using ASB.Console;
using Microsoft.Extensions.DependencyInjection;

var console = new ASBConsole();
console
    .AdicionaConfiguracao(args)
    .AdicionaInjecaoDependencia((services, configuracao) =>
    {
        services.AddASBConsole();
        services.AddTransient<ControlePrincipal>();

        services.AddTransient<IMenuInicial, ListarCadastro>();
    })
    .AdicionaLog()
    .Iniciar<ControlePrincipal>(false);
    