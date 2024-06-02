using Microsoft.AspNetCore.Mvc;
using Limpeza.Meta.Repositorios.Interfaces;
using System.Runtime.InteropServices;
using TarefasPDV.Meta.Models;

[Route("api/[controller]")]
[ApiController]
public class WorkersController : ControllerBase
{
    private readonly IWorkerRepository _workerRepository;
    private readonly ILogger<WorkersController> _logger;

    public WorkersController(IWorkerRepository workerRepository, ILogger<WorkersController> logger)
    {
        _workerRepository = workerRepository;
        _logger = logger;   
    }

    [HttpGet("services")]
    public IActionResult GetServices()
    {
        try
        {
            var services = _workerRepository.GetAllServices();
            return Ok(services);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);

            return StatusCode(500, ex.Message);
        }
    }

    [HttpPost("updateExecutionTimeServices")]
    public IActionResult UpdateExecutionTimeServices([FromBody] ServiceDetails serviceDetails)
    {
        try
        {
            var workerId = _workerRepository.GetOrCreateWorkerId(serviceDetails.WorkerName, serviceDetails.Description);
            _workerRepository.UpdateWorkerExecution(workerId, serviceDetails.ExecutionTime);

            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);

            return StatusCode(500, ex.Message);
        }
    }
}
