using Limpeza.Meta.Repositorios.BancoDados;
using Limpeza.Meta.Repositorios;
using Quartz;
using Limpeza.Workers;
using Limpeza.Meta.Repositorios.Interfaces;
using Limpeza.Meta.RegraTarefas.LimpezaDadosIpiranga;
using TarefasPDV.Meta.Repositorios.BancoDados;
using TarefasPDV.Meta.Repositorios.Interfaces;

namespace Limpeza.Meta.Extensoes
{
    public static class ServiceExtensions
    {
        public static IHostBuilder ConfigurarLog(this IHostBuilder builder)
        {
            builder.ConfigureLogging(logging =>
            {
                logging.AddEventLog(eventLogSettings =>
                {
                    eventLogSettings.SourceName = "TarefasPDV";
                });
            });

            return builder;
        }

        public static void ConfigurarInjecaoDependencia(this IServiceCollection services)
        {
            services.AddSingleton<IDapperContext, DapperContext>();
            services.AddSingleton<IDapperContextSqlServer, DapperContextSqlServer>();
            services.AddSingleton<IWorkerRepository, WorkerRepository>();

            services.AddSingleton<IRegraTarefaLimpezaDadosIpiranga, RegraTarefaLimpezaDadosIpiranga>();

            services.AddHostedService<Worker>();
        }

        public static void ConfigurarQuartz(this IServiceCollection services)
        {
            services.AddQuartz(q =>
            {
                var serviceProvider = services.BuildServiceProvider();
                var workerRepository = serviceProvider.GetService<IWorkerRepository>();

                var cronSchedule = workerRepository.GetExecutionTime("LimpezaTabelaIpiranga");

                q.AddJob<LimpezaTabelaIpiranga>(opts => opts.WithIdentity(new JobKey("LimpezaTabelaIpiranga")));
                q.AddTrigger(opts => opts
                    .ForJob("LimpezaTabelaIpiranga")
                    .UsingJobData("descricao", "Executa tarefas de limpeza das tabelas no Sistema MetaNet")
                    .WithIdentity("LimpezaTabelaIpiranga-trigger")
                    .WithCronSchedule(cronSchedule));
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
