using System.Collections.Generic;
using ProgrammersPoint.Models;

namespace ProgrammersPoint.ViewModels.Review
{
    public class ReviewOnderPostViewModel
    {
        public List<Models.Review> ReviewLijst { get; set; }

        public List<ReviewWaardering> ReviewWaarderingLijst { get; set; }
    }
}
