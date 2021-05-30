using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TEST_DEV.Helpers
{
    public class RegexHelper
    {
        public const string Correo = @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$";
        public const string RFC = @"^([A-ZÑ&]{3,4}) ?(?:- ?)?(\d{2}(?:0[1-9]|1[0-2])(?:0[1-9]|[12]\d|3[01])) ?(?:- ?)?([A-Z\d]{2})([A\d])$";
        public const string RFCPersonaFisica = @"^([A-ZÑ&]{4}) ?(?:- ?)?(\d{2}(?:0[1-9]|1[0-2])(?:0[1-9]|[12]\d|3[01])) ?(?:- ?)?([A-Z\d]{2})([A\d])$";
    }
}