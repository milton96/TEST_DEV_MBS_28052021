using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using TEST_DEV.Responses;

namespace TEST_DEV.Requests
{
    public class TokenRequest
    {
        public static async Task<string> ObtenerToken()
        {
            string json = "{'Username': 'ucand0021','Password':'yNDVARG80sr@dDPc2yCT!'}";
            string url = "https://api.toka.com.mx/candidato/api/login/authenticate";
            TokenResponse token = new TokenResponse();
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.PostAsync(
                         url,
                         new StringContent(json, Encoding.UTF8, "application/json"));
                    string res = await response.Content.ReadAsStringAsync();
                    token = JsonConvert.DeserializeObject<TokenResponse>(res);
                }
            }
            catch (Exception ex)
            {
                token.Data = "";
            }
            
            return token.Data;
        }
    }
}