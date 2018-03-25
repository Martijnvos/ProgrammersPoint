using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProgrammersPoint.Interfaces;
using ProgrammersPoint.Models;

namespace ProgrammersPoint.Controllers.API
{
    [Route("api/[controller]")]
    public class PostAPIController : Controller
    {
        private IPostContext postContext;

        public PostAPIController(IPostContext postContext)
        {
            this.postContext = postContext;
        }

        [HttpGet("")]
        public async Task<List<Post>> Index()
        {
            return await postContext.GetAll();
        }

        [HttpGet("{id:int}", Name = "GetPostById")]
        public IActionResult GetById([FromQuery] int id)
        {
            Post post = postContext.GetById(id);
            if (post == null)
            {
                return NotFound();
            }
            return new ObjectResult(post);
        }

        [HttpGet("{naam}", Name = "GetPostByNaam")]
        public IActionResult GetByNaam([FromQuery] string naam)
        {
            Post post = postContext.GetByNaam(naam);
            if (post == null)
            {
                return NotFound();
            }
            return new ObjectResult(post);
        }
    }
}
