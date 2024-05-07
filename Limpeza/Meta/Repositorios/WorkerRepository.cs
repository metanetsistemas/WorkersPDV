using Dapper;
using Limpeza.Meta.Repositorios.BancoDados;
using Limpeza.Meta.Repositorios.Interfaces;

namespace Limpeza.Meta.Repositorios
{
    public class WorkerRepository : IWorkerRepository
    {
        private readonly IDapperContext _context;

        public WorkerRepository(IDapperContext context)
        {
            _context = context;
        }

        public void InsertWorkerExecution(int workerId, DateTime lastExecutionTime)
        {
            using (var connection = _context.CreateConnection())
            {
                connection.Execute("INSERT INTO Workers (Id, LastExecutionTime) VALUES (@Id, @LastExecutionTime)",
                                   new { Id = workerId, LastExecutionTime = lastExecutionTime });
            }
        }

        public DateTime? GetLastExecutionTime(int workerId)
        {
            using (var connection = _context.CreateConnection())
            {
                return connection.QuerySingleOrDefault<DateTime?>("SELECT LastExecutionTime FROM Workers WHERE Id = @Id", new { Id = workerId });
            }
        }
    }
}
