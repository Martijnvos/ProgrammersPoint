using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProgrammersPoint.Enums;
using ProgrammersPoint.Interfaces;
using ProgrammersPoint.Models;

namespace ProgrammersPoint.Controllers
{
    public class InstellingenController : Controller
    {
        private IScheldwoordContext scheldwoordContext;
        private IGebruikerContext gebruikerContext;

        public InstellingenController(IScheldwoordContext scheldwoordContext, IGebruikerContext gebruikerContext)
        {
            this.scheldwoordContext = scheldwoordContext;
            this.gebruikerContext = gebruikerContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> ScheldwoordenFilter()
        {
            try
            {
                List<Scheldwoord> scheldwoordenLijst = await scheldwoordContext.GetAll();
                return View(scheldwoordenLijst);
            }
            catch (SqlException exp)
            {
                //TODO handel exception netjes af
                return RedirectToAction("Error", "Errors");
            }
        }

        public IActionResult VoegScheldwoordToe()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize("EditPolicy")]
        public IActionResult VoegScheldwoordToe(Scheldwoord scheldwoord)
        {
            try
            {
                scheldwoordContext.InsertScheldwoord(scheldwoord);
                return RedirectToAction("ScheldwoordenFilter");
            }
            catch (SqlException exp)
            {
                //TODO handel exception netjes af
                return RedirectToAction("Error", "Errors");
            }
        }

        [HttpGet]
        [Authorize("EditPolicy")]
        public async Task<IActionResult> PasScheldwoordAan(string aanTePassenScheldwoord)
        {
            try
            {
                Scheldwoord scheldwoord = await scheldwoordContext.GetScheldwoordByNaam(aanTePassenScheldwoord);
                return View(scheldwoord);
            }
            catch (SqlException exp)
            {
                //TODO handel exception netjes af
                return RedirectToAction("Error", "Errors");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize("EditPolicy")]
        public IActionResult SlaAangepastScheldwoordOp(Scheldwoord nieuwScheldwoord)
        {
            try
            {
                scheldwoordContext.UpdateScheldwoord(nieuwScheldwoord);
                return RedirectToAction("ScheldwoordenFilter");
            }
            catch (SqlException)
            {
                //TODO handel exception netjes af
                return RedirectToAction("Error", "Errors");
            }
        }

        [Authorize("EditPolicy")]
        public IActionResult VerwijderScheldwoord(string teVerwijderenScheldwoord)
        {
            try
            {
                scheldwoordContext.DeleteScheldwoord(teVerwijderenScheldwoord);
                return RedirectToAction("ScheldwoordenFilter");
            }
            catch (SqlException exp)
            {
                //TODO handel exception netjes af
                return RedirectToAction("Error", "Errors");
            }
        }

        public IActionResult GebruikerBeheer()
        {
            try
            {
                List<Gebruiker> gebruikersLijst = gebruikerContext.GetAll();
                return View(gebruikersLijst);
            }
            catch (SqlException)
            {
                return RedirectToAction("Error", "Errors");
            }
        }

        public IActionResult GeefGebruikerBeheerrechten(int gebruikerId)
        {
            try
            {
                Gebruiker gebruiker = gebruikerContext.GetById(gebruikerId);
                gebruiker.Functie = Functie.Beheerder;
                gebruikerContext.Update(gebruiker);
                return RedirectToAction("GebruikerBeheer");
            }
            catch (SqlException)
            {
                return RedirectToAction("Error", "Errors");
            }
        }

        public async Task<IActionResult> VerwijderGebruiker(string gebruikersnaam)
        {
            try
            {
                Gebruiker gebruiker = await gebruikerContext.GetByNaam(gebruikersnaam);
                if (gebruikersnaam == User.Identity.Name)
                {
                    await HttpContext.Authentication.SignOutAsync("CookieAuthenticationScheme");
                    gebruikerContext.Delete(gebruiker);
                    return RedirectToAction("LogIn", "Account");
                    
                }
                gebruikerContext.Delete(gebruiker);
                return RedirectToAction("GebruikerBeheer");
            }
            catch (SqlException)
            {
                return RedirectToAction("Error", "Errors");
            }
        }
    }
}
