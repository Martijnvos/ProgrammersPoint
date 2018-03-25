using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProgrammersPoint.Models;
using ProgrammersPoint.Models.MSSQLContext;

namespace ProgrammersPoint.ViewComponents
{
    public class AllePostsListViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            PostMSSQLContext mssqlContext = new PostMSSQLContext();
            List<Post> allePosts = await mssqlContext.GetAll();
            return View(allePosts);
        }
    }
}
