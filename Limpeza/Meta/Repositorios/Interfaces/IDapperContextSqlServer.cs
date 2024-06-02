using System.Data.Common;
using TarefasPDV.Meta.Models;

namespace TarefasPDV.Meta.Repositorios.Interfaces
{
    public interface IDapperContextSqlServer
    {
        DbConnection CreateConnection();
        string GetConnectionString();
    }
}