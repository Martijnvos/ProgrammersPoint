
using System.Collections.Generic;
using System.Threading.Tasks;
using ProgrammersPoint.Interfaces;
using ProgrammersPoint.Models;
using ProgrammersPoint.Models.MSSQLContext;

namespace ProgrammersPoint.Repositories
{
    public class ReviewRepository : IReviewContext
    {
        private ReviewMSSQLContext context;

        public ReviewRepository(ReviewMSSQLContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Verkrijg alle reviews in de database
        /// </summary>
        /// <returns>Lijst van reviews</returns>
        public Task<List<Review>> GetAll()
        {
            return context.GetAll();
        }

        /// <summary>
        /// Ontvang een review op basis van het ID.
        /// </summary>
        /// <param name="id">Het id van de review.</param>
        /// <returns>Een gevuld review object. </returns>
        public Review GetReviewById(int id)
        {
            return context.GetReviewById(id);
        }

        /// <summary>
        /// Ontvang een lijst van reviews op basis van het postID.
        /// </summary>
        /// <param name="id">Het id van de post.</param>
        /// <returns>Een gevulde reviewlijst. </returns>
        public Task<List<Review>> GetListByPostId(int id)
        {
            return context.GetListByPostId(id);
        }

        /// <summary>
        /// Voeg reviews aan de repository toe.
        /// </summary>
        /// <param name="review">De review om toe te voegen</param>
        /// <returns>Een nieuwe reviewinstantie met de bijbehorende waardes.</returns>
        public void InsertReview(Review review)
        {
            context.InsertReview(review);
        }

        /// <summary>
        /// Update de details van de gegeven review in de repository.
        /// </summary>
        /// <param name="review">Het reviewobject waarvan de informatie geüpdatet moet worden in de repository.</param>
        public void Update(Review review)
        {
            context.Update(review);
        }

        /// <summary>
        /// Verwijder een review uit de repository.
        /// </summary>
        /// <param name="review">Het reviewobject waarvan de informatie verwijderd moet worden uit de repository.</param>
        public void Delete(Review review)
        {
            context.Delete(review);
        }
    }
}
