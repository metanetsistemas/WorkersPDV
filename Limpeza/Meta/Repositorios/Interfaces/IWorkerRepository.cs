namespace Limpeza.Meta.Repositorios.Interfaces
{
    public interface IWorkerRepository
    {
        void InsertOrUpdateWorkerExecution(int workerId, DateTime lastExecutionTime);
        int GetOrCreateWorkerId(string workerName);
        DateTime? GetLastExecutionTime(int workerId);
    }
}
