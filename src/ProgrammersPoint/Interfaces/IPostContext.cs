using System.Collections.Generic;
using System.Threading.Tasks;
using ProgrammersPoint.Enums;
using ProgrammersPoint.Models;

namespace ProgrammersPoint.Interfaces
{
    public interface IPostContext
    {
        List<Post> GetNieuwePosts(int dateTimeVerschil);

        List<Post> GetInteressantePosts(int gebruikerId);

        Task<List<Post>> GetAll();

        Post GetById(int id);

        Post GetByNaam(string naam);

        void RecordCategoryVisit(Categorieën categorie, Gebruiker huidigeGebruiker);

        void Insert(Post post);

        void Update(Post post);

        void Delete(Post post);

        List<Post> GetAllNonAsyncTest();
    }
}
