using Limpeza.Meta.Repositorios.BancoDados;
using Limpeza.Meta.Repositorios;
using Quartz;
using Limpeza.Workers;

namespace Limpeza.Meta.Extensoes
{
    public static class ServiceExtensions
    {
        public static void ConfigurarInjecaoDependencia(this IServiceCollection services)
        {
            services.AddSingleton<IDapperContext, DapperContext>();
            services.AddSingleton<IWorkerRepository, WorkerRepository>();
            services.AddQuartz(q =>
            {
                q.AddJob<CleanupJob>(opts => opts.WithIdentity(new JobKey("CleanupJob")));
                q.AddTrigger(opts => opts
                    .ForJob("CleanupJob")
                    .WithIdentity("CleanupJob-trigger")
                    .WithCronSchedule("0 0 21 ? * FRI"));
            });
            services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);
            services.AddHostedService<Worker>();
        }
    }
}
