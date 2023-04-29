using ArrumaCaixasBG.Dominio;
using ArrumaCaixasBG.Dominio.Interfaces;
using Microsoft.Extensions.Options;

namespace ArrumaCaixasBG.DadosCSV;

internal class RepositorioCaixas : RepositorioCSV<Caixa, CaixaInterna>, IRepositorioCaixas
{
    public RepositorioCaixas(IOptions<ConfiguracaoCSV> configuracao, IOrigemDados origem) : base(configuracao, origem)
    {
    }

    protected override Caixa Converter(CaixaInterna registroInterno)
    {
        return new Caixa
        {
            Altura = registroInterno.Altura ?? 0,
            Comprimento = registroInterno.Comprimento ?? 0,
            Largura = registroInterno.Largura ?? 0,
            Nome = registroInterno.Nome,
            Peso = registroInterno.Peso ?? 0
        };
    }

    protected override string NomeArquivo()
    {
        return Constantes.IdCaixas;
    }
}