namespace Limpeza.Meta.Repositorios.Interfaces
{
    public interface IWorkerRepository
    {
        void InsertWorkerExecution(int workerId, DateTime lastExecutionTime);
        DateTime? GetLastExecutionTime(int workerId);
    }
}
