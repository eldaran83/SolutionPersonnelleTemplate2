using SolutionPersonnelleTemplate.Models.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SolutionPersonnelleTemplate.Models.BLL.Interfaces
{
    public interface IRepositoryHistoire
    {
          
         Task<Histoire> NouvelleHistoire(Histoire histoireModele);
        Task<bool> HistoireExist(string titreHistoire);

        Task<IEnumerable<Histoire>> GetAllHistoiresAsync();

    }
}
