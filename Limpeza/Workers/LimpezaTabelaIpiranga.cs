using Limpeza.Meta.RegraTarefas.LimpezaDadosIpiranga;
using Limpeza.Meta.Repositorios.Interfaces;

namespace Limpeza.Workers
{
    public class LimpezaTabelaIpiranga : AJob
    {
        public LimpezaTabelaIpiranga(IWorkerRepository workerRepository, IRegraTarefaLimpezaDadosIpiranga regraTarefa)
            : base(workerRepository, regraTarefa)
        {
        }
    }
}
