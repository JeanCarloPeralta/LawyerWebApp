using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto_Abogados_API.Entities
{
    public class ConsultaEnt
    {
        public string Nombre { get; set; }
        public string CorreoElectronico { get; set; }
        public string Motivo { get; set; }
        public string Mensaje { get; set; }
    }
}