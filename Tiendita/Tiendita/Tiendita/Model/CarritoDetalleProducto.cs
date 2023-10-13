using System;
using System.Collections.Generic;
using System.Text;

namespace Tiendita.Model
{
    public class CarritoDetalleProducto
    {
        public string Product { get; set; }
        public int Cantidad { get; set; }
        public double Costo { get; set; }
        public string Detalle { get; set; }
        public int IdCarrito { get; set; }
        public int Id { get; set; }
    }
}
