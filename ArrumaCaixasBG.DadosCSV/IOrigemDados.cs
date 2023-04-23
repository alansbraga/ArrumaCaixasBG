using Microsoft.Extensions.Options;

namespace ArrumaCaixasBG.DadosCSV;

public interface IOrigemDados
{
    Stream LerDados(string id);
    void DefinePrateleiras(byte[] stream);
    void DefineCaixas(byte[] stream);
}

class OrigemDadosArquivo : IOrigemDados
{
    private readonly IOptions<ConfiguracaoCSV> configuracao;

    public OrigemDadosArquivo(IOptions<ConfiguracaoCSV> configuracao)
    {
        this.configuracao = configuracao;
    }
    public Stream LerDados(string id)
    {
        return new FileStream(Path.Combine(configuracao.Value.PastaDados, id), FileMode.Open);
    }

    public void DefinePrateleiras(byte[] stream)
    {
        throw new NotImplementedException();
    }

    public void DefineCaixas(byte[] stream)
    {
        throw new NotImplementedException();
    }
}

internal class OrigemdadosWeb : IOrigemDados
{

    private readonly IDictionary<string, byte[]> cache = new Dictionary<string, byte[]>();

    public Stream LerDados(string id)
    {
        return cache.TryGetValue(id, out var dados) 
            ? new MemoryStream(dados)
            : Stream.Null;
    }

    public void DefinePrateleiras(byte[] stream)
    {
        cache[Constantes.IdPrateleiras] = stream;
    }

    public void DefineCaixas(byte[] stream)
    {
        cache[Constantes.IdCaixas] = stream;
    }
}