using ArrumaCaixasBG.Dominio.Interfaces;
using ArrumaCaixasBG.Properties;

namespace ArrumaCaixasBG.MenuInicial;

internal class OrganizarCadastrados : IMenuInicial
{
    private readonly IOrganizadorCaixas organizador;

    public OrganizarCadastrados(IOrganizadorCaixas organizador)
    {
        this.organizador = organizador;
    }

    public void Executar()
    {
        var ret = organizador.Organizar();
        Console.WriteLine(ret);
        Console.ReadLine();
    }

    public override string ToString()
    {
        return Resources.OrganizarCadastrados;
    }
}