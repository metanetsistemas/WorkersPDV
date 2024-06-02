

namespace TarefasPDV.Meta.Models
{
    public class ServiceDetails
    {
        public int Id { get; set; }
        public string? WorkerName { get; set; }
        public DateTime LastExecutionTime { get; set; }
        public string Description { get; set; }
        public DateTime NextExecutionTime { get; set; }
        public string ExecutionTime { get; set; }
    }

}
