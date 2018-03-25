using System.Collections.Generic;
using ProgrammersPoint.Models;

namespace ProgrammersPoint.ViewModels.Reactie
{
    public class ReactieViewModel
    {
        public Models.Review Review { get; set; }

        public List<Gebruiker> Gebruikers { get; set; }

        public List<Models.Reactie> ReactieLijst { get; set; }
    }
}
