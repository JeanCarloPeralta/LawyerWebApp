using Proyecto_Abogados.Entities;
using Proyecto_Abogados.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyecto_Abogados.Controllers
{
    public class UsuarioController : Controller
    {
        UsuarioModel model = new UsuarioModel();

        [HttpGet]
        public ActionResult ConsultaAbogados()
        {
            var resp = model.ConsultaAbogados();
            return View(resp);
        }

        [HttpGet]
        public ActionResult ConsultaEspecialidad()
        {
            var datos = model.ConsultaEspecialidad();
            return View(datos);
        }

    }
}