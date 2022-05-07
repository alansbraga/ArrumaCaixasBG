using ArrumaCaixasBG.Dominio;
using ArrumaCaixasBG.Dominio.Interfaces;
using Microsoft.Extensions.Options;

namespace ArrumaCaixasBG.DadosCSV;

internal class RepositorioPrateleiras : RepositorioCSV<Prateleira, PrateleiraInterna>, IRepositorioPrateleiras
{
    public RepositorioPrateleiras(IOptions<ConfiguracaoCSV> configuracao) : base(configuracao)
    {
    }

    protected override Prateleira Converter(PrateleiraInterna registroInterno)
    {
        return new Prateleira
        {
            Altura = registroInterno.Altura,
            Largura = registroInterno.Largura,
            Nome = registroInterno.Nome,
            Profundidade = registroInterno.Profundidade
        };
    }

    protected override string NomeArquivo()
    {
        return "Prateleiras.csv";
    }
}