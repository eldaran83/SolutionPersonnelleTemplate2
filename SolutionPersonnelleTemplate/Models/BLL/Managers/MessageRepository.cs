using Microsoft.EntityFrameworkCore;
using SolutionPersonnelleTemplate.Data;
using SolutionPersonnelleTemplate.Models.BLL.Interfaces;
using SolutionPersonnelleTemplate.Models.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SolutionPersonnelleTemplate.Models.BLL.Managers
{
    public class MessageRepository : IRepositoryMessage
    {
        private readonly ApplicationDbContext _context;
         /// <summary>
        /// contructeur 
        /// </summary>
        /// <param name="context"></param>
        public MessageRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// renvoi tous les messages d'un histoire
        /// </summary>
        /// <param name="histoireID"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Message>> GetAllMessageOfStoryAsync(int histoireID)
        {
            IEnumerable<Message> lesMessagesDeLHistoire = await _context.Messages.Where(m => m.HistoireID == histoireID)
                                                                .OrderBy(m=>m.MessageID).ToListAsync();
             return lesMessagesDeLHistoire;
        }

        /// <summary>
        /// vérifie si le titre du message existe deja ou pas en bdd
        /// </summary>
        /// <param name="titreMessage"></param>
        /// <returns></returns>
        public async Task<bool> messageTitreExist(string titreMessage)
        {
            Message titreExist = await _context.Messages.Where(m => m.Titre.ToUpper() == titreMessage.ToUpper()).FirstOrDefaultAsync();
            if (titreMessage != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// ajoute en bdd un message
        /// </summary>
        /// <param name="messageModele"></param>
        /// <returns></returns>
        public async Task<Message> NouveauMessage(Message messageModele)
        {
            _context.Add(messageModele);
            await _context.SaveChangesAsync();
            return messageModele;
        }
    }
}
