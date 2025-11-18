using BACKEND.Dto;
using BACKEND.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BACKEND.Controllers
{
    // Define la ruta base del controlador y que es un API controller
    [Route("api/clientes")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        // Campo privado para guardar la instancia del servicio de clientes
        private readonly ClienteService _service;

        // Constructor que recibe el servicio mediante inyección de dependencias
        public ClientesController(ClienteService service)
        {            
            _service = service;
        }

        // Método GET para listar todos los clientes
        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            try
            {
                // Llamamos al servicio para obtener los clientes
                var response = await _service.ListarClientes();

                // Retornamos el resultado como OK
                return Ok(response);
            }
            catch (Exception ex)
            {
                // En caso de error, retornamos un status 500 con mensaje de error
                return StatusCode(500, new ApiResponse<string>
                {
                    Success = false,
                    Message = ex.Message
                });
            }
        }
    }
}
