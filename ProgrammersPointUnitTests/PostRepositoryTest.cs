using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProgrammersPoint.Enums;
using ProgrammersPoint.Models;
using ProgrammersPoint.Models.TestContext;
using ProgrammersPoint.Repositories;

namespace ProgrammersPointUnitTests
{
    [TestClass]
    public class PostRepositoryTest
    {
        private PostRepository repo;
        private List<Post> posts;

        public PostRepositoryTest()
        {
            repo = new PostRepository(new TestPostContext());
            posts = new List<Post>
            {
                new Post(1, Categorieën.C, "C", "De taal C", 4, 1, "2.3.6", DateTime.Now, 0),
                new Post(2, Categorieën.Java, "Java", "De taal Java", 4, 1, "5.6.3", DateTime.Now, 0)
            };

            foreach (Post post in posts)
            {
                repo.Insert(post);
            }
        }

        [TestMethod]
        public void GetPostById()
        {
            //Assert
            Assert.AreEqual(posts[0], repo.GetById(1), "Postobjecten komen niet overeen");
        }

        [TestMethod]
        public void GetPostByNaam()
        {
            //Assert
            Assert.AreEqual(posts[0], repo.GetByNaam("C"));
        }

        [TestMethod]
        public void UpdatePost()
        {
            Post update = new Post(2, Categorieën.CPlusPlus, "C++", "De taal C++", 4, 2, "5.6.3", DateTime.Now, 0);

            repo.Update(update);

            //Assert
            Assert.AreEqual(repo.GetById(2), update);
        }

        [TestMethod]
        public void DeletePost()
        {
            Post update = new Post(2, Categorieën.Java, "Java", "De taal Java", 4, 1, "5.6.3", DateTime.Now, 0);

            List<Post> posts = new List<Post>
            {
                update,
                new Post(1, Categorieën.C, "C", "De taal C", 4, 1, "2.3.6", DateTime.Now, 0)
            };

            //Act
            foreach (Post post in posts)
            {
                repo.Insert(post);
            }

            repo.Delete(update);

            //Assert
            Assert.AreNotEqual(posts, repo.GetAll());
        }
    }
}
