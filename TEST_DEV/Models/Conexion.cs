using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml;

namespace TEST_DEV.Models
{
    public class Conexion
    {
        public static SqlConnection Conectar()
        {
            SqlConnection con = null;
            try
            {
                string query = GetConnectionString();                
                con = new SqlConnection(query);
            }
            catch (Exception ex)
            {
                con = null;
            }
            return con;
        }

        private static string GetConnectionString()
        {
            string filename = String.Format(@"{0}{1}", AppDomain.CurrentDomain.BaseDirectory, @"Config\db_config.xml");
            string server, port, database, user, pass;
            using (var xml = XmlReader.Create(filename))
            {
                xml.ReadToFollowing("server");
                server = xml.ReadElementContentAsString();

                xml.ReadToFollowing("port");
                port = xml.ReadElementContentAsString();

                xml.ReadToFollowing("database");
                database = xml.ReadElementContentAsString();

                xml.ReadToFollowing("user");
                user = xml.ReadElementContentAsString();

                xml.ReadToFollowing("pass");
                pass = xml.ReadElementContentAsString();
            }
            string template = @"Server={0}{1};Database={2};User Id={3};Password={4};";
            if (!port.Equals(String.Empty))
            {
                template = @"Server={0},{1};Database={2};User Id={3};Password={4};";
            }
            return String.Format(template, server, port, database, user, pass);
        }
    }
}