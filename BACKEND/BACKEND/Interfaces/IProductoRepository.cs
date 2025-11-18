using BACKEND.Entities;

namespace BACKEND.Interfaces
{
    // Repositorio de productos
    public interface IProductoRepository
    {
        Task<IEnumerable<Producto>> GetAllAsync(); // Obtiene todos los productos
    }
}
