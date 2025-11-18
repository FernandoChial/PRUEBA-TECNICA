using BACKEND.Dto;
using BACKEND.Service;
using Microsoft.AspNetCore.Mvc;

namespace BACKEND.Controllers
{
    [Route("api/ventas")]
    [ApiController]
    public class VentasController : ControllerBase
    {
        private readonly VentaService _service;

        public VentasController(VentaService service)
        {
            _service = service;
        }

        // Obtener historial de ventas por cliente
        [HttpGet("historial/{clienteId}")]
        public async Task<IActionResult> Historial(int clienteId)
        {
            try
            {
                var resp = await _service.Historial(clienteId);
                return Ok(resp);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<string> { Success = false, Message = ex.Message });
            }
        }

        // Registrar una venta
        [HttpPost("registrar")]
        public async Task<IActionResult> Registrar([FromBody] RegistrarVentaDto dto)
        {
            try
            {
                var resp = await _service.RegistrarVentaAsync(dto);
                return Ok(resp);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<string> { Success = false, Message = ex.Message });
            }
        }
    }
}
