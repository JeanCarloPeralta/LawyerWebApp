using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto_Abogados_API.Entities
{
    public class UsuarioEnt
    {
        public long Id_Usuario { get; set; }
        public string Correo_Electronico { get; set; }
        public string Contrasenna { get; set; }
        public string Identificacion { get; set; }
        public string Nombre { get; set; }
        public bool Estado { get; set; }
        public int Id_Rol { get; set; }
        public string Nombre_Rol { get; set; }
        public string ContrasennaNueva { get; set; }
    }
}