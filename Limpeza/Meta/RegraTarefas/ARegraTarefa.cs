using Limpeza.Meta.Repositorios.Interfaces;
using Limpeza.Workers;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
