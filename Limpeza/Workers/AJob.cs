using Limpeza.Meta.RegraTarefas;
using Limpeza.Meta.RegraTarefas.LimpezaDadosIpiranga;
using Limpeza.Meta.Repositorios.Interfaces;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Limpeza.Workers
{
    public abstract class AJob : IJob
    {
        protected IRegraTarefa RegraTarefa { get; private set; }

        private IWorkerRepository _workerRepository;

        public AJob(IWorkerRepository workerRepository, IRegraTarefa regraTarefa)
        {
            this._workerRepository = workerRepository;
            this.RegraTarefa = regraTarefa;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            await this.RegraTarefa.Executar(context);

            this.DefinirUltimaExecucao();
        }

        protected void DefinirUltimaExecucao()
        {
            _workerRepository.InsertWorkerExecution(this.ChaveTarefa, DateTimeOffset.Now.DateTime);
        }

        protected abstract int ChaveTarefa { get; }
    }
}
