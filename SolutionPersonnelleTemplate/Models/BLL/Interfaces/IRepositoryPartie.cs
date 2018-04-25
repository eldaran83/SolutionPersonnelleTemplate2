using SolutionPersonnelleTemplate.Models.BO;
using SolutionPersonnelleTemplate.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SolutionPersonnelleTemplate.Models.BLL.Interfaces
{
   public interface IRepositoryPartie
    {
        Task<Partie> NouvellePartie(CreerSonHerosViewModel herosDeLaPartieModel);
        Task<bool> DejaJouerDeCetteHistoire(int HistoireID, string utilisateurID);
        Task<Partie> GetPartieById(int partieID);
        Task<Partie> GetPartieByUtilisateurAndHistoireID(int HistoireID, string utilisateurID);
    }
}
