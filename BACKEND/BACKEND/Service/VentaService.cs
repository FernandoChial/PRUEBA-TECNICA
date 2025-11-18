using BACKEND.Dto;
using BACKEND.Interfaces;

namespace BACKEND.Service
{
    // Servicio encargado de la lógica de negocio relacionada con ventas
    public class VentaService
    {
        private readonly IVentaRepository _repo;

        // Constructor que recibe el repositorio de ventas (inyección de dependencias)
        public VentaService(IVentaRepository repo)
        {
            _repo = repo;
        }

        // Método para obtener el historial de ventas de un cliente específico
        public async Task<ApiResponse<IEnumerable<HistorialVentaDto>>> Historial(int clienteId)
        {
            // Consultar el historial de ventas desde el repositorio
            var data = await _repo.HistorialPorCliente(clienteId);

            // Mapear los resultados a DTOs (solo los campos necesarios)
            var dto = data.Select(h => new HistorialVentaDto
            {
                VentaId = h.VentaId,
                Fecha = h.Fecha,
                Cliente = h.Cliente,
                Email = h.Email,
                Producto = h.Producto,
                Cantidad = h.Cantidad,
                PrecioUnitario = h.PrecioUnitario,
                TotalLinea = h.TotalLinea
            });

            // Retornar la respuesta estandarizada con éxito
            return new ApiResponse<IEnumerable<HistorialVentaDto>>
            {
                Success = true,
                Data = dto
            };
        }

        // Método para registrar una venta nueva
        public async Task<ApiResponse<string>> RegistrarVentaAsync(RegistrarVentaDto dto)
        {
            // Llamar al repositorio para registrar la venta y obtener un mensaje de resultado
            var mensaje = await _repo.RegistrarVentaAsync(dto);

            // Retornar la respuesta estandarizada con éxito
            return new ApiResponse<string>
            {
                Success = true,
                Message = mensaje
            };
        }

    }
}
