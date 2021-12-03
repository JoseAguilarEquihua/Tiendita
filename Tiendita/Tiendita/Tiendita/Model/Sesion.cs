using System;
using System.Collections.Generic;
using System.Text;

namespace Tiendita.Model
{
    public class Sesion
    {
        public Usuario usuario { get; set; }
        public string Token { get; set; }
    }
}
