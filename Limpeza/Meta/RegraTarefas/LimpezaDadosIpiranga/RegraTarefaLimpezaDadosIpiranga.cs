using Dapper;
using Limpeza.Meta.Repositorios.BancoDados;
using Limpeza.Meta.Repositorios.Interfaces;
using Limpeza.Workers;
using Quartz;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TarefasPDV.Meta.Repositorios.Interfaces;

namespace Limpeza.Meta.RegraTarefas.LimpezaDadosIpiranga
{
    public class RegraTarefaLimpezaDadosIpiranga : ARegraTarefa, IRegraTarefaLimpezaDadosIpiranga
    {
        private readonly ILogger<RegraTarefaLimpezaDadosIpiranga> _logger;
        public RegraTarefaLimpezaDadosIpiranga(IDapperContextSqlServer dapperContex, ILogger<RegraTarefaLimpezaDadosIpiranga> logger)
            : base(dapperContex)
        {
            _logger = logger;
        }

        public override async Task Executar(IJobExecutionContext context)
        {
            _logger.LogInformation("Executando tarefa de limpeza de dados Ipiranga");
            using (var connection = _dapperContext.CreateConnection())
            {
                // Define a data limite para deletar registros de mais de um mês atrás
                var dataLimite = DateTime.UtcNow.AddMonths(-3);

                var parametros = new DynamicParameters();
                parametros.Add("@DataLimite", dataLimite, DbType.Date);

                connection.Execute("TarefasPDV.PC_Apagar_Logs_Tabelas_Ipiranga", parametros,
                                   commandType: CommandType.StoredProcedure);
            }

            _logger.LogInformation("Tarefa de limpeza de dados Ipiranga executada com sucesso");
            await Task.CompletedTask;
        }

    }
}
