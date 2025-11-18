using FrontEnd.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FrontEnd.Controllers
{
    public class ProductosController : Controller
    {
        private readonly ApiService _api; // Servicio para consumir la API

        public ProductosController(ApiService api)
        {
            _api = api;
        }

        //Obtiene la lista de productos y la envía a la vista
        public async Task<IActionResult> Productos()
        {
            // Validar sesión del usuario
            if (HttpContext.Session.GetString("Usuario") == null)
                return RedirectToAction("Login", "Auth");

            var productos = await _api.GetAsync<dynamic>("api/productos"); // Llamada a la API

            if (productos == null)
            {
                ViewBag.Error = "No se pudo obtener los productos."; // Mostrar error si no hay datos
                return View();
            }

            return View(productos.data); // Pasar datos a la vista
        }
    }
}
