using System.Collections.Generic;
using System.Threading.Tasks;
using ProgrammersPoint.Interfaces;
using ProgrammersPoint.Models;
using ProgrammersPoint.Models.MSSQLContext;

namespace ProgrammersPoint.Repositories
{
    public class ReactieRepository : IReactieContext
    {
        private ReactieMSSQLContext reactieContext;

        public ReactieRepository(ReactieMSSQLContext context)
        {
            reactieContext = context;
        }
        
        public List<Reactie> GetAll()
        {
            return reactieContext.GetAll();
        }

        public async Task<List<Reactie>> GetAllByReview(Review review)
        {
            return await reactieContext.GetAllByReview(review);
        }

        public Reactie GetById(int id)
        {
            return reactieContext.GetById(id);
        }

        public void Insert(Reactie reactie)
        {
            reactieContext.Insert(reactie);
        }

        public void Update(Reactie reactie)
        {
            reactieContext.Update(reactie);
        }

        public void Delete(Reactie reactie)
        {
            reactieContext.Delete(reactie);
        }
    }
}
