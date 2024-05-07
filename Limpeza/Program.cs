using Limpeza.Meta.Extensoes;

var builder = Host.CreateApplicationBuilder(args);
builder.Configuration.AddJsonFile("appsettings.json");

builder.Services.ConfigurarInjecaoDependencia();
builder.Services.ConfigurarQuartz();

var host = builder.Build();

host.Services.CriarBancoDados();

host.Run();
