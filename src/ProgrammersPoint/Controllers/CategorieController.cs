using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProgrammersPoint.Interfaces;
using ProgrammersPoint.Models;
using ProgrammersPoint.Models.MSSQLContext;

namespace ProgrammersPoint.Controllers
{
    public class CategorieController : Controller
    {
        private ICategorieContext context;

        public CategorieController(ICategorieContext context)
        {
            this.context = context;
        }

        public async Task<IActionResult> Index()
        {
            List<Categorie> categorieLijst = await context.GetAll();
            return View(categorieLijst);
        }

        [HttpGet]
        [Authorize("EditPolicy")]
        public IActionResult PasCategorieAan(string naam)
        {
            Categorie categorie = context.GetByNaam(naam);
            return View(categorie);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize("EditPolicy")]
        public IActionResult PasCategorieAan(Categorie categorie)
        {
            try
            {
                context.Update(categorie);
                return RedirectToAction("Index", "Instellingen");
            }
            catch (SqlException exp)
            {
                //Handel error netjes af
                return RedirectToAction("Error", "Errors");
            }
        }

        public IActionResult VerwijderCategorie(string categorieNaam)
        {
            try
            {
                Categorie categorie = context.GetByNaam(categorieNaam);
                //TODO zorg voor verwijderen foreign key voor verwijderen
                context.Delete(categorie);
                return RedirectToAction("Index");
            } catch(SqlException exp) {
                //Handel error netjes af
                return RedirectToAction("Error");
            }
        }
    }
}
