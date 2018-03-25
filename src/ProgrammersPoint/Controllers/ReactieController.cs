using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProgrammersPoint.Interfaces;
using ProgrammersPoint.Models;
using ProgrammersPoint.Models.MSSQLContext;
using ProgrammersPoint.ViewModels;
using ProgrammersPoint.ViewModels.Reactie;


namespace ProgrammersPoint.Controllers
{
    public class ReactieController : Controller
    {
        private IReactieContext reactieContext;
        private IGebruikerContext gebruikerContext;

        public ReactieController(IReactieContext reactieContext, IGebruikerContext gebruikerContext)
        {
            this.reactieContext = reactieContext;
            this.gebruikerContext = gebruikerContext;
        }

        public IActionResult Reageer(int reactieId, int postId)
        {
            Reactie reactie = reactieContext.GetById(reactieId);
            //Reactie die gemaakt wordt verwijst terug naar de aangeklikte reactie
            reactie.ReactieOpReactieId = reactieId;

            ReageerOpReactieViewModel reageerOpReactieViewModel = new ReageerOpReactieViewModel
            {
                Reactie = reactie,
                PostId = postId
            };
            return View(reageerOpReactieViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Reageer(ReageerOpReactieViewModel reageerOpReactieViewModel)
        {
            //Zet gebruikerId van reactie naar ingelogde gebruiker
            Gebruiker huidigeGebruiker = await gebruikerContext.GetByNaam(User.Identity.Name);
            reageerOpReactieViewModel.Reactie.GebruikerId = huidigeGebruiker.GebruikerId;
            reactieContext.Insert(reageerOpReactieViewModel.Reactie);
            return RedirectToAction("Post", "Posts", new { id = reageerOpReactieViewModel.PostId });
        }

        [HttpGet]
        public IActionResult ReageerOpReview(int postId, int reviewId)
        {
            Reactie reactie = new Reactie
            {
                ReviewId = reviewId
            };

            ReageerOpReviewViewModel reageerOpPostViewModel = new ReageerOpReviewViewModel
            {
                PostId = postId,
                Reactie = reactie
            };
            return View(reageerOpPostViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> ReageerOpReview(ReageerOpReviewViewModel vm)
        {
            Gebruiker huidigeGebruiker = await gebruikerContext.GetByNaam(User.Identity.Name);
            vm.Reactie.GebruikerId = huidigeGebruiker.GebruikerId;

            try
            {
                reactieContext.Insert(vm.Reactie);
                return RedirectToAction("Post", "Posts", new { id = vm.PostId });
            }
            catch (SqlException)
            {
                return RedirectToAction("Error", "Errors");
            }
        }

        [HttpGet]
        public IActionResult PasReactieAan(int reactieId, int postId)
        {
            Reactie reactie = reactieContext.GetById(reactieId);
            ReageerOpReviewViewModel vm = new ReageerOpReviewViewModel
            {
                PostId = postId,
                Reactie = reactie
            };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult PasReactieAan(ReageerOpReviewViewModel vm)
        {
            try
            {
                reactieContext.Update(vm.Reactie);
                return RedirectToAction("Post", "Posts", new { id = vm.PostId });
            }
            catch (SqlException)
            {
                return RedirectToAction("Error", "Errors");
            }
        }
    }
}
