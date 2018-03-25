using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProgrammersPoint.Interfaces;
using ProgrammersPoint.Models;

namespace ProgrammersPoint.Controllers.API
{
    [Route("api/[controller]")]
    public class ReviewWaarderingAPIController : Controller
    {
        private IReviewWaarderingContext reviewWaarderingContext;

        public ReviewWaarderingAPIController(IReviewWaarderingContext reviewWaarderingContext)
        {
            this.reviewWaarderingContext = reviewWaarderingContext;
        }

        [HttpGet("")]
        public async Task<List<ReviewWaardering>> Index()
        {
            return await reviewWaarderingContext.GetAll();
        }

        [HttpGet("{id:int}", Name = "GetReviewWaarderingById")]
        public IActionResult GetById([FromQuery] int id)
        {
            ReviewWaardering reviewWaardering = reviewWaarderingContext.GetReviewWaarderingById(id);
            if (reviewWaardering == null)
            {
                return NotFound();
            }
            return new ObjectResult(reviewWaardering);
        }
    }
}
