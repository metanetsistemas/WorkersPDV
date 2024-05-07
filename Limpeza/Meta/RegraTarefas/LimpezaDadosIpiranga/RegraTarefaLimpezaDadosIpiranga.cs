using Limpeza.Meta.Repositorios.Interfaces;
using Limpeza.Workers;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Limpeza.Meta.RegraTarefas.LimpezaDadosIpiranga
{
    public class RegraTarefaLimpezaDadosIpiranga : ARegraTarefa, IRegraTarefaLimpezaDadosIpiranga
    {
        public RegraTarefaLimpezaDadosIpiranga(ILogger<LimpezaTabelaIpiranga> logger)
            : base(logger)
        {

        }

        public override async Task Executar(IJobExecutionContext context)
        {
            _logger.LogInformation($"Vai executar a tarefa de limpeza: {DateTimeOffset.Now}");

            await Task.CompletedTask;
        }
    }
}
