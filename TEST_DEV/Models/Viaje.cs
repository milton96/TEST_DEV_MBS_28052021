using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TEST_DEV.Models
{
    public class Viaje
    {
        public int IdViaje { get; set; }
        public int IdCliente { get; set; }
        public int IdEmpleado { get; set; }
        public string RazonSocial { get; set; }
        public string RFC { get; set; }
        public string Nombre { get; set; }
        public string Paterno { get; set; }
        public string Materno { get; set; }
        public string Sucursal { get; set; }
        public DateTime FechaRegistroEmpresa { get; set; }
    }
}