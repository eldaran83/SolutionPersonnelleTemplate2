using SolutionPersonnelleTemplate.Models.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SolutionPersonnelleTemplate.Models.ViewModels
{
    public class CreerSonHerosViewModel
    {
        public Partie Partie {get;set;}
        public Utilisateur Utilisateur { get; set; }
        public EtreVivant EtreVivant { get; set; }
        public Histoire Histoire { get; set; }

    }
}
