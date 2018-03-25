using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProgrammersPoint.Interfaces;
using ProgrammersPoint.Models;

namespace ProgrammersPoint.Controllers.API
{
    [Route("api/[controller]")]
    public class ReactieAPIController : Controller
    {
        private IReactieContext reactieContext;
        private IReviewContext reviewContext;

        public ReactieAPIController(IReactieContext reactieContext, IReviewContext reviewContext)
        {
            this.reactieContext = reactieContext;
            this.reviewContext = reviewContext;
        }

        [HttpGet("")]
        public List<Reactie> Index()
        {
            return reactieContext.GetAll();
        }

        [HttpGet("{id:int}", Name = "GetReactieById")]
        public IActionResult GetById([FromQuery] int id)
        {
            Reactie reactie = reactieContext.GetById(id);
            if (reactie == null)
            {
                return NotFound();
            }
            return new ObjectResult(reactie);
        }

        [HttpGet("{reviewId:int}", Name = "GetAllReactiesByReview")]
        public async Task<List<Reactie>> GetAllReactiesByReviewId([FromQuery] int reviewId)
        {
            Review review = reviewContext.GetReviewById(reviewId);
            List<Reactie> reactielijst = await reactieContext.GetAllByReview(review);
            return new List<Reactie>(reactielijst);
        }
    }
}
