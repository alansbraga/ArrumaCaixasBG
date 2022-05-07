namespace ArrumaCaixasBG.Dominio.Interfaces;

public interface IRepositorio<T>
{
    IEnumerable<T> LerTodos();
}

