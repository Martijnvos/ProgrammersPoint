namespace ProgrammersPoint.Models
{
    public class ReviewWaardering
    {
        public int GebruikerId { get; set; }

        public int ReviewId { get; set; }

        public bool Upvote { get; set; }

        public bool Downvote { get; set; }

        public bool Report { get; set; }

        public ReviewWaardering()
        {
            
        }

        public ReviewWaardering(int gebruikerId, int reviewId, bool upvote, bool downvote, bool report)
        {
            GebruikerId = gebruikerId;
            ReviewId = reviewId;
            Upvote = upvote;
            Downvote = downvote;
            Report = report;
        }
    }
}
