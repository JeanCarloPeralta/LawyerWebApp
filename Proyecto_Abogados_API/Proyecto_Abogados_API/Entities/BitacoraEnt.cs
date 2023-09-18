using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto_Abogados_API.Entities
{
    public class BitacoraEnt
    {
        public long Id_Bitacora { get; set; }
        public DateTime Fecha_Hora { get; set; }
        public string Mensaje { get; set; }
        public string Origen { get; set; }
        public long Id_Usuario { get; set; }
        public string Direccion_IP { get; set; }

    }
}