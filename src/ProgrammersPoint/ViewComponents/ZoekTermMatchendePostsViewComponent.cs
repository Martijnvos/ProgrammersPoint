using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProgrammersPoint.Models;

namespace ProgrammersPoint.ViewComponents
{
    public class ZoekTermMatchendePostsViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(List<Post> matchendePosts)
        {
            return View(matchendePosts);
        }
    }
}
