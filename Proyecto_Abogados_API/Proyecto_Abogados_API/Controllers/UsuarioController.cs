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
    public class UsuarioController : ApiController
    {
        [HttpPost]
        [Route("api/IniciarSesion")]
        public UsuarioEnt IniciarSesion(UsuarioEnt entidad)
        {
            using (var bd = new BufeteEntities())
            {
                var datos = (from x in bd.Usuario
                             join y in bd.Rol on x.Id_Rol equals y.Id_Rol
                             where x.Correo_Electronico == entidad.Correo_Electronico
                                      && x.Contrasenna == entidad.Contrasenna
                                      && x.Estado == true
                             select new
                             {
                                 x.Id_Usuario,
                                 x.Correo_Electronico,
                                 x.Nombre,
                                 x.Identificacion,
                                 x.Estado,
                                 x.Id_Rol,
                                 x.Caducidad,
                                 x.Clave_Temporal,
                                 y.Nombre_Rol
                             }).FirstOrDefault();

                if (datos != null)
                {
                    if (datos.Clave_Temporal.Value && datos.Caducidad < DateTime.Now)
                    {
                        return null;
                    }

                    UsuarioEnt resp = new UsuarioEnt();
                    resp.Correo_Electronico = datos.Correo_Electronico;
                    resp.Nombre = datos.Nombre;
                    resp.Identificacion = datos.Identificacion;
                    resp.Estado = datos.Estado;
                    resp.Id_Rol = datos.Id_Rol;
                    resp.Nombre_Rol = datos.Nombre_Rol;
                    resp.Id_Usuario = datos.Id_Usuario;
                    return resp;
                }
                else
                {
                    return null;
                }
            }

        }

        [HttpPost]
        [Route("api/RegistrarUsuario")]
        public int RegistrarUsuario(UsuarioEnt entidad)
        {
            using (var bd = new BufeteEntities())
            {
                var datos = (from x in bd.Usuario where x.Correo_Electronico == entidad.Correo_Electronico
                             select new { x.Id_Usuario }).FirstOrDefault();

                if (datos == null)
                {
                    Usuario tabla = new Usuario();
                    tabla.Correo_Electronico = entidad.Correo_Electronico;
                    tabla.Contrasenna = entidad.Contrasenna;
                    tabla.Identificacion = entidad.Identificacion;
                    tabla.Nombre = entidad.Nombre;
                    tabla.Estado = entidad.Estado;
                    tabla.Id_Rol = entidad.Id_Rol;
                    tabla.Clave_Temporal = false;
                    tabla.Caducidad = DateTime.Now;

                    bd.Usuario.Add(tabla);
                    return bd.SaveChanges();
                }
                else
                {
                    return 0;
                }

            }

        }

        [HttpPost]
        [Route("api/RecuperarClave")]
        public bool RecuperarClave(UsuarioEnt entidad)
        {
            BitacoraModel util = new BitacoraModel();

            using (var bd = new BufeteEntities())
            {
                var id =( from x in bd.Usuario 
                          where x.Correo_Electronico == entidad.Correo_Electronico 
                          select x).FirstOrDefault();
                var datos = (from x in bd.Usuario
                             where x.Correo_Electronico == entidad.Correo_Electronico
                                   && x.Estado == true && x.Id_Usuario == id.Id_Usuario
                             select x).FirstOrDefault();

                if (datos != null)
                {
                    string pass = util.CreatePassword();
                    string mensaje = "Estimado(a): " + datos.Nombre + ". El sistema ha generado una contraseña temporal: " + pass + " Tiene 15 minutos para realizar el trámite de cambiar la contraseña";
                    util.SendEmail(datos.Correo_Electronico, "Recuperar Contraseña", mensaje);

                    //Update de LiQ
                    datos.Contrasenna = pass;
                    datos.Clave_Temporal = true;
                    datos.Caducidad = DateTime.Now.AddMinutes(15);
                    bd.SaveChanges();
                    return true;
                }
            }

            return false;
        }

        [HttpPut]
        [Route("api/CambiarClave")]
        public int CambiarClave(UsuarioEnt entidad)
        {
            using (var bd = new BufeteEntities())
            {
                var id = (from x in bd.Usuario
                          where x.Correo_Electronico == entidad.Correo_Electronico
                          select x).FirstOrDefault();
                var datos = (from x in bd.Usuario
                             where x.Id_Usuario == id.Id_Usuario
                             select x).FirstOrDefault();

                if (datos != null)
                {
                    datos.Contrasenna = entidad.ContrasennaNueva;
                    datos.Clave_Temporal = false;
                    datos.Caducidad = DateTime.Now;
                    return bd.SaveChanges();
                }

                return 0;
            }
        }

        [HttpGet]
        [Route("api/ConsultaAbogados")]
        public List<AbogadoEnt> ConsultaAbogados()
        {
            using (var bd = new BufeteEntities())
            {
                var datos = (from a in bd.Abogado
                             join o in bd.Especialidad on a.Id_Especialidad equals o.Id_Especialidad
                             select new
                             {
                                 Id_Abogado = a.Id_Abogado,
                                 Nombre_Completo = a.Nombre_Completo,
                                 Correo_Electronico = a.Correo_Electronico,
                                 Nombre_Especialidad = o.Nombre_Especialidad
                             }).ToList();

                var resp = new List<AbogadoEnt>();

                foreach (var item in datos)
                {
                    var abogadoEnt = new AbogadoEnt
                    {
                        Id_Abogado = item.Id_Abogado,
                        Nombre_Completo = item.Nombre_Completo,
                        Correo_Electronico = item.Correo_Electronico,
                        Nombre_Especialidad = item.Nombre_Especialidad
                    };
                    resp.Add(abogadoEnt);
                }
                return resp;
            }
        }

        [HttpPut]
        [Route("api/CambiarEstado")]
        public int CambiarEstado(UsuarioEnt entidad)
        {
            using (var bd = new BufeteEntities())
            {
                var datos = (from x in bd.Usuario
                             where x.Id_Usuario == entidad.Id_Usuario
                             select x).FirstOrDefault();

                if (datos != null)
                {
                    bool estadoActual = datos.Estado;

                    datos.Estado = (estadoActual == true ? false : true);
                    return bd.SaveChanges();
                }

                return 0;
            }
        }

        [HttpGet]
        [Route("api/ConsultaUsuario")]
        public UsuarioEnt ConsultaUsuario(long q)
        {
            using (var bd = new BufeteEntities())
            {
                var datos = (from x in bd.Usuario
                             where x.Id_Usuario == q
                             select x).FirstOrDefault();

                if (datos != null)
                {
                    UsuarioEnt resp = new UsuarioEnt();
                    resp.Correo_Electronico = datos.Correo_Electronico;
                    resp.Nombre = datos.Nombre;
                    resp.Identificacion = datos.Identificacion;
                    resp.Estado = datos.Estado;
                    resp.Id_Rol = datos.Id_Rol;
                    resp.Id_Usuario = datos.Id_Usuario;
                    return resp;
                }
                else
                {
                    return null;
                }
            }
        }

        [HttpGet]
        [Route("api/ConsultaRoles")]
        public List<RolEnt> ConsultaRoles()
        {
            using (var bd = new BufeteEntities())
            {
                var datos = (from x in bd.Rol
                             where x.Estado == true
                             select x).ToList();

                if (datos.Count > 0)
                {
                    var resp = new List<RolEnt>();
                    foreach (var item in datos)
                    {
                        resp.Add(new RolEnt
                        {
                            Id_Rol = item.Id_Rol,
                            Nombre_Rol = item.Nombre_Rol,
                        });
                    }
                    return resp;
                }
                else
                {
                    return new List<RolEnt>();
                }
            }
        }

        [HttpPost]
        [Route("api/ConsultaGratis")]
        public int ConsultaGratis(ConsultaEnt entidad)
        {
            BitacoraModel util = new BitacoraModel();
            string mensaje = "Estimado(a): " + entidad.Nombre + ". Su consulta se ha recibido exitosamente. En los próximos días se extenderá una respuesta.";
            util.SendEmail(entidad.CorreoElectronico, "Consulta Bufete", mensaje);

            using (var bd = new BufeteEntities())
            {
                Consulta tabla = new Consulta();
                tabla.Nombre = entidad.Nombre;
                tabla.CorreoElectronico = entidad.CorreoElectronico;
                tabla.Motivo = entidad.Motivo;
                tabla.Mensaje = entidad.Mensaje;
                bd.Consulta.Add(tabla);
                return bd.SaveChanges();
            }
        }

        [HttpPost]
        [Route("api/CitaUsuario/{IdEspecialidad}/{idUsuario}")]
        public IHttpActionResult CitaUsuario(CitaEnt entidad, int IdEspecialidad, int IdUsuario)
        {
            BitacoraModel util = new BitacoraModel();
            string mensaje = "Estimado(a): " + entidad.Nombre_Cliente + ". Su cita se ha reservado exitosamente. " +
                "En los próximos días se extenderá una respuesta." +
                "La cita actual se realizará el día "+entidad.Fecha_Hora;
            util.SendEmail(entidad.Correo_Electronico, "Consulta Bufete", mensaje);

            using (var bd = new BufeteEntities())
            {
                long IDAbogado = 0;
                long Id = IdEspecialidad;
                var abogado = (from x in bd.Abogado
                               where x.Id_Especialidad == Id
                               select x).FirstOrDefault();
                if (abogado == null)
                {
                    IDAbogado = GenerarNumeroAleatorio(1, 3);
                }
                else
                {
                    IDAbogado = abogado.Id_Abogado;
                }

                Cita tabla = new Cita();
                tabla.Nombre_Cliente = entidad.Nombre_Cliente;
                tabla.Id_Abogado = IDAbogado;
                tabla.Fecha_Hora = entidad.Fecha_Hora;
                tabla.Telefono = entidad.Telefono;
                tabla.Correo_Electronico = entidad.Correo_Electronico;
                tabla.Id_Usuario = IdUsuario;
                tabla.Id_Especialidad = IdEspecialidad;

                bd.Cita.Add(tabla);
                bd.SaveChanges();
            }

            return Ok(); // Puedes devolver cualquier otro resultado si es necesario
        }

        static long GenerarNumeroAleatorio(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max + 1);
        }

        [HttpPost]
        [Route("api/FormularioContacto")]
        public int FormularioContacto(ConsultaEnt entidad)
        {
            using (var bd = new BufeteEntities())
            {
                Consulta tabla = new Consulta();
                tabla.Nombre = entidad.Nombre;
                tabla.CorreoElectronico = entidad.CorreoElectronico;
                tabla.Motivo = entidad.Motivo;
                tabla.Mensaje = entidad.Mensaje;
                bd.Consulta.Add(tabla);
                return bd.SaveChanges();
            }
        }

        [HttpGet]
        [Route("api/InfoUsuarios/{Id_Usuario}")]
        public List<CitaEnt> InfoUsuarios(long Id_Usuario)
        {
            using (var bd = new BufeteEntities())
            {
                var datos = (from x in bd.Cita
                             where x.Id_Usuario == Id_Usuario
                             select x).ToList();

                var resp = new List<CitaEnt>();

                foreach (var item in datos)
                {

                    var citaEnt = new CitaEnt
                    {
                        Nombre_Cliente = item.Nombre_Cliente,
                        Correo_Electronico = item.Correo_Electronico,
                        Fecha_Hora = item.Fecha_Hora,
                        Telefono = item.Telefono
                    };
                    resp.Add(citaEnt);
                }

                return resp;
            }
        }

    }
}
