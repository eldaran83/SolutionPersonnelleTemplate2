﻿using SolutionPersonnelleTemplate.Models.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SolutionPersonnelleTemplate.Models.BLL.Interfaces
{
   public interface IRepositoryMessage
    {
        Task<Message> NouveauMessage(Message messageModele);
        Task<bool> messageTitreExistDansCetteHistoire(string titreMessage, int HistoireID);
        Task<IEnumerable<Message>> GetAllMessageOfStoryAsync(int histoireID);
        Task<Message> UpdateMessage(Message messageModele);
        Task<Message> GetMessageByMessageIDAndHistoireId(int? messageId,int? histoireID);
    }
}
