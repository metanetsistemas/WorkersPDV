using Dapper;
using Limpeza.Meta.Repositorios.BancoDados;
using Limpeza.Meta.Repositorios.Interfaces;
using System;

namespace Limpeza.Meta.Repositorios
{
    public class WorkerRepository : IWorkerRepository
    {
        private readonly IDapperContext _context;

        public WorkerRepository(IDapperContext context)
        {
            _context = context;
        }

        public void InsertOrUpdateWorkerExecution(int workerId, string workerName, DateTime lastExecutionTime)
        {
            using (var connection = _context.CreateConnection())
            {
                // Utiliza o comando INSERT OR REPLACE para realizar um "upsert"
                connection.Execute(@"
                    INSERT INTO Workers (Id, WorkerName, LastExecutionTime)
                    VALUES (@Id, @WorkerName, @LastExecutionTime)
                    ON CONFLICT(Id) DO UPDATE SET
                    LastExecutionTime = excluded.LastExecutionTime;
                ", new { Id = workerId, WorkerName = workerName,  LastExecutionTime = lastExecutionTime });
            }
        }

        public int GetOrCreateWorkerId(string workerName)
        {
            using (var connection = _context.CreateConnection())
            {
                // Verificar se o worker já existe
                var workerId = connection.QuerySingleOrDefault<int?>(
                    "SELECT Id FROM Workers WHERE WorkerName = @WorkerName",
                    new { WorkerName = workerName });

                if (workerId.HasValue)
                {
                    return workerId.Value;
                }
                else
                {
                    // Inserir novo worker e obter o ID
                    connection.Execute(
                        "INSERT INTO Workers (WorkerName) VALUES (@WorkerName)",
                        new { WorkerName = workerName });

                    return connection.QuerySingle<int>("SELECT last_insert_rowid()");
                }
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
