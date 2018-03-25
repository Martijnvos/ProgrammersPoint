using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProgrammersPoint.Interfaces;
using ProgrammersPoint.Models;
using ProgrammersPoint.Models.MSSQLContext;
using ProgrammersPoint.ViewModels.Review;

namespace ProgrammersPoint.Controllers
{
    public class ReviewController : Controller
    {
        private IReviewContext reviewContext;
        private IGebruikerContext gebruikerContext;

        public ReviewController(IReviewContext reviewContext, IGebruikerContext gebruikerContext)
        {
            this.reviewContext = reviewContext;
            this.gebruikerContext = gebruikerContext;
        }

        [HttpGet]
        public async Task<IActionResult> ReviewAanmaken(int postId)
        {
            Gebruiker gebruiker = await gebruikerContext.GetByNaam(User.Identity.Name);

            Review review = new Review
            {
                PostId = postId,
                GebruikerId = gebruiker.GebruikerId
            };

            ReviewViewModel reviewViewModel = new ReviewViewModel
            {
                PostId = postId,
                Review = review
            };
            return View(reviewViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult ReviewAanmaken(ReviewViewModel vm)
        {
            try
            {
                reviewContext.InsertReview(vm.Review);
                return RedirectToAction("Post", "Posts", new { id = vm.PostId });
            }
            catch (SqlException exp)
            {
                //TODO handel exception beter af
                return RedirectToAction("Error", "Errors");
            }
        }

        public IActionResult PasReviewAan(int reviewId, int postId)
        {
            Review review = reviewContext.GetReviewById(reviewId);
            ReviewViewModel reviewVM = new ReviewViewModel
            {
                Review = review,
                PostId = postId
            };
            return View(reviewVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult PasReviewAan(ReviewViewModel vm)
        {
            try
            {
                reviewContext.Update(vm.Review);
                return RedirectToAction("Post", "Posts", new {id = vm.PostId});
            }
            catch (SqlException)
            {
                return RedirectToAction("Error", "Errors");
            }
        }
    }
}
