﻿using ArrumaCaixasBG.Dominio;
using ArrumaCaixasBG.Dominio.Interfaces;
using Microsoft.Extensions.Options;

namespace ArrumaCaixasBG.DadosCSV;

internal class RepositorioPrateleiras : RepositorioCSV<Prateleira, PrateleiraInterna>, IRepositorioPrateleiras
{
    public RepositorioPrateleiras(IOptions<ConfiguracaoCSV> configuracao, IOrigemDados origem) : base(configuracao, origem)
    {
    }

    protected override Prateleira Converter(PrateleiraInterna registroInterno)
    {
        return new Prateleira
        {
            Altura = registroInterno.Altura,
            Largura = registroInterno.Largura,
            Nome = registroInterno.Nome,
            Profundidade = registroInterno.Profundidade,
            Ordem = registroInterno.Ordem == 0 ? 5000 : registroInterno.Ordem
        };
    }

    protected override string NomeArquivo()
    {
        return Constantes.IdPrateleiras;
    }
}