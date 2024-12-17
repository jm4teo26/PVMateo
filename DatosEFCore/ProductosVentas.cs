using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatosEFCore
{
    [Table("ProductosVentas")]

    public class ProductosVentas
    {
        [ForeignKey("FK_ProductosVentas_Ventas_Id")]
        public int VentaId { get; set; }

        [ForeignKey("FK_ProductosVentas_Productos_Id")]
        public int ProductoId { get; set; }

        [Required]
        public decimal PrecioVenta { get; set; }  // Cambié a decimal para mayor precisión en los precios

        [Required]
        public int Cantidad { get; set; }

        public decimal Total { get; set; }  // Cambié a decimal para manejar montos precisos

        // Para manejo de EF Core
        public Venta Venta { get; set; }
        public Producto Producto { get; set; }
    }
}
