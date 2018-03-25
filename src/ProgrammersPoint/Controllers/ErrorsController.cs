using Microsoft.AspNetCore.Mvc;

namespace ProgrammersPoint.Controllers
{
    public class ErrorsController : Controller
    {
        public IActionResult Error(string Id)
        {
            ViewBag.Title = "Verkeerde pagina!";
            //TODO check op meer dan alleen 404
            return View("Error404");
        }

        /// <summary>
        /// Gebruiker is niet geauthorizeerd.
        /// </summary>
        /// <returns>Unauthorized view</returns>
        public IActionResult Unauthorized()
        {
            return View();
        }

        /// <summary>
        /// Gebruiker heeft geen rechten om bepaalde pagina te openen
        /// </summary>
        /// <returns>Forbidden view</returns>
        public IActionResult Forbidden()
        {
            return View();
        }
    }
}
