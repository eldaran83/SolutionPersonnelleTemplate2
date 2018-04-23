using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SolutionPersonnelleTemplate.Models.BO
{
    public class Partie
    {
        public int PartieID { get; set; }

        //FK
        public string UtilisateurID { get; set; }
        public int HistoireID { get; set; }
        //prop


        //nav 
        public Utilisateur Utilisateur { get; set; }
        public Histoire Histoire { get; set; }
        public EtreVivant EtreVivant { get; set; }
    }
}
