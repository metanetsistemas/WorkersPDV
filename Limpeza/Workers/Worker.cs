using Limpeza.Meta.Repositorios;
using Limpeza.Meta.Repositorios.Interfaces;
using Quartz;

namespace Limpeza.Workers
{
    public class Worker : BackgroundService
    {
        private readonly ISchedulerFactory _schedulerFactory;
        private readonly ILogger<Worker> _logger;
        private readonly IWorkerRepository _repository;
        private IScheduler? _scheduler;

        public Worker(ISchedulerFactory schedulerFactory, ILogger<Worker> logger, IWorkerRepository repository)
        {
            _schedulerFactory = schedulerFactory;
            _logger = logger;
            _repository = repository;
            _scheduler = null; 
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //try
            //{
            //    _scheduler = await _schedulerFactory.GetScheduler(stoppingToken);
            //    _logger.LogInformation("Scheduler started.");

            //    var lastExecutionTime = _repository.GetLastExecutionTime(1); // Assuming 1 for CleanupJob
            //    if (NeedsCatchUp(lastExecutionTime))
            //    {
            //        _logger.LogInformation("Realizando catch-up da execução perdida.");
            //        await _scheduler.TriggerJob(new JobKey("CleanupJob"));
            //    }

            //    await _scheduler.Start(stoppingToken);
            //    await Task.Delay(-1, stoppingToken); // Manter o serviço ativo
            //}
            //catch (Exception)
            //{ }

            await Task.CompletedTask;
        }

        private bool NeedsCatchUp(DateTimeOffset? lastExecutionTime)
        {
            return DateTimeOffset.Now.DayOfWeek == DayOfWeek.Friday &&
                   (lastExecutionTime == null || lastExecutionTime?.Day != DateTimeOffset.Now.Day);
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            if (_scheduler != null)
            {
                await _scheduler.Shutdown(cancellationToken);
            }

            await base.StopAsync(cancellationToken);
        }
    }
}
