using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProgrammersPoint.Enums;
using ProgrammersPoint.Models;
using ProgrammersPoint.Repositories;
using ProgrammersPoint.Models.TestContext;

namespace ProgrammersPointUnitTests
{
    [TestClass]
    public class GebruikerRepositoryTest
    {
        private GebruikerRepository repo;
        private List<Gebruiker> gebruikers;

        public GebruikerRepositoryTest()
        {
            //Arrange
            repo = new GebruikerRepository(new TestGebruikerContext());
            gebruikers = new List<Gebruiker>
            {
                new Gebruiker(1, 0, "Henrie", "henrie", "Niew", "Nieuwe gebruiker", 0, "henrie@hotmail.com",
                    Functie.Gebruiker),
                new Gebruiker(2, 0, "Dirk", "dirk", "Niew", "Nieuwe gebruiker", 0, "dirk@hotmail.com",
                    Functie.Beheerder)
            };

            //Act
            foreach (Gebruiker gebruiker in gebruikers)
            {
                repo.InsertNonAsyncTest(gebruiker);
            }
        }

        [TestMethod]
        public void GetGebruikerById()
        {
            //Assert
            Assert.AreEqual(gebruikers[0], repo.GetById(1), "Gebruikerobjecten komen niet overeen");
        }

        [TestMethod]
        public void GetGebruikerByNaam()
        {
            //Assert
            Assert.AreEqual(gebruikers[0], repo.GetByNaamNonAsyncTest("Henrie"));
        }

        [TestMethod]
        public void UpdateGebruiker()
        {
            Gebruiker harry = new Gebruiker(1, 0, "Harry", "henrie", "Niew", "Nieuwe gebruiker", 0,
                "henrie@hotmail.com",
                Functie.Gebruiker);

            repo.Update(harry);

            //Assert
            Assert.AreEqual(repo.GetById(1), harry);
        }

        [TestMethod]
        public void DeleteGebruiker()
        {
            Gebruiker henrie = new Gebruiker(1, 0, "Henrie", "henrie", "Niew", "Nieuwe gebruiker", 0,
                "henrie@hotmail.com",
                Functie.Gebruiker);

            List<Gebruiker> gebruikers = new List<Gebruiker>
            {
                henrie,
                new Gebruiker(2, 0, "Dirk", "dirk", "Niew", "Nieuwe gebruiker", 0, "dirk@hotmail.com",
                    Functie.Beheerder)
            };

            //Act
            foreach (Gebruiker gebruiker in gebruikers)
            {
                repo.InsertNonAsyncTest(gebruiker);
            }

            repo.Delete(henrie);

            //Assert
            Assert.AreNotEqual(gebruikers, repo.GetAll());
        }
    }
}
