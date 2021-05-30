using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TEST_DEV.Helpers.Tabla
{
    public class Fila
    {
        public Fila()
        {
            Celdas = new List<Celda>();
            Mostrar = true;
        }
        public List<Celda> Celdas { get; set; }        
        public bool Mostrar { get; set; }
        public object Propiedades { get; set; }
    }

    public class Celda
    {
        public int Posicion { get; set; }
        public string Valor { get; set; }
    }
}