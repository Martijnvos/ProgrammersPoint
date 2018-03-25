using System.Collections.Generic;
using System.Threading.Tasks;
using ProgrammersPoint.Models;

namespace ProgrammersPoint.Interfaces
{
    public interface ICategorieContext
    {
        Task<List<Categorie>> GetAll();

        Categorie GetByNaam(string naam);

        void Insert(Categorie categorie);

        void Update(Categorie categorie);

        void Delete(Categorie categorie);
    }
}