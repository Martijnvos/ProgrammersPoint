using System.Collections.Generic;
using System.Threading.Tasks;
using ProgrammersPoint.Models;

namespace ProgrammersPoint.Interfaces
{
    public interface IScheldwoordContext
    {
        Task<List<Scheldwoord>> GetAll();

        Task<Scheldwoord> GetScheldwoordById(int id);

        Task<Scheldwoord> GetScheldwoordByNaam(string naam);

        void InsertScheldwoord(Scheldwoord scheldwoord);

        void UpdateScheldwoord(Scheldwoord scheldwoord);

        void DeleteScheldwoord(string scheldwoord);
    }
}