using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProgrammersPoint.Interfaces;
using ProgrammersPoint.Models;
using ProgrammersPoint.Repositories;

namespace ProgrammersPoint.Controllers
{
    
    public class HomeController : Controller
    {
        private IPostContext postRepository;
        
        public HomeController(IPostContext postContext)
        {
            postRepository = postContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        //TODO verwijderen?
        public IActionResult Error()
        {
            return View();
        }

        public string HaalOngelezenPostsOp()
        {
            try
            {
                //Verkrijg alle posts die minder dan 1 uur oud zijn
                List<Post> nieuwePosts = postRepository.GetNieuwePosts(-1);
                
                return JsonConvert.SerializeObject(nieuwePosts);

            }
            catch (SqlException)
            {
                return null;
                //TODO 'Stille' catch goed idee? 
                //'Stille' catch zonder waarschuwing aan gebruiker
            }
            
        }
    }
}
