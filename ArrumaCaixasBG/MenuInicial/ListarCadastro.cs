using ArrumaCaixasBG.DadosCSV;
using ArrumaCaixasBG.Dominio.Interfaces;
using ASB.Console;
using Microsoft.Extensions.Options;

namespace ArrumaCaixasBG.MenuInicial;

internal class ListarCadastro : IMenuInicial
{
    private readonly IFuncoesConsole console;
    private readonly IRepositorioCaixas repositorioCaixas;
    private readonly IRepositorioPrateleiras repositorioPrateleiras;
    private readonly IOptions<ConfiguracaoCSV> cfgCSV;

    public ListarCadastro(IFuncoesConsole console,
                          IRepositorioCaixas repositorioCaixas,
                          IRepositorioPrateleiras repositorioPrateleiras,
                          IOptions<ConfiguracaoCSV> cfgCSV)
    {
        this.console = console;
        this.repositorioCaixas = repositorioCaixas;
        this.repositorioPrateleiras = repositorioPrateleiras;
        this.cfgCSV = cfgCSV;
    }

    public void Executar()
    {
        var caixas = repositorioCaixas.LerTodos();

        console.MostrarDisplay("--- Configurações ---", true);
        console.MostrarDisplay($"Pasta com os Dados: {cfgCSV.Value.PastaDados}", true);
        console.MostrarDisplay($"Separador: {cfgCSV.Value.Separador}", true);
        console.MostrarDisplay($"Formato dos Dados no arquivo: {cfgCSV.Value.FormatoDados}", true);

        console.MostrarDisplay("--- Caixas ---", true);
        foreach (var caixa in caixas)
        {
            console.MostrarDisplay(caixa.ToString(), true);
        }
        console.MostrarDisplay("", true);
        var prateleiras = repositorioPrateleiras.LerTodos();
        console.MostrarDisplay("--- Prateleiras ---", true);
        foreach (var prateleira in prateleiras)
        {
            console.MostrarDisplay(prateleira.ToString(), true);
        }
        console.LerTexto("Aperte Enter");
    }

    public override string ToString()
    {
        return "Listar Cadastros";
    }
}