using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using TEST_DEV.Models;
using TEST_DEV.Responses;

namespace TEST_DEV.Requests
{
    public class ViajeRequest
    {
        public static async Task<List<Viaje>> ObtenerViajes(string token)
        {
            List<Viaje> viajes = new List<Viaje>();
            try
            {
                string url = "https://api.toka.com.mx/candidato/api/customers";
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    HttpResponseMessage response = await client.GetAsync(url);
                    string res = await response.Content.ReadAsStringAsync();
                    ViajeResponse viaje = JsonConvert.DeserializeObject<ViajeResponse>(res);
                    viajes = viaje.Data;
                }
            }
            catch (Exception ex)
            {
            }
            return viajes;
        }
    }
}