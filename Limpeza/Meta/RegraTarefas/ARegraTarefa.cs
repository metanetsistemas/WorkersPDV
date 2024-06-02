using Limpeza.Workers;
using Quartz;
using TarefasPDV.Meta.Repositorios.Interfaces;

namespace Limpeza.Meta.RegraTarefas
{
    public abstract class ARegraTarefa : IRegraTarefa
    {
        protected readonly ILogger<LimpezaTabelaIpiranga> _logger;
        protected readonly IDapperContextSqlServer _dapperContext;

        public ARegraTarefa(IDapperContextSqlServer dapperContex)
        {
            _dapperContext = dapperContex;
        }

        public abstract Task Executar(IJobExecutionContext context);
    }
}
