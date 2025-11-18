using Microsoft.AspNetCore.Mvc;

namespace FrontEnd.Controllers
{
    public class AuthController : Controller
    {
        //Mostrar pantalla de login
        public IActionResult Login()
        {
            return View();
        }

        //Procesar login
        [HttpPost]
        public IActionResult Login(string usuario, string password)
        {
            // Validación simulada de credenciales
            if (usuario == "admin" && password == "123")
            {
                HttpContext.Session.SetString("Usuario", usuario); // Guardar usuario en sesión
                return RedirectToAction("Productos", "Productos"); // Redirigir a Productos
            }

            // Credenciales incorrectas
            ViewBag.Error = "Credenciales incorrectas (simuladas).";
            return View();
        }

        //Cerrar sesión
        public IActionResult Logout()
        {
            HttpContext.Session.Clear(); // Limpiar sesión
            return RedirectToAction("Login"); // Volver a login
        }
    }
}
