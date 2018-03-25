using System.ComponentModel.DataAnnotations;

namespace ProgrammersPoint.ViewModels.LogIn
{
    public class AccountLogGebruikerInViewModel
    {
        [Required]
        public string Gebruikersnaam { get; set; }

        [Required]
        public string Wachtwoord { get; set; }
    }
}
