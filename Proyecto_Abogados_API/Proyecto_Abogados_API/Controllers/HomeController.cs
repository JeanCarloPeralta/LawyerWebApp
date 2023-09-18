using Proyecto_Abogados_API.Entities;
using Proyecto_Abogados_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyecto_Abogados_API.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        //[HttpGet]
        //[Route("api/ConsultaEspecialidad")]
        //public List<EspecialidadEnt> ConsultaEspecialidad()
        //{
        //    using (var bd = new BufeteEntities())
        //    {
        //        var datos = (from x in bd.Especialidad
        //                     select x).ToList();

        //        var resp = new List<EspecialidadEnt>();

        //        foreach (var item in datos)
        //        {

        //            var especialidadEnt = new EspecialidadEnt

        //            {

        //                Id_Especialidad = item.Id_Especialidad,
        //                Nombre_Especialidad = item.Nombre_Especialidad,
        //                Descripcion = item.Descripcion

        //            };

        //            return resp;

        //        }
        //    }
        //}
    }
}
