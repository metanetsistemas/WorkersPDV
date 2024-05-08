using Limpeza.Meta.Extensoes;

var builder = Host.CreateDefaultBuilder(args)
    .UseWindowsService()
    .ConfigureServices((hostContext, services) =>
    {
        services.ConfigurarInjecaoDependencia();
        services.ConfigurarQuartz();
    })
    .ConfigureAppConfiguration((hostingContext, config) =>
    {
        config.AddJsonFile("appsettings.json");
    });

var host = builder.Build();
host.Services.CriarBancoDados();

host.Run();
