using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using TEST_DEV.Helpers;
using TEST_DEV.Requests;

namespace TEST_DEV.Models
{
    public class Usuario : Conexion
    {
        public const string USUARIO = "__user__";
        public int ID { get; set; }
        public string Correo { get; set; }
        public string Password { get; set; }
        public bool Activo { get; set; }


        public static Usuario Login(LoginRequest usuario)
        {
            Usuario u = null;
            try
            {
                String query = "SELECT * FROM [dbo].Tb_Usuario U WHERE U.Correo = @Correo AND Activo = @Activo";
                using (SqlConnection con = Conectar())
                {
                    using (SqlCommand command = new SqlCommand(query, con)
                    {
                        CommandType = CommandType.Text,
                        CommandTimeout = 60
                    })
                    {
                        command.Parameters.AddWithValue("@Correo", usuario.Correo);
                        command.Parameters.AddWithValue("@Activo", true);
                        con.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            var i = new
                            {
                                ID = reader.GetOrdinal("ID"),
                                Correo = reader.GetOrdinal("Correo"),
                                Password = reader.GetOrdinal("Password"),
                                Activo = reader.GetOrdinal("Activo")
                            };
                            if (reader.Read())
                            {
                                u = new Usuario();
                                u.ID = reader.GetValor<int>(i.ID);
                                u.Correo = reader.GetValor<string>(i.Correo);
                                u.Password = reader.GetValor<string>(i.Password);
                                u.Activo = reader.GetValor<bool>(i.Activo);
                            }
                        }
                        con.Close();
                    }
                }

                if (u == null)
                {
                    return null;
                }
                if (!MD5Helper.Comprar(u.Password, usuario.Password))
                {
                    return null;
                }
                u.Password = "";
            }
            catch (Exception ex)
            {
            }            
            return u;
        }

        public static void UsuarioPrueba(string correo, string pass, bool activo)
        {
            try
            {
                String query = "INSERT INTO [dbo].Tb_Usuario VALUES(@Correo, @Password, @Activo)";
                using (SqlConnection con = Conectar())
                {
                    using (SqlCommand command = new SqlCommand(query, con)
                    {
                        CommandType = CommandType.Text,
                        CommandTimeout = 60
                    })
                    {
                        command.Parameters.AddWithValue("@Correo", correo);
                        command.Parameters.AddWithValue("@Password", MD5Helper.CalcularHash(pass));
                        command.Parameters.AddWithValue("@Activo", activo);
                        con.Open();
                        command.ExecuteNonQuery();
                        con.Close();
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}