using System;
using System.Xml.Serialization;

namespace TarefasPDV.Meta.Models
{
    [XmlRoot("configuracao-pdv", Namespace = "configuracao-pdv")]
    public class ConfiguracaoPDV
    {
        [XmlElement("configuracoes")]
        public Configuracoes Configuracoes { get; set; }
    }

    public class Configuracoes
    {
        [XmlElement("configuracaoBancoDados")]
        public ConfiguracaoBancoDados ConfiguracaoBancoDados { get; set; }
    }

    public class ConfiguracaoBancoDados
    {
        [XmlElement("instancia")]
        public string Instancia { get; set; }

        [XmlElement("nomeBancoDados")]
        public string NomeBancoDados { get; set; }

        [XmlElement("senha")]
        public string Senha { get; set; }

        [XmlElement("utilizarSenhaGlobal")]
        public bool UtilizarSenhaGlobal { get; set; }

        [XmlElement("segurancaIntegrada")]
        public bool SegurancaIntegrada { get; set; }

        [XmlElement("usuario")]
        public string Usuario { get; set; }

        [XmlElement("tipoBancoDados")]
        public int TipoBancoDados { get; set; }
    }
}
