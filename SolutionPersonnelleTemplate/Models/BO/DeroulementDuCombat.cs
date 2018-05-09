using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static SolutionPersonnelleTemplate.Models.BLL.Managers.MoteurDuJeuManager;

namespace SolutionPersonnelleTemplate.Models.BO
{
    public class DeroulementDuCombat
    {
        public int NombreRound { get; set; }
        public bool? JoueurInit { get; set; }
        public ActionCombat? ActionChoisie { get; set; }
        public Personne LeHeros { get; set; }
        public Personne LeMonstre { get; set; }

    }
}
