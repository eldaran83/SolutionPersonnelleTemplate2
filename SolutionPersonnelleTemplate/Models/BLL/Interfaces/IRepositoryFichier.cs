using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SolutionPersonnelleTemplate.Models.BLL.Interfaces
{
  public  interface IRepositoryFichier
    {
        string SaveFichier(string webRoot, string nameDirectory,string userId, string nomDuDossier, IFormCollection form);
        void RemoveFichier(string path, string fichiersASupprimer);
    }
}
