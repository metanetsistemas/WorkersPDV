using Limpeza.Meta.RegraTarefas;
using Limpeza.Meta.Repositorios.Interfaces;
using Quartz;

namespace Limpeza.Workers
{
    public abstract class AJob : IJob
    {
        protected IRegraTarefa RegraTarefa { get; private set; }

        private readonly IWorkerRepository _workerRepository;
        private readonly string _workerName;

        public AJob(IWorkerRepository workerRepository, IRegraTarefa regraTarefa, string workerName)
        {
            _workerRepository = workerRepository;
            RegraTarefa = regraTarefa;
            _workerName = workerName;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            await RegraTarefa.Executar(context);

            DefinirUltimaExecucao();
        }

        protected void DefinirUltimaExecucao()
        {
            var workerId = _workerRepository.GetOrCreateWorkerId(_workerName);
            _workerRepository.InsertOrUpdateWorkerExecution(workerId, _workerName, DateTimeOffset.Now.DateTime);
        }
    }
}
