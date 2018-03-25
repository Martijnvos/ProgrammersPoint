using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProgrammersPoint.Interfaces;
using ProgrammersPoint.Models;
using ProgrammersPoint.Repositories;

namespace ProgrammersPoint.Controllers
{
    public class ReviewWaarderingController : Controller
    {
        private IReviewWaarderingContext reviewWaarderingRepository;
        private IGebruikerContext gebruikerRepository;

        public ReviewWaarderingController(IReviewWaarderingContext reviewWaarderingContext, IGebruikerContext gebruikerContext)
        {
            reviewWaarderingRepository = reviewWaarderingContext;
            gebruikerRepository = gebruikerContext;
        }
        
        public async Task<IActionResult> InsertOrUpdateReviewWaardering(int postId, int reviewId, bool upvote, bool downvote, bool report)
        {
            Gebruiker huidigeGebruiker = await gebruikerRepository.GetByNaam(User.Identity.Name);
            int gebruikerId = huidigeGebruiker.GebruikerId;

            ReviewWaardering reviewWaardering = new ReviewWaardering(gebruikerId, reviewId, upvote, downvote, report);

            try
            {
                reviewWaarderingRepository.InsertOrUpdateReviewWaardering(reviewWaardering);
                return RedirectToAction("Post", "Posts", new { id = postId });
            }
            catch (SqlException exp)
            {
                return RedirectToAction("Error", "Errors");
            }
            
        }
    }
}
