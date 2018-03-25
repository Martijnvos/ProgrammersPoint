using System.Collections.Generic;
using System.Threading.Tasks;
using ProgrammersPoint.Models;

namespace ProgrammersPoint.Interfaces
{
    public interface IReactieContext
    {
        List<Reactie> GetAll();

        Task<List<Reactie>> GetAllByReview(Review review);

        Reactie GetById(int id);

        void Insert(Reactie reactie);

        void Update(Reactie reactie);

        void Delete(Reactie reactie);
    }
}