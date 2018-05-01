using SolutionPersonnelleTemplate.Models.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SolutionPersonnelleTemplate.Models.BLL.Interfaces
{
  public  interface IPersonneInterface
    {
        Task<Personne> AjouterPersonneAsync(Personne personne);
        Task<int> GetPersonneByID(int personneID);
        Task<Personne> MiseAJourPersonneByID(Personne personne);
    }
}
