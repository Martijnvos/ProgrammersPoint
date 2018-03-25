using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProgrammersPoint.Interfaces;
using ProgrammersPoint.Models;
using ProgrammersPoint.ViewModels;
using ProgrammersPoint.ViewModels.Posts;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace ProgrammersPoint.Controllers
{
    public class PostsController : Controller
    {
        private IPostContext postContext;
        private IGebruikerContext gebruikerContext;

        public PostsController(IPostContext postContext, IGebruikerContext gebruikerContext)
        {
            this.postContext = postContext;
            this.gebruikerContext = gebruikerContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult StelPostOp()
        {
            return View();
        }

        public async Task<IActionResult> Post(int id)
        {
            if (id != 0)
            {
                Post post = postContext.GetById(id);

                if (User.Identity.Name != null)
                {
                    Gebruiker huidigeGebruiker = await gebruikerContext.GetByNaam(User.Identity.Name);
                    postContext.RecordCategoryVisit(post.Categorie, huidigeGebruiker);
                }

                return View(post);
            }
            return View(null);
        }

        [Authorize("EditPolicy")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult MaakPostAan(PostStelPostOpViewModel vm)
        {
            Post post = new Post { Categorie = vm.Categorie, Naam = vm.Naam, BeschrijvingTaal = vm.BeschrijvingTaal,
                MoeilijkheidsgraadOnderwerp = vm.MoeilijkheidsgraadOnderwerp, TaalVersie = vm.TaalVersie };
            try
            {
                postContext.Insert(post);
                return RedirectToAction("Index");
            } catch(SqlException exp)
            {
                return RedirectToAction("StelPostOp", vm);
            }
        }

        [HttpGet]
        public IActionResult PasPostAan(int id)
        {
            Post aanTePassenPost = postContext.GetById(id);
            PostPasPostAanViewModel pasPostAanVM = new PostPasPostAanViewModel
            {
                BeschrijvingTaal = aanTePassenPost.BeschrijvingTaal,
                Categorie = aanTePassenPost.Categorie,
                MoeilijkheidsgraadOnderwerp = aanTePassenPost.MoeilijkheidsgraadOnderwerp,
                Naam = aanTePassenPost.Naam,
                PostVersie = aanTePassenPost.PostVersie,
                TaalVersie = aanTePassenPost.TaalVersie
            };
            PostsViewModel postVM = new PostsViewModel { PostId = id, PasPostAanVM = pasPostAanVM };
            return View(postVM);
        }

        [Authorize("EditPolicy")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult PasPostAan(PostsViewModel vm)
        {
            Post post = new Post { PostId = vm.PostId, Categorie = vm.PasPostAanVM.Categorie, Naam = vm.PasPostAanVM.Naam,
                BeschrijvingTaal = vm.PasPostAanVM.BeschrijvingTaal,
                MoeilijkheidsgraadOnderwerp = vm.PasPostAanVM.MoeilijkheidsgraadOnderwerp, PostVersie = vm.PasPostAanVM.PostVersie,
                TaalVersie = vm.PasPostAanVM.TaalVersie };
            try
            {
                postContext.Update(post);
                return RedirectToAction("Post", new { id = vm.PostId });
            } catch (SqlException exp)
            {
                return View("PasPostAan", vm);
            }
        }

        public IActionResult VerwijderPost(int postId)
        {
            try
            {
                Post post = postContext.GetById(postId);
                postContext.Delete(post);
                return RedirectToAction("Index");
            }
            catch (SqlException)
            {
                //TODO exception netjes afhandelen
                return RedirectToAction("Error", "Errors");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ZoekPostOpNaam(ZoektermViewModel vm)
        {
            try
            {
                List<Post> postLijst = await postContext.GetAll();
                List<Post> matchendePosts = new List<Post>();

                if (postLijst == null) return View(null);

                foreach (Post post in postLijst)
                {
                    if (post.Naam.Contains(vm.Zoekterm))
                    {
                        matchendePosts.Add(post); 
                    }
                }

                if (matchendePosts.Count != 0)
                {
                    return View(matchendePosts);
                }
                return View(null);

            }
            catch (SqlException)
            {
                return RedirectToAction("Error", "Errors");
            }
        }
    }
}
