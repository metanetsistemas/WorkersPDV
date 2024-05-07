using Limpeza.Meta.RegraTarefas;
using Limpeza.Meta.RegraTarefas.LimpezaDadosIpiranga;
using Limpeza.Meta.Repositorios;
using Limpeza.Meta.Repositorios.Interfaces;
using Quartz;

namespace Limpeza.Workers
{
    public class LimpezaTabelaIpiranga : AJob
    {
        public LimpezaTabelaIpiranga(IWorkerRepository workerRepository, IRegraTarefaLimpezaDadosIpiranga regraTarefa)
            :base(workerRepository, regraTarefa)
        {
        }

        protected override int ChaveTarefa
        {
            get
            {
                return 1;
            }
        }
    }

}
