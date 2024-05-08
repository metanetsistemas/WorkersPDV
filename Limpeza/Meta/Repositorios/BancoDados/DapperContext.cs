using Microsoft.Data.Sqlite;
using SQLitePCL;
using System.Data;

namespace Limpeza.Meta.Repositorios.BancoDados
{
    public class DapperContext : IDapperContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public DapperContext(IConfiguration configuration)
        {
            Batteries.Init(); // Inicializando aqui
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection") ?? string.Empty;
        }

        public IDbConnection CreateConnection()
        {
            return new SqliteConnection(_connectionString);
        }

        public void EnsureDatabaseCreated()
        {
            using var connection = CreateConnection();
            var command = connection.CreateCommand();
            command.CommandText = @"
        CREATE TABLE IF NOT EXISTS Workers (
            Id INTEGER PRIMARY KEY,
            WorkerName TEXT NOT NULL,
            LastExecutionTime DATETIME
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
