using ArrumaCaixasBG.Dominio.Interfaces;
using ArrumaCaixasBG.Properties;

namespace ArrumaCaixasBG.MenuInicial;

internal class OrganizarCadastrados : IMenuInicial
{
    private readonly IOrganizadorCaixas organizador;
    private readonly IMostrarResultado mostrarResultado;

    public OrganizarCadastrados(IOrganizadorCaixas organizador, IMostrarResultado mostrarResultado)
    {
        this.organizador = organizador;
        this.mostrarResultado = mostrarResultado;
    }

    public void Executar()
    {
        var ret = organizador.Organizar();
        mostrarResultado.Mostrar(ret);
        Console.WriteLine("Fim");
        Console.ReadLine();
    }

    public override string ToString()
    {
        return Resources.OrganizarCadastrados;
    }
}