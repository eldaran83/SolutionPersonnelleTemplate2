﻿using SolutionPersonnelleTemplate.Models.BO;
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
        Task<IEnumerable<Histoire>> GetAllStoryOfUtilisateur(string userId);
        Task<bool> RemoveHistoireById(int? histoireId);
        Task<Histoire> UpdateHistoire(Histoire histoireModele);
        Task<Histoire> GetHistoireByID(int? histoireID);
    }
}
