using System.ComponentModel.DataAnnotations;

namespace Travel_Nest.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Inserisci l'e-mail o il nome utente")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Inserisci la Password")]
        public string Password { get; set; }

        public bool IsAdmin { get; set; }
        public string ErrorMessage { get; set; }
    }
}
