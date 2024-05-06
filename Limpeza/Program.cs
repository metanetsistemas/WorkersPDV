using Limpeza.Meta.Extensoes;
using Limpeza.Meta.Repositorios.BancoDados;

var builder = Host.CreateApplicationBuilder(args);
builder.Configuration.AddJsonFile("appsettings.json");

builder.Services.ConfigurarInjecaoDependencia();

var host = builder.Build();

var dapperContext = host.Services.GetRequiredService<IDapperContext>() as DapperContext;
dapperContext?.EnsureDatabaseCreated();

host.Run();
