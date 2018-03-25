using System.Collections.Generic;
using System.Data.SqlClient;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProgrammersPoint.Enums;
using ProgrammersPoint.Interfaces;
using ProgrammersPoint.Models;
using ProgrammersPoint.ViewModels.LogIn;

namespace ProgrammersPoint.Controllers
{
    public class AccountController : Controller
    {
        private IGebruikerContext gebruikerRepository;

        public AccountController(IGebruikerContext gebruikerContext)
        {
            gebruikerRepository = gebruikerContext;
        }

        [HttpGet]
        public IActionResult LogIn()
        {
            return View();
        }

        public IActionResult Uitgelogd()
        {
            return View();
        }

        public async Task<IActionResult> LogGebruikerUit()
        {
            await HttpContext.Authentication.SignOutAsync("CookieAuthenticationScheme");

            return RedirectToAction("Uitgelogd", "Account");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogGebruikerIn(LogInViewModel vm)
        {
            if (!ModelState.IsValid) return View("LogIn", vm);
            
            Gebruiker valideGebruiker = await gebruikerRepository.GetByNaam(vm.LogIn.Gebruikersnaam);

            if (valideGebruiker == null || valideGebruiker.Wachtwoord != vm.LogIn.Wachtwoord) return View("LogIn", vm);
            
            SetupCookieAuthenticatie(valideGebruiker);

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegistreerGebruiker(LogInViewModel registreervm)
        {
            if (!ModelState.IsValid) return View("LogIn", registreervm);

            try
            {
                Gebruiker gebruiker = new Gebruiker {Gebruikersnaam = registreervm.Registreer.Gebruikersnaam, Wachtwoord = registreervm.Registreer.Wachtwoord,
                    Emailadres = registreervm.Registreer.Emailadres};

                bool succesvol = await gebruikerRepository.Insert(gebruiker);
                if (succesvol)
                {
                    Gebruiker toegevoegdeGebruiker = await gebruikerRepository.GetByNaam(gebruiker.Gebruikersnaam);
                    //TODO handel dit soort viewerrors af in error controller
                    if (toegevoegdeGebruiker == null) return View("LogIn", registreervm);

                    SetupCookieAuthenticatie(toegevoegdeGebruiker);

                    return RedirectToAction("Index", "Home");
                }

                return View("LogIn", registreervm);
            }
            catch (SqlException exp)
            {
                return View("LogIn", registreervm);
            }
        }

        public async void SetupCookieAuthenticatie(Gebruiker gebruiker)
        {
            string functieBeheerder = "false";

            if (gebruiker.Functie == Functie.Beheerder)
            {
                functieBeheerder = "true";
            }

            var claims = new List<Claim>
            {
                new Claim("Beheerder", functieBeheerder),
                new Claim(ClaimTypes.Name, gebruiker.Gebruikersnaam),
                new Claim(ClaimTypes.Email, gebruiker.Emailadres)
            };

            var claimsIdentity = new ClaimsIdentity(claims, "CookieAuthenticationScheme");
            var claimsPrinciple = new ClaimsPrincipal(claimsIdentity);

            await HttpContext.Authentication.SignInAsync("CookieAuthenticationScheme", claimsPrinciple);
        }
    }
}
