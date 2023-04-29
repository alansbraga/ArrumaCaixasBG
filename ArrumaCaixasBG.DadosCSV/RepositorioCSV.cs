using ArrumaCaixasBG.Dominio.Interfaces;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.Extensions.Options;
using System.Globalization;

namespace ArrumaCaixasBG.DadosCSV;

internal abstract class RepositorioCSV<T, TInterno> : IRepositorio<T>
{
    private readonly IOptions<ConfiguracaoCSV> configuracao;
    private readonly IOrigemDados origemDados;

    protected RepositorioCSV(IOptions<ConfiguracaoCSV> configuracao, IOrigemDados origemDados)
    {
        this.configuracao = configuracao;
        this.origemDados = origemDados;
    }

    protected abstract string NomeArquivo();
    protected abstract T Converter(TInterno registroInterno);


    public IEnumerable<T> LerTodos()
    {
        var cultura = CultureInfo.GetCultureInfo(configuracao.Value.FormatoDados);
        using var stream = origemDados.LerDados(NomeArquivo());
        using var reader = new StreamReader(stream);
        using var csv = new CsvReader(reader, new CsvConfiguration(cultura)
        {
            Delimiter = configuracao.Value.Separador
        });
        var registros = csv.GetRecords<TInterno>().ToList();
        return registros.Select(Converter);
    }
}
