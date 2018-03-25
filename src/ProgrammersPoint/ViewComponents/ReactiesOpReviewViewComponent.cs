using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProgrammersPoint.Models;
using ProgrammersPoint.Models.MSSQLContext;
using ProgrammersPoint.ViewModels.Reactie;

namespace ProgrammersPoint.ViewComponents
{
    public class ReactiesOpReviewViewComponent : ViewComponent
    {
        private List<Reactie> reactieLijst;
        private List<Reactie> gesorteerdeReactieLijst;

        public async Task<IViewComponentResult> InvokeAsync(int reviewId)
        {
            ReactieMSSQLContext reactieMSSQLContext = new ReactieMSSQLContext();
            ReviewMSSQLContext reviewMSSQLContext = new ReviewMSSQLContext();
            GebruikerMSSQLContext gebruikerMSSQLContext = new GebruikerMSSQLContext();

            Review review = reviewMSSQLContext.GetReviewById(reviewId);

            reactieLijst = await reactieMSSQLContext.GetAllByReview(review);
            gesorteerdeReactieLijst = new List<Reactie>();

            foreach (Reactie reactie in reactieLijst.Where(x => x.ReactieOpReactieId == null))
            {
                gesorteerdeReactieLijst.Add(reactie);
                WalkTreeNode(reactie);
            }

            ReactieViewModel reactieViewModel =
                new ReactieViewModel
                {
                    Review = review,
                    Gebruikers = gebruikerMSSQLContext.GetAll(),
                    ReactieLijst = gesorteerdeReactieLijst
                };
            return View(reactieViewModel);
        }

        public void WalkTreeNode(Reactie reactie)
        {
            foreach (Reactie recursieveReactie in reactieLijst.Where(x => x.ReactieOpReactieId == reactie.ReactieId))
            {
                gesorteerdeReactieLijst.Add(recursieveReactie);
                WalkTreeNode(recursieveReactie);
            }
        }
    }
}
