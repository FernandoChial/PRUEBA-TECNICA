namespace BACKEND.Dto
{
    public class RegistrarVentaDto
    {
        public string EmailCliente { get; set; }
        public List<DetalleVentaDto> Productos { get; set; }
    }
}
