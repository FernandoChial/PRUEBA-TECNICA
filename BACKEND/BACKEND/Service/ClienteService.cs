using BACKEND.Dto;
using BACKEND.Entities;
using BACKEND.Interfaces;

namespace BACKEND.Service
{
    public class ClienteService
    {
        private readonly IClienteRepository _repo;

        public ClienteService(IClienteRepository repo)
        {
            _repo = repo;
        }

        public async Task<ApiResponse<IEnumerable<ClienteDto>>> ListarClientes()
        {
  
                var clientes = await _repo.GetAllAsync();

                var dto = clientes.Select(c => new ClienteDto
                {
                    Id = c.Id,
                    Nombre = c.Nombre,
                    Email = c.Email
                });

                return new ApiResponse<IEnumerable<ClienteDto>>
                {
                    Success = true,
                    Message = "Datos obtenido con exito",
                    Data = dto
                };
           
        }
    }
}
