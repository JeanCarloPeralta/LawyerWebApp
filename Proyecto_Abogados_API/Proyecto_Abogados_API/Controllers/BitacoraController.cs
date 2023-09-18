using Proyecto_Abogados_API.Entities;
using Proyecto_Abogados_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Proyecto_Abogados_API.Controllers
{
    public class BitacoraController : ApiController
    {

        [HttpPost]
        [Route("api/RegistrarBitacora")]
        public int RegistrarBitacora(BitacoraEnt entidad)
        {
            using (var bd = new BufeteEntities())
            {
                Bitacora tabla = new Bitacora();
                tabla.Fecha_Hora = entidad.Fecha_Hora;
                tabla.Mensaje = entidad.Mensaje;
                tabla.Origen = entidad.Origen;
                tabla.Id_Usuario = entidad.Id_Usuario;
                tabla.Direccion_IP = entidad.Direccion_IP;

                bd.Bitacora.Add(tabla);
                return bd.SaveChanges();
            }
        }

    }
}
