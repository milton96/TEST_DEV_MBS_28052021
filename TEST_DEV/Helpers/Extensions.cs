using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
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
    }
}