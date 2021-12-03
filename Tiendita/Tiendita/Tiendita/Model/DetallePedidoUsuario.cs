using System;
using System.Collections.Generic;
using System.Text;

namespace Tiendita.Model
{
    public class DetallePedidoUsuario
    {
        public int IdPedido { get; set; }
        public string Producto { get; set; }
        public int Cantidad { get; set; }
        public double Costo { get; set; }
        public string Detalle { get; set; }
        public double Subtotal { get; set; }
    }
}
