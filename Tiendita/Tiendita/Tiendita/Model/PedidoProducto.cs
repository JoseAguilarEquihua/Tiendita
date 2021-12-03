using System;
using System.Collections.Generic;
using System.Text;

namespace Tiendita.Model
{
    public class PedidoProducto
    {
        public int IdPedido { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Telefono { get; set; }
        public double Total { get; set; }
    }
}
