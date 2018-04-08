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
    public class HistoireRepository : IRepositoryHistoire
    {
        private readonly ApplicationDbContext _context;
        private readonly IRepositoryMessage _messageRepository;

        public HistoireRepository(ApplicationDbContext context, IRepositoryMessage messageRepository)
        {
            _context = context;
            _messageRepository = messageRepository;
        }

        /// <summary>
        /// ajoute une nouvelle histoire
        /// prend en param l id de l utilisateur
        /// </summary>
        /// <param name="utilisateurID"></param>
        /// <returns></returns>
        public async Task<Histoire> NouvelleHistoire(Histoire histoireModele)
        {
            var utilisateur = await _context.Utilisateurs.Where(u => u.ApplicationUserID == histoireModele.UtilisateurID).FirstOrDefaultAsync();
            string createur = utilisateur.Pseudo;
            Histoire histoireAAjouter = new Histoire
            {
                Titre = histoireModele.Titre,
                NombreDeFoisJouee = 0,
                Score = 0,
                UtilisateurID = histoireModele.UtilisateurID,
                Createur= createur,
                Synopsis = histoireModele.Synopsis,
                UrlMedia = "/images/story-media-default.jpg"
            };

             _context.Histoires.Add(histoireAAjouter);
            await _context.SaveChangesAsync();

            return histoireAAjouter;
        }
         /// <summary>
        /// cherche dans la bdd si l histoire existe ou pas 
        /// fais la recherche sur le titre de l histoire
        /// </summary>
        /// <param name="titreHistoire"></param>
        /// <returns></returns>
        public async Task<bool> HistoireExist(string titreHistoire)
        {
            try
            {
                Histoire histoireAChercher =  await _context.Histoires.Where(h => h.Titre.ToUpper().Trim() == titreHistoire.ToUpper().Trim()).FirstOrDefaultAsync();

                 if (histoireAChercher != null)
                {
                    return true; //l histoire existe en bdd
                }
                else
                {
                    return false; //l histoire n 'existe pas en bdd
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("L'accès a la requête s'est mal passée " + ex);
                return false;
            }
        }
         /// <summary>
        /// renvoie toutes les histoires 
        /// classées par nombre de fois jouées descendant
        /// </summary>
        /// <returns></returns>
        public async  Task<IEnumerable<Histoire>> GetAllHistoiresAsync()
        {
            IEnumerable<Histoire> lesHistoires = await _context.Histoires
                                     .Include(u=>u.Utilisateur)
                                     .OrderByDescending(h=>h.NombreDeFoisJouee)
                                     .ToListAsync();
             return lesHistoires;
        }

        /// <summary>
        /// supprime une histoire
        /// avec tous ses messages
        /// </summary>
        /// <param name="histoireId"></param>
        /// <returns></returns>
        public async Task<bool> RemoveHistoireById(int? histoireId)
        {
            try
            {
                //Supprime le dossier qui contient tous les fichiers de l'utilisateur  
                var dirPath = Path.Combine(
                               Directory.GetCurrentDirectory(),
                               "wwwroot" + "/StoryFiles/" + Convert.ToString(histoireId) + "/");

                Directory.Delete(dirPath, true);
             }
            catch (Exception ex)
            {
                Console.WriteLine("L'histoire n'a pas de dossier image " + ex);
             }

            //L'histoire doit etre supprimé en dernier  !!
            try
            {
                 //suppresion des messages de l histoire
                IEnumerable<Message> lesMessagesDeLhistoire= await _messageRepository.GetAllMessageOfStoryAsync(histoireId);

                //Supprime l histoire
                Histoire histoire = await _context.Histoires
                                         .Where(m => m.HistoireID == histoireId)
                                        .FirstOrDefaultAsync();

                _context.Histoires.Remove(histoire);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("La supression de l histoire a rencontré un problème :" + ex);
                return false;
            }
        }

        /// <summary>
        /// met a jour l histoire
        /// </summary>
        /// <param name="messageModele"></param>
        /// <returns></returns>
        public async Task<Histoire> UpdateHistoire(Histoire histoireModele)
        {
            try
            {
                _context.Update(histoireModele);
                await _context.SaveChangesAsync();
                return histoireModele;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                Console.WriteLine("La MAJ de l histoire a rencontré un problème :" + ex);
                return histoireModele;
            }
        }

        /// <summary>
        /// retourne une histoire grace a son id
        /// </summary>
        /// <param name="histoireID"></param>
        /// <returns></returns>
        public async Task<Histoire> GetHistoireByID(int? histoireID)
        {
            Histoire histoire = await _context.Histoires.Where(h => h.HistoireID == histoireID).FirstOrDefaultAsync();
            return histoire;
        }

        /// <summary>
        /// renvoie toutes les histoires de l utilisateur
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Histoire>> GetAllStoryOfUtilisateur(string userId)
        {
            IEnumerable<Histoire> lesHistoiresDeLUtilisateur = await _context.Histoires.Where(h => h.UtilisateurID == userId).ToListAsync();
            return lesHistoiresDeLUtilisateur;
        }
    }
}
