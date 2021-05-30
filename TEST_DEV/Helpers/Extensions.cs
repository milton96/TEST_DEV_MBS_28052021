using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text.RegularExpressions;
using System.Web;

namespace TEST_DEV.Helpers
{
    public static class Extensions
    {
        public static bool IsCorreo(this string value)
        {
            return new Regex(RegexHelper.Correo).IsMatch(value);
        }

        public static T GetValor<T>(this SqlDataReader reader, int index)
        {
            return reader.IsDBNull(index) ? default : (T)reader.GetValue(index);
        }

        public static int GetId(this IIdentity user)
        {
            ClaimsIdentity currentUser = user as ClaimsIdentity;            
            Claim claim = currentUser.FindFirst(ClaimTypes.NameIdentifier);
            if (!Int32.TryParse(claim.Value, out int id))
                throw new Exception("El usuario no es válido");
            return id;
        }

        public static string ToDateFormat(this DateTime dateTime)
        {
            return dateTime.ToString("dd/MM/yyyy");
        }
    }
}