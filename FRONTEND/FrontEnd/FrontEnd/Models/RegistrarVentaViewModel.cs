using Microsoft.AspNetCore.Mvc.Rendering;

namespace FrontEnd.Models
{
    public class RegistrarVentaViewModel
    {
        public string EmailCliente { get; set; } = string.Empty;
        public List<RegistrarProductoViewModel> Productos { get; set; } = new();

        //SOLO PARA LA VISTA (dropdown)
        public List<SelectListItem> Clientes { get; set; } = new();
        public List<SelectListItem> ProductosDisponibles { get; set; } = new(); 

    }
}
