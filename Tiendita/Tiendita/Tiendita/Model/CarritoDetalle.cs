using System;
using System.Collections.Generic;
using System.Text;

namespace Tiendita.Model
{
    public class CarritoDetalle
    {
        public int Id { get; set; }
        public int IdCarrito { get; set; }
        public int IdProducto { get; set; }
        public int Cantidad { get; set; }
        public int Detalle { get; set; }
    }
}
