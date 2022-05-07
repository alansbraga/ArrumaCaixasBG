using ArrumaCaixasBG.MenuInicial;
using ArrumaCaixasBG.Properties;
using ASB.Console;

namespace ArrumaCaixasBG;

internal class ControlePrincipal : IConsoleController
{
    private readonly IFuncoesConsole console;
    private readonly IEnumerable<IMenuInicial> menus;

    public ControlePrincipal(IFuncoesConsole console, IEnumerable<IMenuInicial> menus)
    {
        this.console = console;
        this.menus = menus;
    }

    public void Dispose()
    {
        
    }

    public void Executar()
    {
        while (true)
        {
            console.LimparTela();
            var selecionado = console.SelecionarOpcao(Resources.SelecioneOpcao, menus);
            if (selecionado is null)
                break;

            selecionado.Executar();
        }

    }
}