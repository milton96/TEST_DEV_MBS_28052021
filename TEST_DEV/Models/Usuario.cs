using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using TEST_DEV.Helpers;
using TEST_DEV.Requests;

namespace TEST_DEV.Models
{
    public class Usuario : Conexion
    {
        public int ID { get; set; }
        public string Correo { get; set; }
        public string Password { get; set; }
        public bool Activo { get; set; }


        public static Usuario Login(LoginRequest usuario)
        {
            Usuario u = null;
            try
            {
                String query = "SELECT * FROM [dbo].Tb_PersonasFisicas P WHERE P.IdPersonaFisica = @ID";
                using (SqlConnection con = Conectar())
                {
                    using (SqlCommand command = new SqlCommand(query, con)
                    {
                        CommandType = CommandType.Text,
                        CommandTimeout = 60
                    })
                    {
                        command.Parameters.AddWithValue("@Correo", usuario.Correo);
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
    }
}