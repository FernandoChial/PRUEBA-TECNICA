namespace BACKEND.Entities
{
    public class HistorialVenta
    {
        public int VentaId { get; set; }
        public int ClienteId { get; set; }
        public DateTime Fecha { get; set; }
        public string Cliente { get; set; }
        public string Email { get; set; }
        public int ProductoId { get; set; }
        public string Producto { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal TotalLinea { get; set; }
    }
}
