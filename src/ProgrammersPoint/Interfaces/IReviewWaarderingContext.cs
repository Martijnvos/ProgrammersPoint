using System.Collections.Generic;
using System.Threading.Tasks;
using ProgrammersPoint.Models;

namespace ProgrammersPoint.Interfaces
{
    public interface IReviewWaarderingContext
    {
        Task<List<ReviewWaardering>> GetAll();

        ReviewWaardering GetReviewWaarderingById(int id);

        void InsertOrUpdateReviewWaardering(ReviewWaardering reviewWaardering);

        void DeleteReviewWaardering(ReviewWaardering reviewWaardering);
    }
}