using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TEST_DEV.Helpers.Tabla
{
    public class Columna
    {
        public Columna()
        {
            Posicion = 0;
            Nombre = "";
        }

        public int Posicion { get; set; }
        public string Nombre { get; set; }
    }
}