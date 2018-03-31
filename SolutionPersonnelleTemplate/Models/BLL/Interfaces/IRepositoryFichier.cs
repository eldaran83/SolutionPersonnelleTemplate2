using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SolutionPersonnelleTemplate.Models.BLL.Interfaces
{
  public  interface IRepositoryFichier
    {
        string SaveFichierAvatar(string webRoot, string userId, string nomDuDossier, IFormCollection form);
        void RemoveFichierAvatar(string path, string fichiersASupprimer);
    }
}
