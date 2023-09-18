using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto_Abogados_API.Entities
{
    public class AbogadoEnt
    {
        public long Id_Abogado { get; set; }
        public string Nombre_Completo { get; set; }
        public string Correo_Electronico { get; set; }
        public string Nombre_Especialidad { get; set; }
    }
}