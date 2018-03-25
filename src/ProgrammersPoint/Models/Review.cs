namespace ProgrammersPoint.Models
{
    public class Review
    {
        public int ReviewId { get; set; }
        
        public int PostId { get; set; }

        public int GebruikerId { get; set; }

        public string Reviewtekst { get; set; }

        public string Titel { get; set; }

        public Review()
        {

        }

        public Review(int reviewId, int postId, int gebruikerId, string reviewtekst, string titel)
        {
            ReviewId = reviewId;
            PostId = postId;
            GebruikerId = gebruikerId;
            Reviewtekst = reviewtekst;
            Titel = titel;
        }
    }
}
