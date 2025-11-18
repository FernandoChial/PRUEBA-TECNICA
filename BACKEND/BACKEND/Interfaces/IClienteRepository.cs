using BACKEND.Entities;

namespace BACKEND.Interfaces
{
    // Repositorio de clientes
    public interface IClienteRepository
    {
        Task<IEnumerable<Cliente>> GetAllAsync(); // Obtiene todos los clientes
    }
}
