using Limpeza.Meta.RegraTarefas;
using Limpeza.Meta.Repositorios.Interfaces;
using Quartz;

namespace Limpeza.Workers
{
    public abstract class AJob : IJob
    {
        protected IRegraTarefa RegraTarefa { get; private set; }
        private readonly IWorkerRepository _workerRepository;

        public AJob(IWorkerRepository workerRepository, IRegraTarefa regraTarefa)
        {
            RegraTarefa = regraTarefa;
            _workerRepository = workerRepository;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            await RegraTarefa.Executar(context);
            DefinirUltimaExecucao(context);
        }

        protected void DefinirUltimaExecucao(IJobExecutionContext context)
        {
            DateTime nextExecutionTime = CalculateNextExecutionTime(context);
            string Description = context.MergedJobDataMap.GetString("descricao");

            // Pegar a WithCronSchedule
            string cronExpression = context.Trigger is ICronTrigger cronTrigger ? cronTrigger.CronExpressionString : null;

            string workerName = context.JobDetail.Key.Name;
            var workerId = _workerRepository.GetOrCreateWorkerId(workerName, Description);
            _workerRepository.InsertOrUpdateWorkerExecution(workerId, workerName, Description, DateTimeOffset.Now.DateTime, nextExecutionTime, cronExpression);
        }

        protected DateTime CalculateNextExecutionTime(IJobExecutionContext context)
        {
            ICronTrigger trigger = context.Trigger as ICronTrigger;
            var nextFireTime = trigger.GetFireTimeAfter(DateTimeOffset.Now);
            return nextFireTime.HasValue ? nextFireTime.Value.LocalDateTime : DateTime.Now;
        }
    }
}
