using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProgrammersPoint.Interfaces;
using ProgrammersPoint.Models;

namespace ProgrammersPoint.Controllers.API
{
    [Route("api/[controller]")]
    public class ReviewAPIController : Controller
    {
        private IReviewContext reviewContext;

        public ReviewAPIController(IReviewContext reviewContext)
        {
            this.reviewContext = reviewContext;
        }

        [HttpGet("")]
        public async Task<List<Review>> Index()
        {
            return await reviewContext.GetAll();
        }

        [HttpGet("{id:int}", Name = "GetReviewById")]
        public IActionResult GetById([FromQuery] int id)
        {
            Review review = reviewContext.GetReviewById(id);
            if (review == null)
            {
                return NotFound();
            }
            return new ObjectResult(review);
        }

        [HttpGet("{postId:int}", Name = "GetListByPostId")]
        public async Task<List<Review>> GetListByPostId([FromQuery] int postId)
        {
            return await reviewContext.GetListByPostId(postId);
        }
    }
}
