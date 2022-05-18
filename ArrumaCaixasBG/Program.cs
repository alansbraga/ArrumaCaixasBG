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
        services.AddArrumaCaixaBGDominio();
        services.AddDadosCSV(configuracao);
        //services.AddSolucoesComoCaixa();
        services.AddSolucoesXML();
        services.AddArrumaCaixaBGMostrarEmImagens();

        services.AddTransient<ControlePrincipal>();

        services.AddTransient<IMenuInicial, ListarCadastro>();
        services.AddTransient<IMenuInicial, OrganizarCadastrados>();
    })
    .AdicionaLog()
    .Iniciar<ControlePrincipal>(false);
    