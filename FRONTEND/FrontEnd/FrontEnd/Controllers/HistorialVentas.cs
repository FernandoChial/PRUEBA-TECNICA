using FrontEnd.Models;
using FrontEnd.Services;
using Microsoft.AspNetCore.Mvc;

namespace FrontEnd.Controllers
{
    public class HistorialVentasController : Controller
    {
        private readonly ApiService _api; // Servicio para consumir la API

        public HistorialVentasController(ApiService api)
        {
            _api = api;
        }

        //Mostrar vista de historial vacía al inicio
        public IActionResult Historial()
        {
            // Validar sesión del usuario
            if (HttpContext.Session.GetString("Usuario") == null)
                return RedirectToAction("Login", "Auth");

            return View(); // Vista inicial sin datos
        }

        //Obtener historial de ventas de un cliente específico
        [HttpPost]
        public async Task<IActionResult> Historial(int clienteId)
        {
            // Validar sesión
            if (HttpContext.Session.GetString("Usuario") == null)
                return RedirectToAction("Login", "Auth");

            // Llamada a la API para obtener historial
            var response = await _api.GetAsync<dynamic>($"api/ventas/historial/{clienteId}");
            if (response == null || response.data == null)
            {
                ViewBag.Error = "No hay datos o cliente inválido"; // Mensaje si no hay datos
                return View();
            }

            // Mapear datos a la vista
            var ventas = ((IEnumerable<dynamic>)response.data)
                .Select(x => new HistorialVentaViewModel
                {
                    VentaId = x.ventaId,
                    Fecha = x.fecha,
                    Cliente = x.cliente,
                    Email = x.email,
                    Producto = x.producto,
                    Cantidad = x.cantidad,
                    PrecioUnitario = x.precioUnitario,
                    TotalLinea = x.totalLinea
                })
                .ToList();

            return View(ventas); // Enviar datos a la vista
        }
    }
}
