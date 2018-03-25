using System.Collections.Generic;
using System.Threading.Tasks;
using ProgrammersPoint.Interfaces;

namespace ProgrammersPoint.Models.TestContext
{
    public class TestGebruikerContext : IGebruikerContext
    {
        private List<Gebruiker> gebruikersLijst;

        public TestGebruikerContext()
        {
            gebruikersLijst = new List<Gebruiker>();
        }

        public List<Gebruiker> GetAll()
        {
            return gebruikersLijst;
        }

        public Gebruiker GetById(int id)
        {
            return gebruikersLijst.Find(x => x.GebruikerId == id);
        }

        public Task<Gebruiker> GetByNaam(string naam)
        {
            throw new System.NotImplementedException();
        }

        public Gebruiker GetByNaamNonAsyncTest(string naam)
        {
            return gebruikersLijst.Find(x => x.Gebruikersnaam == naam);
        }

        public Task<bool> Insert(Gebruiker gebruiker)
        {
            throw new System.NotImplementedException();
        }

        public bool InsertNonAsyncTest(Gebruiker gebruiker)
        {
            gebruikersLijst.Add(gebruiker);
            if (gebruikersLijst.Contains(gebruiker))
            {
                return true;
            }
            return false;
        }

        public void Update(Gebruiker gebruiker)
        {
            Gebruiker teUpdatenGebruiker = gebruikersLijst.Find(x => x.GebruikerId == gebruiker.GebruikerId);
            int index = gebruikersLijst.IndexOf(teUpdatenGebruiker);
            gebruikersLijst.RemoveAt(index);
            teUpdatenGebruiker = gebruiker;
            gebruikersLijst.Add(teUpdatenGebruiker);
        }

        public void Delete(Gebruiker gebruiker)
        {
            int index = gebruikersLijst.IndexOf(gebruiker);
            gebruikersLijst.RemoveAt(index);
        }
    }
}
