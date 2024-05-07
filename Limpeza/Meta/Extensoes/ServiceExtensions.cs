using Limpeza.Meta.Repositorios.BancoDados;
using Limpeza.Meta.Repositorios;
using Quartz;
using Limpeza.Workers;
using Limpeza.Meta.Repositorios.Interfaces;
using Limpeza.Meta.RegraTarefas.LimpezaDadosIpiranga;

namespace Limpeza.Meta.Extensoes
{
    public static class ServiceExtensions
    {
        public static void ConfigurarInjecaoDependencia(this IServiceCollection services)
        {
            services.AddSingleton<IDapperContext, DapperContext>();
            services.AddSingleton<IWorkerRepository, WorkerRepository>();

            services.AddSingleton<IRegraTarefaLimpezaDadosIpiranga, RegraTarefaLimpezaDadosIpiranga>();
            
            services.AddHostedService<Worker>();
        }

        public static void ConfigurarQuartz(this IServiceCollection services)
        {
            services.AddQuartz(q =>
            {
                q.AddJob<LimpezaTabelaIpiranga>(opts => opts.WithIdentity(new JobKey("LimpezaTabelaIpiranga")));
                q.AddTrigger(opts => opts
                    .ForJob("LimpezaTabelaIpiranga")
                    .WithIdentity("LimpezaTabelaIpiranga-trigger")
                    .WithCronSchedule("0 25 15 ? * TUE"));
            });

            services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);
        }

        public static void CriarBancoDados(this IServiceProvider serviceProvider)
        {
            var dapperContext = serviceProvider.GetRequiredService<IDapperContext>() as DapperContext;
            dapperContext?.EnsureDatabaseCreated();
        }
    }
}
