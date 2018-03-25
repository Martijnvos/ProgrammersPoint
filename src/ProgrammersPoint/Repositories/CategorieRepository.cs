using System.Collections.Generic;
using System.Threading.Tasks;
using ProgrammersPoint.Enums;
using ProgrammersPoint.Interfaces;
using ProgrammersPoint.Models;
using ProgrammersPoint.Models.MSSQLContext;

namespace ProgrammersPoint.Repositories
{
    public class CategorieRepository : ICategorieContext
    {
        private CategorieMSSQLContext context;

        public CategorieRepository(CategorieMSSQLContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Verkrijg alle categorieën in de repository
        /// </summary>
        /// <returns>Een lijst van categorieën.</returns>
        public Task<List<Categorie>> GetAll()
        {
            return context.GetAll();
        }

        /// <summary>
        /// Ontvang een categorie op basis van de naam.
        /// </summary>
        /// <param name="naam">De naam van de categorie.</param>
        /// <returns>Een gevuld categorieobject. </returns>
        public Categorie GetByNaam(string naam)
        {
            return context.GetByNaam(naam);
        }

        /// <summary>
        /// Voeg categorie aan repository toe.
        /// </summary>
        /// <param name="categorie">De categorie om toe te voegen</param>
        /// <returns>Een nieuwe categorieinstantie met de bijbehorende waardes.</returns>
        public void Insert(Categorie categorie)
        {
            context.Insert(categorie);
        }

        /// <summary>
        /// Update de details van de gegeven categorie in de repository.
        /// </summary>
        /// <param name="categorie">Het categorieobject waarvan de informatie geüpdatet moet worden in de database.</param>
        public void Update(Categorie categorie)
        {
            context.Update(categorie);
        }

        /// <summary>
        /// Verwijder een categorie van de repository.
        /// </summary>
        /// <param name="categorie">Het categorieobject waarvan de informatie verwijderd moet worden uit de repository.</param>
        public void Delete(Categorie categorie)
        {
            context.Delete(categorie);
        }
    }
}
