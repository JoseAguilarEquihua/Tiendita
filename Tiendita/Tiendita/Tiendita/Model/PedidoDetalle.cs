using System;
using System.Collections.Generic;
using System.Text;

namespace Tiendita.Model
{
    class PedidoDetalle
    {
        public int Id { get; set; }
        public int IdPedido { get; set; }
        public int IdProducto { get; set; }
        public int Cantidad { get; set; }
        public string Detalle { get; set; }

    }
}
