using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProgrammersPoint.Models;
using ProgrammersPoint.Models.MSSQLContext;

namespace ProgrammersPoint.ViewComponents
{
    public class GebruikerInfoViewComponent : ViewComponent 
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            GebruikerMSSQLContext mssqlContext = new GebruikerMSSQLContext();
            Gebruiker gebruiker = await mssqlContext.GetByNaam("Testgebruiker");
            return View(gebruiker);
        }
    }
}
