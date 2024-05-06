using System.Data;

namespace ProjetoLimpeza.Repositorios.Interfaces
{
    public interface IDapperContext
    {
        IDbConnection CreateConnection();
    }
}
