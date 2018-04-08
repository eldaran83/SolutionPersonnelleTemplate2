﻿using Microsoft.EntityFrameworkCore;
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
        public async Task<IEnumerable<Message>> GetAllMessageOfStoryAsync(int? histoireID)
        {
            IEnumerable<Message> lesMessagesDeLHistoire = await _context.Messages
                                                                .Include(h => h.Histoire)
                                                                .Where(m => m.HistoireID == histoireID)
                                                                .OrderBy(m=>m.MessageID).ToListAsync();
             return lesMessagesDeLHistoire;
        }

        /// <summary>
        /// recupere le message ne fonction de son id et de l id de l histoire
        /// </summary>
        /// <param name="messageId"></param>
        /// <param name="HistoireID"></param>
        /// <returns></returns>
        public async Task<Message> GetMessageByMessageIDAndHistoireId(int? messageId, int? histoireID)
        {
            Message leMessage = await _context.Messages
                                 .Include(h=>h.Histoire)
                                 .Where(m => m.MessageID == messageId)
                                 .Where(m => m.HistoireID == histoireID)
                                 .FirstOrDefaultAsync();
            return leMessage;
        }

        /// <summary>
        /// vérifie si le titre du message existe deja ou pas en bdd
        /// </summary>
        /// <param name="titreMessage"></param>
        /// <returns></returns>
        public async Task<bool> messageTitreExistDansCetteHistoire(string titreMessage, int HistoireID)
        {
            Message titreExist = await _context.Messages
                .Where(m => m.Titre.ToUpper().Trim() == titreMessage.ToUpper().Trim())
                .Where(m=>m.HistoireID == HistoireID)
                .FirstOrDefaultAsync();
            if (titreExist != null)
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
            Message messageAAjouter = new Message
            {
                Titre = messageModele.Titre,
                Contenu = messageModele.Contenu,
                HistoireID = messageModele.HistoireID,
                UrlMedia = "/images/message-media-default.jpg"
            };
            _context.Messages.Add(messageAAjouter);
            await _context.SaveChangesAsync();

            return messageAAjouter;
 
        }

        /// <summary>
        /// supprime un message de la bdd lié a une histoire
        /// </summary>
        /// <param name="messageId"></param>
        /// <param name="histoireId"></param>
        /// <returns></returns>
        public async Task<bool> RemoveMessageOfStoryById(int? messageId, int? histoireId)
        {
            //Le message doit etre supprimé en dernier  !!
            try
            {
                //Il faut supprimer ici les médias de ce message par la suite 

                //Supprime le message 
                Message leMessage  = await _context.Messages
                                         .Where(m=>m.MessageID == messageId)
                                        .Where(m=>m.HistoireID == histoireId)
                                        .FirstOrDefaultAsync();

                _context.Messages.Remove(leMessage);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("La supression du message a rencontré un problème :" + ex);
                return false;
            }
        }

        /// <summary>
        /// met a jour un message en BDD
        /// </summary>
        /// <param name="messageModele"></param>
        /// <returns></returns>
        public async Task<Message> UpdateMessage(Message messageModele)
        {
            try
            {
                _context.Update(messageModele);
                await _context.SaveChangesAsync();
                return messageModele;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                Console.WriteLine("La MAJ du message a rencontré un problème :" + ex);
                return messageModele;
            }
        }
    }
}
