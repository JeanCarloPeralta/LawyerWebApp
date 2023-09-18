using Proyecto_Abogados.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace Proyecto_Abogados.Models
{
    public class CitaModel
    {
        public async Task<int> CrearCita(CitaEnt entidad, int IdEspecialidad, int IdUsuario)
        {
            using (var client = new HttpClient())
            {
                string url = ConfigurationManager.AppSettings["urlApi"].ToString() + $"api/CitaUsuario/{IdEspecialidad}/{IdUsuario}";
                JsonContent body = JsonContent.Create(entidad); //Serializar
                HttpResponseMessage resp = client.PostAsync(url, body).Result;

                if (resp.IsSuccessStatusCode)
                {
                    try
                    {
                        string responseContent = await resp.Content.ReadAsStringAsync();
                        return JsonSerializer.Deserialize<int>(responseContent);
                    }
                    catch (JsonException jsonEx)
                    {
                        Console.WriteLine("Error al deserializar JSON: " + jsonEx.Message);
                    }
                }

                return 0;
            }
        }

        public List<CitaEnt> InfoUsuarios(long Id_Usuario)
        {
            using (var client = new HttpClient())
            {
                string url = ConfigurationManager.AppSettings["urlApi"].ToString() + $"api/InfoUsuarios/{Id_Usuario}";
                HttpResponseMessage resp = client.GetAsync(url).Result;

                if (resp.IsSuccessStatusCode)
                {
                    return resp.Content.ReadFromJsonAsync<List<CitaEnt>>().Result;
                }

                return new List<CitaEnt>();
            }
        }
    }
}