using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TEST_DEV.Helpers.Tabla
{
    public class Tabla
    {
        public Tabla()
        {
            Columnas = new List<Columna>();
            Filas = new List<Fila>();
            FilasPagina = 20;
        }

        public List<Columna> Columnas { get; set; }
        public List<Fila> Filas { get; set; }
        public int FilasPagina { get; set; }
    }
}