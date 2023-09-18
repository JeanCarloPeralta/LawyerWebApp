using Proyecto_Abogados.Entities;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Json;

namespace Proyecto_Abogados.Models
{
    public class EspecialidadModel
    {

        public List<EspecialidadEnt> ConsultaEspecialidad()
        {
            using (var client = new HttpClient())
            {
                string url = ConfigurationManager.AppSettings["urlApi"].ToString() + "api/ConsultaEspecialidad";
                HttpResponseMessage resp = client.GetAsync(url).Result;

                if (resp.IsSuccessStatusCode)
                {
                    return resp.Content.ReadFromJsonAsync<List<EspecialidadEnt>>().Result;
                }

                return new List<EspecialidadEnt>();
            }
        }


    }
}