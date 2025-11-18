using BACKEND.Dto;
using BACKEND.Entities;
using BACKEND.Interfaces;

namespace BACKEND.Service
{
    // Servicio encargado de la lógica de negocio relacionada con productos
    public class ProductoService
    {
        private readonly IProductoRepository _repo;

        // Constructor que recibe el repositorio de productos (inyección de dependencias)
        public ProductoService(IProductoRepository repo)
        {
            _repo = repo;
        }

        // Método para listar todos los productos
        public async Task<ApiResponse<IEnumerable<ProductoDto>>> ListarProductos()
        {
            // Obtener todos los productos desde el repositorio
            var productos = await _repo.GetAllAsync();

            // Mapear los productos a DTOs (solo los campos necesarios)
            var dto = productos.Select(p => new ProductoDto
            {
                Id = p.Id,
                Nombre = p.Nombre,
                Precio = p.Precio,
                Stock = p.Stock
            });

            // Retornar la respuesta estandarizada con éxito
            return new ApiResponse<IEnumerable<ProductoDto>>
            {
                Success = true,
                Message = "Datos obtenido con exito",
                Data = dto
            };
        }

    }
}
