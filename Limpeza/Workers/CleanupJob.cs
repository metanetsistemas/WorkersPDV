using Limpeza.Meta.Repositorios;
using Quartz;

namespace Limpeza.Workers
{
    public class CleanupJob : IJob
    {
        private readonly ILogger<CleanupJob> _logger;
        private readonly IWorkerRepository _repository;

        public CleanupJob(ILogger<CleanupJob> logger, IWorkerRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation("Cleaning up the table at: {time}", DateTimeOffset.Now);

            // Convert DateTimeOffset to DateTime
            _repository.InsertWorkerExecution(1, DateTimeOffset.Now.DateTime);

            await Task.CompletedTask;
        }
    }

}
