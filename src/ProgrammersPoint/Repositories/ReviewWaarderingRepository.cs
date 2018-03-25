using System.Collections.Generic;
using System.Threading.Tasks;
using ProgrammersPoint.Interfaces;
using ProgrammersPoint.Models;
using ProgrammersPoint.Models.MSSQLContext;

namespace ProgrammersPoint.Repositories
{
    public class ReviewWaarderingRepository : IReviewWaarderingContext
    {
        private ReviewWaarderingMSSQLContext reviewWaarderingContext;

        public ReviewWaarderingRepository(ReviewWaarderingMSSQLContext reviewWaarderingContext)
        {
            this.reviewWaarderingContext = reviewWaarderingContext;
        }

        public Task<List<ReviewWaardering>> GetAll()
        {
            return reviewWaarderingContext.GetAll();
        }

        public ReviewWaardering GetReviewWaarderingById(int id)
        {
            return reviewWaarderingContext.GetReviewWaarderingById(id);
        }

        public void InsertOrUpdateReviewWaardering(ReviewWaardering reviewWaardering)
        {
            reviewWaarderingContext.InsertOrUpdateReviewWaardering(reviewWaardering);
        }

        public void DeleteReviewWaardering(ReviewWaardering reviewWaardering)
        {
            reviewWaarderingContext.DeleteReviewWaardering(reviewWaardering);
        }
    }
}
