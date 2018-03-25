using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProgrammersPoint.Models;
using ProgrammersPoint.Models.MSSQLContext;

namespace ProgrammersPoint.ViewComponents
{
    public class CategorieListViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            CategorieMSSQLContext mssqlContext = new CategorieMSSQLContext();
            List<Categorie> alleCategorieën = await mssqlContext.GetAll();
            return View(alleCategorieën);
        }
    }
}
