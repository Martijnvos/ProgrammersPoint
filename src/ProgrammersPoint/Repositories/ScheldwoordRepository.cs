using System.Collections.Generic;
using System.Threading.Tasks;
using ProgrammersPoint.Interfaces;
using ProgrammersPoint.Models;
using ProgrammersPoint.Models.MSSQLContext;

namespace ProgrammersPoint.Repositories
{
    public class ScheldwoordRepository : IScheldwoordContext
    {
        private ScheldwoordMSSQLContext scheldwoordContext;

        public ScheldwoordRepository(ScheldwoordMSSQLContext scheldwoordContext)
        {
            this.scheldwoordContext = scheldwoordContext;
        }

        public Task<List<Scheldwoord>> GetAll()
        {
            return scheldwoordContext.GetAll();
        }

        public Task<Scheldwoord> GetScheldwoordById(int id)
        {
            return scheldwoordContext.GetScheldwoordById(id);
        }

        public Task<Scheldwoord> GetScheldwoordByNaam(string naam)
        {
            return scheldwoordContext.GetScheldwoordByNaam(naam);
        }

        public void InsertScheldwoord(Scheldwoord scheldwoord)
        {
            scheldwoordContext.InsertScheldwoord(scheldwoord);
        }

        public void UpdateScheldwoord(Scheldwoord scheldwoord)
        {
            scheldwoordContext.UpdateScheldwoord(scheldwoord);
        }

        public void DeleteScheldwoord(string scheldwoord)
        {
            scheldwoordContext.DeleteScheldwoord(scheldwoord);
        }
    }
}
