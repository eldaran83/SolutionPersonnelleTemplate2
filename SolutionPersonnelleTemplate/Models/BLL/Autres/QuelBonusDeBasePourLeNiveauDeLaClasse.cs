using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static SolutionPersonnelleTemplate.Models.BO.Personne;

namespace SolutionPersonnelleTemplate.Models.BLL.Autres
{
    public class QuelBonusDeBasePourLeNiveauDeLaClasse
    {
        public int ID { get; set; }

        public Classe Classe { get; set; }
        public Niveau Niveau { get; set; }

        public int Bonus { get; set; }

        
    }
}
