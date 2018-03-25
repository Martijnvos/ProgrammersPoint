using ProgrammersPoint.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProgrammersPoint.Interfaces
{
    public interface IReviewContext
    {
        Task<List<Review>> GetAll();

        Review GetReviewById(int id);

        Task<List<Review>> GetListByPostId(int id);

        void InsertReview(Review review);

        void Update(Review review);

        void Delete(Review review);
    }
}
