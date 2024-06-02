using TarefasPDV.Meta.Models;

namespace Limpeza.Meta.Repositorios.Interfaces
{
    public interface IWorkerRepository
    {
        void InsertOrUpdateWorkerExecution(int workerId, string workerName, string descricaoServico, DateTime lastExecutionTime, DateTime nextExecutionTime, string ExecutionTime);
        int GetOrCreateWorkerId(string workerName, string Description);
        DateTime? GetLastExecutionTime(int workerId);
        IEnumerable<ServiceDetails> GetAllServices();
        string GetExecutionTime(string workerName);
        int UpdateWorkerExecution(int workerId, string xecutionTime);

    }
}
