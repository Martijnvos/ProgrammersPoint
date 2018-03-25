using System.ComponentModel.DataAnnotations;

namespace ProgrammersPoint.ViewModels.LogIn
{
    public class AccountRegistreerGebruikerViewModel : AccountLogGebruikerInViewModel
    {
        [Required]
        [Compare("Wachtwoord", ErrorMessage = "Wachtwoorden komen niet overeen.")]
        [Display(Name = "Bevestig wachtwoord")]
        public string ConfirmatieWachtwoord { get; set; }

        [Required]
        public string Emailadres { get; set; }
    }
}
