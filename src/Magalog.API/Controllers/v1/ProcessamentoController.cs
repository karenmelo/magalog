using Magalog.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Magalog.API.Controllers.v1
{
    [ApiController]
    [Route("v1/[controller]")]
    public class ProcessamentoController : ControllerBase
    {

        private readonly ILogger<ProcessamentoController> _logger;
        private readonly ILegacyProcessingService _legacyProcessingService;

        public ProcessamentoController(ILogger<ProcessamentoController> logger, ILegacyProcessingService legacyProcessingService)
        {
            _logger = logger;
            _legacyProcessingService = legacyProcessingService;
        }

        [HttpPost]
        [Route("processar-arquivo")]
        public async Task<IActionResult> Post(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("Arquivo inválido.");
            }

            using var reader = new StreamReader(file.OpenReadStream());
            var lines = await reader.ReadToEndAsync();
            await _legacyProcessingService.ProcessLegacyFile(lines);

            return Ok();
        }


        [HttpGet("consultar-dados")]
        public async Task<IActionResult> Get([FromQuery] int? order_id, [FromQuery] DateOnly? startDate, [FromQuery] DateOnly? endDate)
        {           

            if (startDate != null && endDate != null && startDate > endDate)
            {
                return BadRequest("A data inicial não pode ser maior que a data final.");
            }

            var result = await _legacyProcessingService.GetOrders(order_id, startDate, endDate);


            if (!result.Any()) return NotFound("Nenhum registro encontrado.");


            return Ok(result);

        }
    }
}
