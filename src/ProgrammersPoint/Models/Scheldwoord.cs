namespace ProgrammersPoint.Models
{
    public class Scheldwoord
    {
        public int ScheldwoordId { get; set; }
        
        public string Woord { get; set; }

        public Scheldwoord()
        {
            
        }

        public Scheldwoord(int scheldwoordId, string woord)
        {
            ScheldwoordId = scheldwoordId;
            Woord = woord;
        }
    }
}
