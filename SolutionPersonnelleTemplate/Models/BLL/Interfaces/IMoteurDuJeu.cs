using SolutionPersonnelleTemplate.Models.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static SolutionPersonnelleTemplate.Models.BLL.Managers.MoteurDuJeuManager;
using static SolutionPersonnelleTemplate.Models.BO.Personne;

namespace SolutionPersonnelleTemplate.Models.BLL.Interfaces
{
    public interface IMoteurDuJeu
    {
        Task<Personne> GetPersonneByID(int personneID);
        Task<int> DegatsDes(Des.TypeDeDes leTypeDeDesALancer, int? nbDeDesALancer, int? bonusDegat);
        Task<bool> TestCaracteristique(int valeurCaracteristiqueATester, int seuilDeDifficulte);
        int QuelBonusPourLaCaracteristique(int laCaractéristique);
        int PointDeViePremierNiveau(Personne laPersonneDontOnDoitTesterSaCaract, Classe classeDuPerso);
        Task<int> GagnerPointDeViePassageDeNiveau(Personne laPersonneDontOnDoitTesterSaCaract, Classe classeDuPerso);
        int ValeurDeLaCaracteristiqueATester(Personne laPersonneDontOnDoitTesterSaCaract, Caracteristiques caracteristique);
        bool EstVivant(Personne laPersonneDontOnDoitTesterSaCaract);

    }
}
