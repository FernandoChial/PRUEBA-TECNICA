using BACKEND.Data;
using BACKEND.Dto;
using BACKEND.Entities;
using BACKEND.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;


namespace BACKEND.Repositories
{
    public class VentaRepository : IVentaRepository
    {
        private readonly AppDbContext _context;

        public VentaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Venta> AddAsync(Venta venta)
        {
            _context.Ventas.Add(venta);
            await _context.SaveChangesAsync();
            return venta;
        }

        public async Task<IEnumerable<HistorialVenta>> HistorialPorCliente(int clienteId)
        {
            // Consulta directamente la ENTIDAD mapeada a la vista
            var data = await _context.HistorialVentas
                .FromSqlInterpolated($@"
                    SELECT *
                    FROM VW_HISTORIAL_VENTAS
                    WHERE ClienteId = {clienteId}
                ")
                .ToListAsync();

            return data;
        }


        public async Task<string> RegistrarVentaAsync(RegistrarVentaDto ventaDto)
        {
            // Crear el dataTable que enviará el TVP al SP
            var detallesTable = new DataTable();
            detallesTable.Columns.Add("ProductoId", typeof(int));
            detallesTable.Columns.Add("Cantidad", typeof(int));

            foreach (var d in ventaDto.Productos)
            {
                detallesTable.Rows.Add(d.ProductoId, d.Cantidad);
            }

            // Crear parámetros
            var paramEmail = new SqlParameter("@ClienteEmail", ventaDto.EmailCliente);

            var paramDetalles = new SqlParameter("@Detalles", detallesTable)
            {
                TypeName = "TVP_DetalleVenta",
                SqlDbType = SqlDbType.Structured
            };

            try
            {
                // Ejecutar SP
                var result = await _context.Database
                    .ExecuteSqlRawAsync("EXEC SP_REGISTRAR_VENTA @ClienteEmail, @Detalles",
                        paramEmail, paramDetalles);

                return "Venta registrada correctamente";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
