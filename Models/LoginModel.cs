using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Travel_Nest.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Inserisci l'Username")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Inserisci la Password")]
        public string Password { get; set; }
        public string ErrorMessage { get; set; }
    }
}