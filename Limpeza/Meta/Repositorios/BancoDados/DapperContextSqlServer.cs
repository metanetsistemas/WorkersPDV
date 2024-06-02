using System.Data.Common;
using System.Data.SqlClient;
using System.Xml;
using System.Xml.Serialization;
using TarefasPDV.Meta.Models;
using TarefasPDV.Meta.Repositorios.Interfaces;

namespace TarefasPDV.Meta.Repositorios.BancoDados
{
    public sealed class DapperContextSqlServer : IDapperContextSqlServer
    {
        private readonly string? connectionString;
        private readonly ILogger<DapperContextSqlServer> _logger;

        public DapperContextSqlServer(IConfiguration configuration, ILogger<DapperContextSqlServer> logger)
        {
            _logger = logger;
            this.connectionString = GetConnectionString();
        }

        public DbConnection CreateConnection()
        {
            _logger.LogError("Criando conexão com o sql server");
            return new SqlConnection(this.connectionString);
        }

        public string GetConnectionString()
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

            // Caminho relativo para subir dois níveis e acessar a pasta Release diretamente
            string filePath = Path.Combine(baseDirectory, @"..\..\configuracao-pdv.xml");

            // Para garantir que o caminho seja resolvido corretamente
            filePath = Path.GetFullPath(filePath);

            try
            {
                var configuracao = LoadFromXmlFile(filePath);

                string connectionString = $"Server={configuracao.Instancia};Database={configuracao.NomeBancoDados};";
                if (configuracao.SegurancaIntegrada)
                    connectionString += "Integrated Security=True;";
                else
                    connectionString += $"User Id={configuracao.Usuario};Password={configuracao.Senha};";

                _logger.LogError("String de conexão obtida com sucesso" + connectionString);
                return connectionString;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return string.Empty;
            }
        }

        public static ConfiguracaoBancoDados LoadFromXmlFile(string filePath)
        {
            
            XmlSerializer serializer = new XmlSerializer(typeof(ConfiguracaoPDV), new XmlRootAttribute("configuracao-pdv") { Namespace = "configuracao-pdv" });

            ConfiguracaoPDV configuracaoPDV;
            using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
            {
                configuracaoPDV = (ConfiguracaoPDV)serializer.Deserialize(fileStream);
            }

            return configuracaoPDV.Configuracoes.ConfiguracaoBancoDados;
        }

    }
}
