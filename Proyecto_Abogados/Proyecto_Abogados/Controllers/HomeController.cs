using Microsoft.Ajax.Utilities;
using Proyecto_Abogados.Entities;
using Proyecto_Abogados.Models;
using Proyecto_Abogados.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Proyecto_Abogados.Controllers
{
    public class HomeController : Controller
    {
        UsuarioModel model = new UsuarioModel();
        AbogadoModel modelA = new AbogadoModel();
        EspecialidadModel modelE = new EspecialidadModel();
        CitaModel modelC = new CitaModel();
        CitaEspecialidadViewModel viewModel = new CitaEspecialidadViewModel();

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult IniciarSesion(UsuarioEnt entidad)
        {
            var resp = model.IniciarSesion(entidad);

            if (resp != null)
            {
                Session["Id_Usuario"] = resp.Id_Usuario;
                Session["Correo_Usuario"] = resp.Correo_Electronico;
                Session["Nombre_Usuario"] = resp.Nombre;
                Session["Identificacion"] = resp.Identificacion;
                Session["Nombre_Rol_Usuario"] = resp.Nombre_Rol;
                return RedirectToAction("Cita", "Home");
            }
            else
            {
                ViewBag.MsjPantalla = "No se pudo iniciar sesión";
                return View("Login");
            }
        }



        [HttpGet]
        public ActionResult Registro()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RegistrarUsuario(UsuarioEnt entidad)
        {
            entidad.Id_Rol = 2;
            entidad.Estado = true;

            var resp = model.RegistrarUsuario(entidad);

            if (resp > 0)
                return RedirectToAction("Login", "Home");
            else
            {
                ViewBag.MsjPantalla = "No se han podido registrar sus datos";
                return View("login");
            }
        }



        [HttpGet]
        public ActionResult Recuperar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RecuperarClave(UsuarioEnt entidad)
        {
            var resp = model.RecuperarClave(entidad);
            Session["Correo_Electronico"] = entidad.Correo_Electronico;
            Session["Id_Usuario"] = entidad.Id_Usuario;

            if (resp)
                return RedirectToAction("Cambiar", "Home");
            else
            {
                ViewBag.MsjPantalla = "No se ha podido recuperar su acceso";
                return RedirectToAction("Login", "Home");
            }

        }



        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }



        [HttpGet]
        public ActionResult CerrarSesion()
        {
            Session.Clear();
            return RedirectToAction("Login", "Home");
        }



        [HttpGet]
        public ActionResult Cambiar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CambiarClave(UsuarioEnt entidad)
        {
            var respValidarClave = model.IniciarSesion(entidad);
            entidad.Correo_Electronico = Session["Correo_Electronico"].ToString();
            entidad.Id_Usuario = long.Parse(Session["Id_Usuario"].ToString());

            if (respValidarClave == null)
            {
                ViewBag.MsjPantalla = "Contraseña actual incorrecta";
                return View("Cambiar");
            }

            if (entidad.ContrasennaNueva != entidad.ConfirmarContrasennaNueva)
            {
                ViewBag.MsjPantalla = "Las contraseñas no coinciden";
                return View("Cambiar");
            }

            var respCambiarClave = model.CambiarClave(entidad);

            if (respCambiarClave > 0)
                return RedirectToAction("Login", "Home");
            else
            {
                ViewBag.MsjPantalla = "No fue posible cambiar la contraseña";
                return View("Cambiar");
            }

        }

        [HttpPost]
        public ActionResult IngresoSolicitud(UsuarioEnt entidad)
        {
            entidad.Id_Rol = 2;
            entidad.Estado = true;

            var resp = model.RegistrarUsuario(entidad);

            if (resp > 0)
                return RedirectToAction("Login", "Home");
            else
            {
                ViewBag.MsjPantalla = "No se han podido registrar sus datos";
                return View("login");
            }
        }

        [HttpGet]
        public ActionResult About()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Contact()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Blog()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Elements()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Single_blog()
        {
            return View();
        }

        [HttpGet]
        public ActionResult service()
        {
            var datos = modelE.ConsultaEspecialidad();
            return View(datos);
        }

        [HttpGet]
        public ActionResult attorneys()
        {
            var datos = modelA.ConsultaAbogados();
            return View(datos);
        }

        [HttpPost]
        public ActionResult ConsultaGratis(ConsultaEnt entidad)
        {
            var resp = model.ConsultaGratis(entidad);

            if (resp > 0)
                return RedirectToAction("Index", "Home");
            else
            {
                ViewBag.MsjPantalla = "No se han podido registrar sus datos";
                return View("Index");
            }
        }

        [HttpPost]
        public ActionResult FormularioContacto(ConsultaEnt entidad)
        {
            var resp = model.FormularioContacto(entidad);

            if (resp > 0)
                return RedirectToAction("Index", "Home");
            else
            {
                ViewBag.MsjPantalla = "No se han podido registrar sus datos";
                return View("Index");
            }
        }

        [HttpGet]
        public ActionResult cita()
        {
            using (var client = new HttpClient())
            {
                string url = ConfigurationManager.AppSettings["urlApi"].ToString() + "api/ConsultaEspecialidad";
                var datos = modelE.ConsultaEspecialidad();
                HttpResponseMessage resp = client.GetAsync(url).Result;

                if (resp.IsSuccessStatusCode)
                {
                    viewModel.Especialidades = datos;
                }
            }

            return View(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> CrearCita(ViewModels.CitaEspecialidadViewModel viewModel, int IdEspecialidad, int Id_Usuario)
        {
            CitaEnt nuevaCita = viewModel.Cita;
            var resp = await modelC.CrearCita(nuevaCita, IdEspecialidad, Id_Usuario);

            if (resp > 0)
            {
                return RedirectToAction("Index"); 
            }
            else
            {
                ViewBag.MsjPantalla = "No se han podido registrar sus datos";
                return View("Index");
            }
        }

        [HttpGet]
        public ActionResult profile()
        {
            long idUsuario = (long)Session["Id_Usuario"];
            var datos = modelC.InfoUsuarios(idUsuario);
            return View(datos);
        }

    }
}