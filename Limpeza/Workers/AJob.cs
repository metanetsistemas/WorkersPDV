using Limpeza.Meta.RegraTarefas;
using Limpeza.Meta.Repositorios.Interfaces;
using Quartz;

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
            _workerRepository.InsertOrUpdateWorkerExecution(this.ChaveTarefa, DateTimeOffset.Now.DateTime);
        }

        protected abstract int ChaveTarefa { get; }
    }
}
