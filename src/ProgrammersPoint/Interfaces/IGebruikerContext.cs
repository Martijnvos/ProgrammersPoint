using System.Collections.Generic;
using System.Threading.Tasks;
using ProgrammersPoint.Models;
using ProgrammersPoint.ViewModels;

namespace ProgrammersPoint.Interfaces
{
    public interface IGebruikerContext
    {
        List<Gebruiker> GetAll();

        Gebruiker GetById(int id);

        Task<Gebruiker> GetByNaam(string naam);

        Task<bool> Insert(Gebruiker gebruiker);

        void Update(Gebruiker gebruiker);

        void Delete(Gebruiker gebruiker);

        Gebruiker GetByNaamNonAsyncTest(string naam);

        bool InsertNonAsyncTest(Gebruiker gebruiker);

    }
}
