using ProgrammersPoint.Enums;
using System.ComponentModel.DataAnnotations;

namespace ProgrammersPoint.ViewModels
{
    public class PostStelPostOpViewModel
    {
        [Required]
        public Categorieën Categorie { get; set; }

        [Required]
        public string Naam { get; set; }

        [Required]
        public string BeschrijvingTaal { get; set; }

        [Required]
        public int MoeilijkheidsgraadOnderwerp { get; set; }

        [Required]
        public string TaalVersie { get; set; }
    }
}
