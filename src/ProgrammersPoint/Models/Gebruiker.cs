using ProgrammersPoint.Enums;

namespace ProgrammersPoint.Models
{
    public class Gebruiker
    {
        public int GebruikerId { get; set; }

        public int PostCount { get; set; }
 
        public string Gebruikersnaam { get; set; }

        public string Wachtwoord { get; set; }

        public string Badge { get; set; }

        public string Badgetekst { get; set; }

        public int Karma { get; set; }
 
        public string Emailadres { get; set; }

        public Functie Functie { get; set; }

        public Gebruiker()
        {
            
        }
        public Gebruiker(string gebruikersnaam, string wachtwoord, string emailAdres)
        {
            Gebruikersnaam = gebruikersnaam;
            Wachtwoord = wachtwoord;
            Emailadres = emailAdres;
        }

        public Gebruiker(int gebruikerId, int postCount, string gebruikersnaam, string wachtwoord, string badge, string badgetekst, int karma, string emailAdres, Functie functie)
        {
            GebruikerId = gebruikerId;
            PostCount = postCount;
            Gebruikersnaam = gebruikersnaam;
            Wachtwoord = wachtwoord;
            Badge = badge;
            Badgetekst = badgetekst;
            Karma = karma;
            Emailadres = emailAdres;
            Functie = functie;
        }
    }
}
