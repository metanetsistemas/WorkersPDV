using Dapper;
using Limpeza.Meta.Repositorios.BancoDados;
using Limpeza.Meta.Repositorios.Interfaces;
using System;
using TarefasPDV.Meta.Models;

namespace Limpeza.Meta.Repositorios
{
    public class WorkerRepository : IWorkerRepository
    {
        private readonly IDapperContext _context;
        private readonly ILogger<WorkerRepository> _logger;
        private string ExecutionTimePadrao = "0 59 23 ? * FRI";

        public WorkerRepository(IDapperContext context, ILogger<WorkerRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public void InsertOrUpdateWorkerExecution(int workerId, string workerName, string description, DateTime lastExecutionTime, DateTime nextExecutionTime, string executionTime)
        {
            _logger.LogInformation("Inserindo ou atualizando execução do worker {WorkerName}", workerName);
            using (var connection = _context.CreateConnection())
            {
                connection.Execute(@"
            INSERT INTO Workers (Id, WorkerName, Description, LastExecutionTime, NextExecutionTime, ExecutionTime)
            VALUES (@Id, @WorkerName, @Description, @LastExecutionTime, @NextExecutionTime, @ExecutionTime)
            ON CONFLICT(Id) DO UPDATE SET
            LastExecutionTime = excluded.LastExecutionTime,
            NextExecutionTime = excluded.NextExecutionTime,
            ExecutionTime = excluded.ExecutionTime;
        ", new { Id = workerId, WorkerName = workerName, Description = description, LastExecutionTime = lastExecutionTime, NextExecutionTime = nextExecutionTime, ExecutionTime = executionTime });
            }
        }


        public int GetOrCreateWorkerId(string workerName, string description)
        {
            _logger.LogInformation("Obtendo ou criando worker {WorkerName}", workerName);
            using (var connection = _context.CreateConnection())
            {
                // Verificar se o worker já existe
                var workerId = connection.QuerySingleOrDefault<int?>(
                    "SELECT Id FROM Workers WHERE WorkerName = @WorkerName",
                    new { WorkerName = workerName });

                if (workerId.HasValue)
                {        
                    _logger.LogInformation("Worker {WorkerName} já existe com ID {WorkerId}", workerName, workerId.Value);
                    return workerId.Value;
                }
                else
                {
                    // Inserir novo worker e obter o ID
                    connection.Execute(
                        "INSERT INTO Workers (WorkerName, Description, NextExecutionTime) VALUES (@WorkerName, @Description, '2000-01-01')",
                        new { WorkerName = workerName, Description = description });

                    _logger.LogInformation("Worker {WorkerName} criado com sucesso", workerName);
                    return connection.QuerySingle<int>("SELECT last_insert_rowid()");
                }
            }
        }

        public DateTime? GetLastExecutionTime(int workerId)
        {
            _logger.LogInformation("Obtendo última execução do worker {WorkerId}", workerId);
            using (var connection = _context.CreateConnection())
            {
                return connection.QuerySingleOrDefault<DateTime?>("SELECT LastExecutionTime FROM Workers WHERE Id = @Id", new { Id = workerId });
            }
        }

        public IEnumerable<ServiceDetails> GetAllServices()
        {
            _logger.LogInformation("Obtendo todos os workers");
            using (var connection = _context.CreateConnection())
            {
                return connection.Query<ServiceDetails>("SELECT * FROM Workers");
            }
        }

        public string GetExecutionTime(string workerName)
        {
            try
            {
                _logger.LogInformation("Obtendo horário de execução do worker {WorkerName}", workerName);
                using (var connection = _context.CreateConnection())
                {
                    var retorno = connection.QuerySingleOrDefault<string>("SELECT ExecutionTime FROM Workers WHERE WorkerName = @WorkerName", new { WorkerName = workerName });
                    if (retorno == null)
                    {
                        return ExecutionTimePadrao;
                    }
                    return retorno;
                }
            }
            catch (Exception)
            {
                _logger.LogError("Erro ao obter horário de execução do worker {WorkerName}", workerName);
                return ExecutionTimePadrao;
            }
            
        }

        public int UpdateWorkerExecution(int workerId, string excutionTime)
        {
           _logger.LogInformation("Atualizando horário de execução do worker {WorkerId}", workerId);
            using (var connection = _context.CreateConnection())
            {
                return connection.Execute("UPDATE Workers SET ExecutionTime = @ExecutionTime WHERE Id = @Id", new { Id = workerId, ExecutionTime = excutionTime });
            }
        }
    }
}
