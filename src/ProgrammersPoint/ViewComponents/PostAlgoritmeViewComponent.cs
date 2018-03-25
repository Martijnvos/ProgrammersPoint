using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProgrammersPoint.Models;
using ProgrammersPoint.Models.MSSQLContext;

namespace ProgrammersPoint.ViewComponents
{
    public class PostAlgoritmeViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            PostMSSQLContext mssqlContext = new PostMSSQLContext();
            GebruikerMSSQLContext gebruikerContext = new GebruikerMSSQLContext();

            Gebruiker gebruiker = await gebruikerContext.GetByNaam(HttpContext.User.Identity.Name);
            List<Post> interessantePosts = mssqlContext.GetInteressantePosts(gebruiker.GebruikerId);
            return View(interessantePosts);
        }
    }
}
