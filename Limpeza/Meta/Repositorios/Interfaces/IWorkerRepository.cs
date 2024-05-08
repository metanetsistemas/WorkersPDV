﻿namespace Limpeza.Meta.Repositorios.Interfaces
{
    public interface IWorkerRepository
    {
        void InsertOrUpdateWorkerExecution(int workerId, DateTime lastExecutionTime);
        DateTime? GetLastExecutionTime(int workerId);
    }
}
