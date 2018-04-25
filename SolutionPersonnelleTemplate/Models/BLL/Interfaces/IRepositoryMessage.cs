using SolutionPersonnelleTemplate.Models.BO;
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
        Task<IEnumerable<Message>> GetAllMessageOfStoryAsync(int? histoireID);
        Task<bool> RemoveMessageOfStoryById(int? messageId, int? histoireId);
        Task<Message> UpdateMessage(Message messageModele);
        Task<Message> GetMessageByMessageIDAndHistoireId(int? messageId,int? histoireID);
        Task<Message> RetourneLePremierMessageDeLHistoire(int histoireID);

        //Zone ADMIN
        Task<bool> PeuplerLesMessagesDesHistoire();
    }
}
