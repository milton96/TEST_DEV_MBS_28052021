using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TEST_DEV.Helpers;

namespace TEST_DEV.Requests
{
    public class PersonaRequest
    {
        [Required(ErrorMessage = "El nombre es requerido")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El apellido paterno es requerido")]
        public string ApellidoPaterno { get; set; }
        [Required(ErrorMessage = "El apellido materno es requerido")]
        public string ApellidoMaterno { get; set; }
        [Required(ErrorMessage = "El RFC es requerido")]
        [RegularExpression(RegexHelper.RFCPersonaFisica, ErrorMessage = "El RFC no tiene el formato válido para una persona física")]
        public string RFC { get; set; }
        [Required(ErrorMessage = "La fecha de nacimiento es requerida")]
        public string FechaNacimiento { get; set; }
    }
}