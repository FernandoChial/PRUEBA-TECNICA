using BACKEND.Data;
using BACKEND.Entities;
using BACKEND.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BACKEND.Repositories
{
    // Repositorio encargado de operaciones de acceso a datos para Productos
    public class ProductoRepository : IProductoRepository
    {
        private readonly AppDbContext _context;

        // Constructor que recibe el DbContext (inyección de dependencias)
        public ProductoRepository(AppDbContext context)
        {
            _context = context;
        }

        // Método para obtener todos los productos de la base de datos
        public async Task<IEnumerable<Producto>> GetAllAsync()
        {
            // Consulta asíncrona usando Entity Framework Core para devolver la lista de productos
            return await _context.Productos.ToListAsync();
        }
    }
}
