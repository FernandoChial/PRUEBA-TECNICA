using BACKEND.Data;
using BACKEND.Dto;
using BACKEND.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BACKEND.Controllers
{
    [Route("api/productos")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        // Campo privado para guardar la instancia del servicio de productos
        private readonly ProductoService _service;

        public ProductosController(ProductoService service)
        {
            _service = service;
        }

        // Listar todos los productos
        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            try
            {
                var response = await _service.ListarProductos();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<string>
                {
                    Success = false,
                    Message = ex.Message
                });
            }
        }
    }
}
