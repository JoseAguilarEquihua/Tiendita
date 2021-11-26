using System;
using System.Collections.Generic;
using System.Text;

namespace Tiendita.Model
{
    public class Usuario
    {
        public string Correo { get; set; }
        public string Contrasenia { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public bool TipoUsuario { get; set; }

    }
}

