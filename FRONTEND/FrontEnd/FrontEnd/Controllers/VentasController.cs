using FrontEnd.Models;
using FrontEnd.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FrontEnd.Controllers
{
    public class VentasController : Controller
    {
        private readonly ApiService _api; // Servicio para llamar a la API

        public VentasController(ApiService api)
        {
            _api = api;
        }

        //Carga la vista de registrar venta con clientes y productos disponibles
        [HttpGet]
        public async Task<IActionResult> Registrar()
        {
            var responseClientes = await _api.GetAsync<object>("api/clientes"); // Obtener clientes
            dynamic jsonClientes = responseClientes;

            var responseProductos = await _api.GetAsync<object>("api/productos"); // Obtener productos
            dynamic jsonProductos = responseProductos;

            var model = new RegistrarVentaViewModel
            {
                Clientes = ((IEnumerable<dynamic>)jsonClientes.data)
                    .Select(c => new SelectListItem { Value = c.email, Text = c.email }).ToList(),

                ProductosDisponibles = ((IEnumerable<dynamic>)jsonProductos.data)
                    .Select(p => new SelectListItem { Value = p.id.ToString(), Text = p.nombre }).ToList()
            };

            return View(model);
        }

        // Recibe los datos de la venta y llama a la API para registrar
        [HttpPost]
        public async Task<IActionResult> Registrar(RegistrarVentaViewModel model)
        {
            model.Productos = model.Productos.Where(p => p.Cantidad > 0).ToList(); // Filtrar productos válidos

            var result = await _api.PostAsync<dynamic>("api/ventas/registrar", model); // Enviar al backend

            TempData["Mensaje"] = result?.message?.ToString() ?? "Venta registrada"; // Guardar mensaje temporal

            return RedirectToAction("Registrar"); // Redirigir para mostrar mensaje
        }
    }
}
