using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TEST_DEV.Helpers;

namespace TEST_DEV.Requests
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "El correo es requerido")]
        [RegularExpression(RegexHelper.Correo, ErrorMessage = "No es un correo eletrónico válido")]
        [DataType(DataType.EmailAddress, ErrorMessage = "No es un correo eletrónico válido")]
        public string Correo { get; set; }
        [Required(ErrorMessage = "La contraseña es requerida")]        
        [DataType(DataType.Password, ErrorMessage = "Contraseña no válida")]
        public string Password { get; set; }
    }
}