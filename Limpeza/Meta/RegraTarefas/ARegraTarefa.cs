using Limpeza.Workers;
using Quartz;

namespace Limpeza.Meta.RegraTarefas
{
    public abstract class ARegraTarefa : IRegraTarefa
    {
        protected readonly ILogger<LimpezaTabelaIpiranga> _logger;
        
        public ARegraTarefa(ILogger<LimpezaTabelaIpiranga> logger)
        {
            _logger = logger;
        }

        public abstract Task Executar(IJobExecutionContext context);
    }
}
