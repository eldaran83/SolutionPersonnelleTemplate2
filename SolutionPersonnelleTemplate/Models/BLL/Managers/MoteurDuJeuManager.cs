using SolutionPersonnelleTemplate.Data;
using SolutionPersonnelleTemplate.Models.BLL.Interfaces;
using SolutionPersonnelleTemplate.Models.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SolutionPersonnelleTemplate.Models.BLL.Managers
{
    public class MoteurDuJeuManager : IMoteurDuJeu
    {       
        private readonly ApplicationDbContext _context;

        public MoteurDuJeuManager(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// renvoi les degats infligés
        /// </summary>
        /// <param name="leDesALancer"></param>
        /// <param name="nbDeDesALancer"></param>
        /// <param name="bonusDegat"></param>
        /// <returns></returns>
        public async Task<int> DegatsDes(Des.TypeDeDes leTypeDeDesALancer, int? nbDeDesALancer, int? bonusDegat)
        {
            int degats = 0;
            Des leDes = new Des();
            while (nbDeDesALancer >0)
            {
                 degats += await leDes.LanceLeDe(leTypeDeDesALancer);
                nbDeDesALancer --;
            }
            if (bonusDegat != null)
            {
                degats += Convert.ToInt16(bonusDegat);
            }

            if (degats < 0) // les degats ne peuvent pas rendre des points de vie !
            {
                degats = 0;
            }
            return degats;
        }

        /// <summary>
        /// renvoi la personne par son ID
        /// </summary>
        /// <param name="personneID"></param>
        /// <returns></returns>
        public async Task<Personne> GetPersonneByID(int personneID)
        {
            return await _context.Personnes.Where(p => p.PersonneID == personneID).FirstOrDefaultAsync();
        }

        /// <summary>
        /// determine si un test de caractéristique est reussi ou pas 
        /// </summary>
        /// <param name="caracteristiqueATester"></param>
        /// <param name="seuilDeDifficulte"></param>
        /// <returns></returns>
        public async Task<bool> TestCaracteristique(int valeurCaracteristiqueATester, int seuilDeDifficulte)
        {
            //je pars sur le principe du D20 ajouté à la caract Versus un seuil de difficulté
            Des leDes = new Des();
           int valeurDuJetDeDes = await leDes.LanceLeDe(Des.TypeDeDes.D20);
            if (await leDes.EstReussiteCritique(valeurDuJetDeDes) || (valeurCaracteristiqueATester + valeurDuJetDeDes >= seuilDeDifficulte))
            {
                return true;
            }
            else if(await leDes.EstEchecCritique(valeurDuJetDeDes) || (valeurCaracteristiqueATester + valeurDuJetDeDes < seuilDeDifficulte))
            {
                return false;
            }
            else
            {
                return false; // ne devrais pas arriver mais au cas où
            }  
            
        }
    }
}
