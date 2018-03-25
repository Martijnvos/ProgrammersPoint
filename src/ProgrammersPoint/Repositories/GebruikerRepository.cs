using System.Collections.Generic;
using System.Threading.Tasks;
using ProgrammersPoint.Interfaces;
using ProgrammersPoint.Models;

namespace ProgrammersPoint.Repositories
{
    public class GebruikerRepository : IGebruikerContext
    {
        private IGebruikerContext context;

        public GebruikerRepository(IGebruikerContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Verkrijg alle gebruikers in de repository
        /// </summary>
        /// <returns>Een lijst van gebruikers.</returns>
        public List<Gebruiker> GetAll()
        {
            return context.GetAll();
        }

        /// <summary>
        /// Ontvang een gebruiker op basis van zijn/haar id.
        /// </summary>
        /// <param name="id">Het id van de gebruiker.</param>
        /// <returns>Een gevuld gebruiker object. </returns>
        public Gebruiker GetById(int id)
        {
            return context.GetById(id);
        }

        /// <summary>
        /// Ontvang een gebruiker op basis van zijn/haar gebruikersnaam.
        /// </summary>
        /// <param name="naam">De gebruikersnaam van de gebruiker.</param>
        /// <returns>Een gevuld gebruiker object. </returns>
        public Task<Gebruiker> GetByNaam(string naam)
        {
            return context.GetByNaam(naam);
        }

        /// <summary>
        /// Voeg gebruiker aan repository toe.
        /// </summary>
        /// <param name="gebruiker">De gebruiker om toe te voegen</param>
        /// <returns>Een nieuwe gebruikerinstantie met de bijbehorende waardes.</returns>
        public Task<bool> Insert(Gebruiker gebruiker)
        {
            return context.Insert(gebruiker);
        }

        /// <summary>
        /// Verkrijg gebruiker a.d.h.v. naam voor testklasse
        /// </summary>
        /// <param name="naam">Naam van de gewenste gebruiker</param>
        /// <returns>Gevuld gebruikersobject</returns>
        public Gebruiker GetByNaamNonAsyncTest(string naam)
        {
            return context.GetByNaamNonAsyncTest(naam);
        }

        /// <summary>
        /// Voeg gebruiker toe voor testklasse
        /// </summary>
        /// <param name="gebruiker">Gebruikerobject dat toegevoegd moet worden</param>
        /// <returns>Bool geslaagd</returns>
        public bool InsertNonAsyncTest(Gebruiker gebruiker)
        {
            return context.InsertNonAsyncTest(gebruiker);
        }

        /// <summary>
        /// Update de details van de gegeven gebruiker in de repository.
        /// </summary>
        /// <param name="gebruiker">Het gebruikerobject waarvan de informatie geüpdatet moet worden in de database.</param>
        public void Update(Gebruiker gebruiker)
        {
            context.Update(gebruiker);
        }

        /// <summary>
        /// Verwijder een gebruiker van de repository.
        /// </summary>
        /// <param name="gebruiker">Het gebruikerobject waarvan de informatie verwijderd moet worden uit de repository.</param>
        public void Delete(Gebruiker gebruiker)
        {
            context.Delete(gebruiker);
        }
    }
}
