using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto_Abogados_API.Entities
{
    public class CitaEnt
    {
        public long Id_Cita { get; set; }
        public long Id_Abogado { get; set; }
        public long Id_Especialidad { get; set; }
        public string Nombre_Cliente { get; set; }
        public DateTime Fecha_Hora { get; set; }
        public int Telefono { get; set; }
        public string Correo_Electronico { get; set; }
        public int Id_Usuario { get; set; }
    }
}