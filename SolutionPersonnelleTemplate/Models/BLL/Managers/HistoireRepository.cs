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
    public class HistoireRepository : IRepositoryHistoire
    {
        private readonly ApplicationDbContext _context;
 
        /// <summary>
        /// contructeur 
        /// </summary>
         /// <param name="context"></param>
        public HistoireRepository(ApplicationDbContext context)
        {
             _context = context;
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
                Createur= createur
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

    }
}
