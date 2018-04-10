using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using SolutionPersonnelleTemplate.Data;
using SolutionPersonnelleTemplate.Models.BLL.Interfaces;
using SolutionPersonnelleTemplate.Models.BO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SolutionPersonnelleTemplate.Models.BLL.Managers
{
    public class MessageRepository : IRepositoryMessage
    {
        private readonly ApplicationDbContext _context;
        private readonly IRepositoryFichier _fichierRepository;
        private readonly IHostingEnvironment _env;
        /// <summary>
        /// contructeur 
        /// </summary>
        /// <param name="context"></param>
        public MessageRepository(ApplicationDbContext context, IRepositoryFichier fichierRepository, IHostingEnvironment env)
        {
            _context = context;
            _fichierRepository = fichierRepository;
            _env = env;
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
                NumeroMessageEnfant1 = messageModele.NumeroMessageEnfant1,
                MessageEnfant1 = messageModele.MessageEnfant1,
                NumeroMessageEnfant2 = messageModele.NumeroMessageEnfant2,
                MessageEnfant2 = messageModele.MessageEnfant2,
                NumeroMessageEnfant3 = messageModele.NumeroMessageEnfant3,
                MessageEnfant3 = messageModele.MessageEnfant3,
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

                //Il faut supprimer ici les médias de ce message par la suite 
                 string webRoot = _env.WebRootPath; // récupère l environnement
                string nameDirectory = "/StoryFiles/"; // nomme le dossier dans lequel le média va se retrouver ici MessageFiles pour l image de histoire
                string lemessageId = Convert.ToString(messageId); // sert à la personnalisation du dossier pour l utilisateur
                string nomDuDossier = "/ImageHistoire/" + Convert.ToString(histoireId) + "_Histoire/Messages/"; // variable qui sert à nommer le dossier dans lequel le fichier sera ajouté, ICI c est le dossier Image

                //Comme l utilisateur ne peut avoir qu'un seul avatar, on vérifie avant d'ajouter un fichier
                //que le dossier n'a pas d autre image en supprimant tous les fichiers qui pourraient s y trouver
                try
                {
                    var sourceDir = Path.Combine(
                                Directory.GetCurrentDirectory(), "wwwroot" + nameDirectory + nomDuDossier + lemessageId);

                    string[] listeImage = Directory.GetFiles(sourceDir);
                // Copy picture files.          
                foreach (string f in listeImage)
                {
                    // Remove path from the file name.
                    string fName = f.Substring(sourceDir.Length);
                    _fichierRepository.RemoveFichier(sourceDir, fName);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            try {
                //Supprime le message 
                Message leMessage = await _context.Messages
                                         .Where(m => m.MessageID == messageId)
                                        .Where(m => m.HistoireID == histoireId)
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
