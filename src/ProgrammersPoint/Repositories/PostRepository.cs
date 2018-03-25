using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProgrammersPoint.Enums;
using ProgrammersPoint.Interfaces;
using ProgrammersPoint.Models;
using ProgrammersPoint.Models.MSSQLContext;

namespace ProgrammersPoint.Repositories
{
    public class PostRepository : IPostContext
    {
        private IPostContext context;

        public PostRepository(IPostContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Returnt lijst met posts die eerder zijn geschreven dan het doorgegeven aantal uur voor de huidige tijd
        /// </summary>
        /// <param name="dateTimeVerschil">Het maximaal aantal uur tussen de laatste update van de post en de huidige tijd</param>
        /// <returns></returns>
        public List<Post> GetNieuwePosts(int dateTimeVerschil)
        {
            return context.GetNieuwePosts(dateTimeVerschil);
        }

        public List<Post> GetInteressantePosts(int gebruikerId)
        {
            return context.GetInteressantePosts(gebruikerId);
        }

        public async Task<List<Post>> GetAll()
        {
            return await context.GetAll();
        }

        public Post GetById(int id)
        {
            return context.GetById(id);
        }

        public Post GetByNaam(string naam)
        {
            return context.GetByNaam(naam);
        }

        public void RecordCategoryVisit(Categorieën categorie, Gebruiker gebruiker)
        {
            context.RecordCategoryVisit(categorie, gebruiker);
        }

        public void Insert(Post post)
        {
            context.Insert(post);
        }

        public void Update(Post post)
        {
            context.Update(post);
        }

        public void Delete(Post post)
        {
            context.Delete(post);
        }

        public List<Post> GetAllNonAsyncTest()
        {
            return context.GetAllNonAsyncTest();
        }

    }
}
