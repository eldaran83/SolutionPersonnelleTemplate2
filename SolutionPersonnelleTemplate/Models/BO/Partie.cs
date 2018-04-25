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
        public int PersonneID { get; set; }
        //prop
        public int DernierMessageJouer { get; set; }

        //nav 
        public Utilisateur Utilisateur { get; set; }
        public Histoire Histoire { get; set; }
        public Personne Heros { get; set; }
    }
}
