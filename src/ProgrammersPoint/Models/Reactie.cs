using System;

namespace ProgrammersPoint.Models
{
    public class Reactie
    {
        public int ReactieId { get; set; }

        public int? ReactieOpReactieId { get; set; }

        public int GebruikerId { get; set; }

        public int ReviewId { get; set; }

        public string Inhoud { get; set; }

        public int AantalReports { get; set; }

        public DateTime Datum { get; set; }

        public Reactie()
        {
            
        }

        public Reactie(int reactieId, int? reactieOpReactieId, int gebruikerId, int reviewId, string inhoud, int aantalReports, DateTime datum)
        {
            ReactieId = reactieId;
            ReactieOpReactieId = reactieOpReactieId;
            GebruikerId = gebruikerId;
            ReviewId = reviewId;
            Inhoud = inhoud;
            AantalReports = aantalReports;
            Datum = datum;
        }
    }
}
