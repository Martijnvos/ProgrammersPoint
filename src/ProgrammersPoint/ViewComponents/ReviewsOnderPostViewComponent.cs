using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProgrammersPoint.Models;
using ProgrammersPoint.Models.MSSQLContext;
using ProgrammersPoint.ViewModels.Review;

namespace ProgrammersPoint.ViewComponents
{
    public class ReviewsOnderPostViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(Post post)
        {
            ReviewMSSQLContext reviewMSSQLContext = new ReviewMSSQLContext();
            ReviewWaarderingMSSQLContext reviewWaarderingMSSQLContext = new ReviewWaarderingMSSQLContext();

            List<Review> reviews = await reviewMSSQLContext.GetListByPostId(post.PostId);
            List<ReviewWaardering> reviewWaarderingen = await reviewWaarderingMSSQLContext.GetAll();

            ReviewOnderPostViewModel reviewOnderPostViewModel = new ReviewOnderPostViewModel
            {
                ReviewLijst = reviews,
                ReviewWaarderingLijst = reviewWaarderingen
            };

            return View(reviewOnderPostViewModel);
        }
    }
}
