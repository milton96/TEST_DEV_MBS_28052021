﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using TEST_DEV.Helpers;

namespace TEST_DEV.Models
{
    public class PersonaFisica : Conexion
    {
        public int IdPersonaFisica { get; set; }
        public DateTime FechaRegistro { get; set; }
        public DateTime FechaActualizacion { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string RFC { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public int UsuarioAgrega { get; set; }
        public bool Activo { get; set; }

        public static PersonaFisica ObtenerPorId(int id)
        {
            PersonaFisica persona = null;
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
                        command.Parameters.AddWithValue("@ID", id);
                        con.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            var i = new
                            {
                                IdPersonaFisica = reader.GetOrdinal("IdPersonaFisica"),
                                FechaRegistro = reader.GetOrdinal("FechaRegistro"),
                                FechaActualizacion = reader.GetOrdinal("FechaActualizacion"),
                                Nombre = reader.GetOrdinal("Nombre"),
                                ApellidoPaterno = reader.GetOrdinal("ApellidoPaterno"),
                                ApellidoMaterno = reader.GetOrdinal("ApellidoMaterno"),
                                RFC = reader.GetOrdinal("RFC"),
                                FechaNacimiento = reader.GetOrdinal("FechaNacimiento"),
                                UsuarioAgrega = reader.GetOrdinal("UsuarioAgrega"),
                                Activo = reader.GetOrdinal("Activo")
                            };
                            if (reader.Read())
                            {
                                persona = new PersonaFisica();
                                persona.IdPersonaFisica = reader.GetValor<int>(i.IdPersonaFisica);
                                persona.FechaRegistro = reader.GetValor<DateTime>(i.FechaRegistro);
                                persona.FechaActualizacion = reader.GetValor<DateTime>(i.FechaActualizacion);
                                persona.Nombre = reader.GetValor<string>(i.Nombre);
                                persona.ApellidoPaterno = reader.GetValor<string>(i.ApellidoPaterno);
                                persona.ApellidoMaterno  = reader.GetValor<string>(i.ApellidoMaterno);
                                persona.RFC = reader.GetValor<string>(i.RFC);
                                persona.FechaNacimiento = reader.GetValor<DateTime>(i.FechaNacimiento);
                                persona.UsuarioAgrega = reader.GetValor<int>(i.UsuarioAgrega);
                                persona.Activo = reader.GetValor<bool>(i.Activo);
                            }
                        }
                        con.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return persona;
        }

        public static List<PersonaFisica> ObtenerRegistros()
        {
            List<PersonaFisica> personas = new List<PersonaFisica>();
            try
            {
                String query = "SELECT * FROM [dbo].Tb_PersonasFisicas";
                using (SqlConnection con = Conectar())
                {
                    using (SqlCommand command = new SqlCommand(query, con)
                    {
                        CommandType = CommandType.Text,
                        CommandTimeout = 60
                    })
                    {
                        con.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            var i = new
                            {
                                IdPersonaFisica = reader.GetOrdinal("IdPersonaFisica"),
                                FechaRegistro = reader.GetOrdinal("FechaRegistro"),
                                FechaActualizacion = reader.GetOrdinal("FechaActualizacion"),
                                Nombre = reader.GetOrdinal("Nombre"),
                                ApellidoPaterno = reader.GetOrdinal("ApellidoPaterno"),
                                ApellidoMaterno = reader.GetOrdinal("ApellidoMaterno"),
                                RFC = reader.GetOrdinal("RFC"),
                                FechaNacimiento = reader.GetOrdinal("FechaNacimiento"),
                                UsuarioAgrega = reader.GetOrdinal("UsuarioAgrega"),
                                Activo = reader.GetOrdinal("Activo")
                            };
                            while (reader.Read())
                            {
                                PersonaFisica persona = new PersonaFisica();
                                persona.IdPersonaFisica = reader.GetValor<int>(i.IdPersonaFisica);
                                persona.FechaRegistro = reader.GetValor<DateTime>(i.FechaRegistro);
                                persona.FechaActualizacion = reader.GetValor<DateTime>(i.FechaActualizacion);
                                persona.Nombre = reader.GetValor<string>(i.Nombre);
                                persona.ApellidoPaterno = reader.GetValor<string>(i.ApellidoPaterno);
                                persona.ApellidoMaterno = reader.GetValor<string>(i.ApellidoMaterno);
                                persona.RFC = reader.GetValor<string>(i.RFC);
                                persona.FechaNacimiento = reader.GetValor<DateTime>(i.FechaNacimiento);
                                persona.UsuarioAgrega = reader.GetValor<int>(i.UsuarioAgrega);
                                persona.Activo = reader.GetValor<bool>(i.Activo);

                                personas.Add(persona);
                            }
                        }
                        con.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return personas;
        }
    }
}