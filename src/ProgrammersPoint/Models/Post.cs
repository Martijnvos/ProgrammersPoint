using System;
using ProgrammersPoint.Enums;

namespace ProgrammersPoint.Models
{
    public class Post
    {
        public int PostId { get; set; }

        public Categorieën Categorie { get; set; }

        public string Naam { get; set; }

        public string BeschrijvingTaal { get; set; }

        public int MoeilijkheidsgraadOnderwerp { get; set; }

        public int PostVersie { get; set; }

        public string TaalVersie { get; set; }

        public DateTime LaatstGeüpdatet { get; set; }

        public int ReportsPostbeschrijving { get; set; }

        public Post()
        {

        }

        public Post(int postId, Categorieën categorie, string naam, string beschrijvingTaal, int moeilijkheidsgraadOnderwerp, int postVersie, string taalVersie, DateTime laatstGeüpdatet, int reportsPostbeschrijving)
        {
            PostId = postId;
            Categorie = categorie;
            Naam = naam;
            BeschrijvingTaal = beschrijvingTaal;
            MoeilijkheidsgraadOnderwerp = moeilijkheidsgraadOnderwerp;
            PostVersie = postVersie;
            TaalVersie = taalVersie;
            LaatstGeüpdatet = laatstGeüpdatet;
            ReportsPostbeschrijving = reportsPostbeschrijving;
        }
    }
}
