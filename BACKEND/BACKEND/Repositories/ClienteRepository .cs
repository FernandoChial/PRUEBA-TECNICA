using BACKEND.Data;
using BACKEND.Entities;
using BACKEND.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BACKEND.Repositories
{
    // Repositorio encargado de operaciones de acceso a datos para Clientes
    public class ClienteRepository : IClienteRepository
    {
        private readonly AppDbContext _context;

        // Constructor que recibe el DbContext mediante inyección de dependencias
        public ClienteRepository(AppDbContext context)
        {
            _context = context;
        }

        // Método para obtener todos los clientes de la base de datos
        public async Task<IEnumerable<Cliente>> GetAllAsync()
        {
            // Consulta asíncrona usando Entity Framework Core para devolver la lista de clientes
            return await _context.Clientes.ToListAsync();
        }
    }
}
