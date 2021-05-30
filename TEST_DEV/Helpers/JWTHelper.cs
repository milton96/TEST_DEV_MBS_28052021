using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml;
using TEST_DEV.Models;

namespace TEST_DEV.Helpers
{
    public class JWTHelper
    {
        public static async Task<string> GenerarToken(Usuario usuario)
        {
            string secret = GetValor("secret");
            string issuer = GetValor("issuer");
            string audience = GetValor("audience");
            if (!Int32.TryParse(GetValor("expires"), out int expires))
                expires = 5;

            // header
            SymmetricSecurityKey _ssk = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            SigningCredentials _sc = new SigningCredentials(_ssk, SecurityAlgorithms.HmacSha256);
            JwtHeader header = new JwtHeader(_sc);

            // claims
            Claim[] _claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.NameId, usuario.ID.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, usuario.Correo)
            };

            // payload
            JwtPayload _payload = new JwtPayload(issuer, audience, _claims, DateTime.UtcNow, DateTime.UtcNow.AddMinutes(expires));

            // token
            JwtSecurityToken _token = new JwtSecurityToken(header, _payload);

            return new JwtSecurityTokenHandler().WriteToken(_token);
        }

        public static string GetValor(string key)
        {
            string valor = "";
            string filename = String.Format(@"{0}{1}", AppDomain.CurrentDomain.BaseDirectory, @"Config\api_config.xml");
            using (var xml = XmlReader.Create(filename))
            {
                xml.ReadToFollowing(key);
                valor = xml.ReadElementContentAsString();
            }
            return valor;
        }
    }
}