using BACKEND.Dto;
using BACKEND.Entities;

namespace BACKEND.Interfaces
{
    // Repositorio de ventas
    public interface IVentaRepository
    {
        Task<string> RegistrarVentaAsync(RegistrarVentaDto ventaDto); // Registra una venta
        Task<IEnumerable<HistorialVenta>> HistorialPorCliente(int clienteId); // Obtiene historial por cliente
    }
}
