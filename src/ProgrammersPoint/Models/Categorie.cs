using ProgrammersPoint.Enums;

namespace ProgrammersPoint.Models
{
    public class Categorie
    {
        public Categorieën CategorieWaarde { get; set; }

        public string Naam { get; set; }

        public string Omschrijving { get; set; }

        public Categorie()
        {
            
        }

        public Categorie(Categorieën categorieWaarde, string naam, string omschrijving)
        {
            CategorieWaarde = categorieWaarde;
            Naam = naam;
            Omschrijving = omschrijving;
        }
    }
}
