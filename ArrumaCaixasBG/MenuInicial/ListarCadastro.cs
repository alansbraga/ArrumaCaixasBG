using ASB.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArrumaCaixasBG.MenuInicial;

internal class ListarCadastro : IMenuInicial
{
    private readonly IFuncoesConsole console;

    public ListarCadastro(IFuncoesConsole console)
    {
        this.console = console;
    }

    public void Executar()
    {
        console.LerTexto("aaaa");
    }
}