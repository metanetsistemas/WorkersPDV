using Microsoft.Data.Sqlite;
using SQLitePCL;
using System.Data;
using System;

namespace Limpeza.Meta.Repositorios.BancoDados
{
    public class DapperContext : IDapperContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        private readonly ILogger<DapperContext> _logger;

        public DapperContext(IConfiguration configuration, ILogger<DapperContext> logger)
        {
            Batteries.Init();
            _configuration = configuration;
            _logger = logger;
            var databasePath = $"{AppDomain.CurrentDomain.BaseDirectory}workers.db";
            _connectionString = _configuration.GetConnectionString("DefaultConnection").Replace("|DataDirectory|", databasePath);
        }

        public IDbConnection CreateConnection()
        {
            _logger.LogError("Criando conexão com o sql lite");
            return new SqliteConnection(_connectionString);
        }

        public void EnsureDatabaseCreated()
        {
            _logger.LogError("Criando database no sql lite");

            using var connection = CreateConnection();
            var command = connection.CreateCommand();
            command.CommandText = @"
                                    CREATE TABLE IF NOT EXISTS Workers (
                                        Id INTEGER PRIMARY KEY,
                                        WorkerName TEXT NOT NULL,
                                        LastExecutionTime DATETIME,
                                        Description TEXT NOT NULL,
                                        NextExecutionTime DATETIME,
                                        ExecutionTime TEXT 
                                    );";
            connection.Open();
            command.ExecuteNonQuery();
        }
    }

    public interface IDapperContext
    {
        IDbConnection CreateConnection();
    }
}
