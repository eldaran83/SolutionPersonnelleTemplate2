using Microsoft.EntityFrameworkCore;
using SolutionPersonnelleTemplate.Data;
using SolutionPersonnelleTemplate.Models.BLL.Interfaces;
using SolutionPersonnelleTemplate.Models.BO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
        /// peuple la bdd de 6 histoires 
        /// </summary>
        /// <returns></returns>
        public async Task<bool> PeuplerHistoiresBDD()
        {

            try
            {
                ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
                // Pour HISTOIRE
                ///////////////////////////////////////////////////////////////////////////////////////////////////////////////

                //Pour les id utilisateur 
                Utilisateur eldaran = _context.Utilisateurs.Where(u => u.Email == "Eldaran83@gmail.com").FirstOrDefault();
                string eldaranId = eldaran.ApplicationUserID;

                Utilisateur pilou = _context.Utilisateurs.Where(u => u.Email == "Piloupilouvar@hotmail.fr").FirstOrDefault();
                string pilouId = pilou.ApplicationUserID;

                Utilisateur testMembre = _context.Utilisateurs.Where(u => u.Email == "TestMembre@gmail.com").FirstOrDefault();
                string testMembreId = testMembre.ApplicationUserID;

                /////////////////////////////////////////////////////////////////////////////////////////////////////////////
                //On utilise cette facon de faire car sinon on ne peut pas ajouter l'ID 
                /////////////////////////////////////////////////////////////////////////////////////////////////////////////
                using (var db = _context)
                using (var transaction = db.Database.BeginTransaction())
                {
                    //histoire 1
                    Histoire histoire1 = new Histoire {
                        HistoireID = 1,
                        Createur = "Eldaran83",
                        Titre = "La Saga du crépuscule",
                        Synopsis = "Cette aventure est géniale vous devez absolument y participer et relever tous ses défis.",
                        UtilisateurID = eldaranId,
                        NombreDeFoisJouee = 0,
                        Score = 0,
                        UrlMedia = "/images/story-media-default.jpg"
                    };
                    db.Histoires.Add(histoire1);
                    //histoire 2
                    Histoire histoire2 = new Histoire
                    {
                        HistoireID = 2,
                        Createur = "Pilou",
                        Titre = "L'histoire de M. Charles",
                        Synopsis = "Cette aventure est géniale vous devez absolument y participer et relever tous ses défis.",
                        UtilisateurID = pilouId,
                        NombreDeFoisJouee = 0,
                        Score = 0,
                        UrlMedia = "/images/story-media-default.jpg"
                    };
                    _context.Add(histoire2);
                    //histoire 3
                    Histoire histoire3 = new Histoire
                    {
                        HistoireID =3,
                        Createur = "TestMembre",
                        Titre = "La fin d'un empire",
                        Synopsis = "Cette aventure est géniale vous devez absolument y participer et relever tous ses défis.",
                        UtilisateurID = testMembreId,
                        NombreDeFoisJouee = 0,
                        Score = 0,
                        UrlMedia = "/images/story-media-default.jpg"
                    };
                    _context.Add(histoire3);
                    //histoire 4
                    Histoire histoire4 = new Histoire
                    {
                        HistoireID = 4,
                        Createur = "Eldaran83",
                        Titre = "Le démon de la dernière chance",
                        Synopsis = "Cette aventure est géniale vous devez absolument y participer et relever tous ses défis.",
                        UtilisateurID = eldaranId,
                        NombreDeFoisJouee = 0,
                        Score = 0,
                        UrlMedia = "/images/story-media-default.jpg"
                    };
                    _context.Add(histoire4);
                    //histoire 5
                    Histoire histoire5 = new Histoire
                    {
                        HistoireID =5,
                        Createur = "Pilou",
                        Titre = "Pour une pièce de cuivre de plus",
                        Synopsis = "Cette aventure est géniale vous devez absolument y participer et relever tous ses défis.",
                        UtilisateurID = pilouId,
                        NombreDeFoisJouee = 0,
                        Score = 0,
                        UrlMedia = "/images/story-media-default.jpg"
                    };
                    _context.Add(histoire5);

                    //histoire 6
                    Histoire histoire6 = new Histoire
                    {
                        HistoireID = 6,
                        Createur = "TestMembre",
                        Titre = "Pour une pièce de cuivre de plus",
                        Synopsis = "Cette aventure est géniale vous devez absolument y participer et relever tous ses défis.",
                        UtilisateurID = testMembreId,
                        NombreDeFoisJouee = 0,
                        Score = 0,
                        UrlMedia = "/images/story-media-default.jpg"
                    };
                    _context.Add(histoire6);

                    //Facon (IDENTITY_INSERT) pour pouvoir SET soit meme l'ID
                    db.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.Histoires ON;");
                    db.SaveChanges();
                    db.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.Histoires OFF");
                    transaction.Commit();
                }
                return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Le peuplement des histoires s'est mal passé " + ex);
                    return false;
                }

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
        {            try
            {
               // Supprime le dossier qui contient tous les fichiers de l'utilisateur  
                var dirPath = Path.Combine(
                               Directory.GetCurrentDirectory(),
                               "wwwroot" + "/StoryFiles/ImageHistoire/" + Convert.ToString(histoireId) + "_Histoire" + "/");

                Directory.Delete(dirPath, true);


                // var dirPath2 = Path.Combine(
                //Directory.GetCurrentDirectory(),
                //"wwwroot" + "/StoryFiles/" + Convert.ToString(histoireId) + "_Histoire");

                // Directory.Delete(dirPath2, true);
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

                //supression des médias des messages 
                foreach (var item in lesMessagesDeLhistoire)
                {
                    await _messageRepository.RemoveMessageOfStoryById(item.MessageID, item.HistoireID);
                }
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
