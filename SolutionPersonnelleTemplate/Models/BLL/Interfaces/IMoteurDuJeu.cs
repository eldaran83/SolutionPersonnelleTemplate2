using SolutionPersonnelleTemplate.Models.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SolutionPersonnelleTemplate.Models.BLL.Interfaces
{
    public interface IMoteurDuJeu
    {
        Task<Personne> GetPersonneByID(int personneID);
        Task<int> DegatsDes(Des.TypeDeDes leTypeDeDesALancer, int? nbDeDesALancer, int? bonusDegat);
        Task<bool> TestCaracteristique(int valeurCaracteristiqueATester, int seuilDeDifficulte);

    }
}
