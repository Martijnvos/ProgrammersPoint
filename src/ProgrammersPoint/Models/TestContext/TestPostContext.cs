using System.Collections.Generic;
using System.Threading.Tasks;
using ProgrammersPoint.Enums;
using ProgrammersPoint.Interfaces;

namespace ProgrammersPoint.Models.TestContext
{
    public class TestPostContext : IPostContext
    {
        private List<Post> postLijst;

        public TestPostContext()
        {
            postLijst = new List<Post>();
        }

        public List<Post> GetNieuwePosts(int dateTimeVerschil)
        {
            throw new System.NotImplementedException();
        }

        public List<Post> GetInteressantePosts(int gebruikerId)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<Post>> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public Post GetById(int id)
        {
            return postLijst.Find(x => x.PostId == id);
        }

        public Post GetByNaam(string naam)
        {
            return postLijst.Find(x => x.Naam == naam);
        }

        public void RecordCategoryVisit(Categorieën categorie, Gebruiker huidigeGebruiker)
        {
            throw new System.NotImplementedException();
        }

        public void Insert(Post post)
        {
            postLijst.Add(post);
        }

        public void Update(Post post)
        {
            Post teUpdatenPost = postLijst.Find(x => x.PostId == post.PostId);
            int index = postLijst.IndexOf(teUpdatenPost);
            postLijst.RemoveAt(index);
            teUpdatenPost = post;
            postLijst.Add(teUpdatenPost);
        }

        public void Delete(Post post)
        {
            int index = postLijst.IndexOf(post);
            postLijst.RemoveAt(index);
        }

        public List<Post> GetAllNonAsyncTest()
        {
            return postLijst;
        }
    }
}
