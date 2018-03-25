using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProgrammersPoint.Interfaces;
using ProgrammersPoint.Models;

namespace ProgrammersPoint.Controllers.API
{
    [Route("api/[controller]")]
    public class CategorieAPIController : Controller
    {
        private ICategorieContext categorieContext;

        public CategorieAPIController(ICategorieContext categorieContext)
        {
            this.categorieContext = categorieContext;
        }

        [HttpGet("")]
        public async Task<List<Categorie>> Index()
        {
            return await categorieContext.GetAll();
        }

        [HttpGet("{naam}", Name = "GetCategorie")]
        public IActionResult GetByNaam([FromQuery] string naam)
        {
            Categorie categorie = categorieContext.GetByNaam(naam);
            if (categorie == null)
            {
                return NotFound();
            }
            return new ObjectResult(categorie);
        }
    }
}
