using Limpeza.Meta.Extensoes;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

var builder = Host.CreateDefaultBuilder(args)
    .UseWindowsService()
    .ConfigurarLog()
    .ConfigureWebHostDefaults(webBuilder =>
    {
        webBuilder.ConfigureServices(services =>
        {
            services.AddControllers();  // Adiciona suporte a controllers
            services.ConfigurarInjecaoDependencia();       
            services.ConfigurarQuartz();
        });
        webBuilder.Configure(app =>
        {
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();  // Mapeia os controllers
            });
        });
    })
    .ConfigureAppConfiguration((hostingContext, config) =>
    {
        config.AddJsonFile("appsettings.json");
    });

var host = builder.Build();

host.Services.CriarBancoDados();
host.Run();
