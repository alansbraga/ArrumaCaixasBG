using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArrumaCaixasBG.SolucaoAPI;
internal class AcessaAPIEspera
{
    public static ICollection<JsonSolutionContainer> Calcular(JsonJob corpo)
    {
        var httpClient = new HttpClient();
        var client = new ArrumaCaixaClient(
                 "http://localhost:4550/",
                 httpClient);

        var ret = client.CalculationsAsync(corpo)
            .GetAwaiter()
            .GetResult();

        while (true)
        {
            Thread.Sleep(2000);
            var status = client.StatusAsync(ret.Id)
            .GetAwaiter()
            .GetResult();

            if (status.Status == StatusCodes.ERROR)
                throw new Exception(status.ErrorMessage);

            if (status.Status == StatusCodes.DONE)
                break;
        }

        var calculado = client.SolutionAsync(ret.Id)
            .GetAwaiter()
            .GetResult();
        return calculado.Containers;

    }
}
