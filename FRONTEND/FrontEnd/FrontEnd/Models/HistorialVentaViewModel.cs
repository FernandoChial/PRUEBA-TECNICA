namespace FrontEnd.Models
{
    public class HistorialVentaViewModel
    {
        public int VentaId { get; set; }
        public DateTime Fecha { get; set; }
        public string Cliente { get; set; }
        public string Email { get; set; }
        public string Producto { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal TotalLinea { get; set; }
    }
}
