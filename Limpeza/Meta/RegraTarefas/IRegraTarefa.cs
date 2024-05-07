using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Limpeza.Meta.RegraTarefas
{
    public interface IRegraTarefa
    {
        Task Executar(IJobExecutionContext context);
    }
}
