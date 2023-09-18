using Proyecto_Abogados.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Json;

namespace Proyecto_Abogados.ViewModels
{
    public class CitaEspecialidadViewModel
    {
        public CitaEnt Cita { get; set; }
        public List<EspecialidadEnt> Especialidades { get; set; }
    }
}