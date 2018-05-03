using SolutionPersonnelleTemplate.Data;
using SolutionPersonnelleTemplate.Models.BLL.Interfaces;
using SolutionPersonnelleTemplate.Models.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using static SolutionPersonnelleTemplate.Models.BO.Personne;

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
        /// retourne le bonus de la caracteristique
        /// </summary>
        /// <param name="laValeurDeCaracteristique"></param>
        /// <returns></returns>
        public int QuelBonusPourLaCaracteristique(int laValeurDeCaracteristique)
        {
            int leBonus = -4;
            switch (laValeurDeCaracteristique)
            {
                case 1:
                case 2:
                case 3:
                    leBonus = -4;
                    break;
                case 4:
                case 5:
                    leBonus = -3;
                    break;
                case 6:
                case 7:
                    leBonus = -2;
                    break;
                case 8:
                case 9:
                    leBonus = -1;
                    break;
                case 10:
                case 11:
                    leBonus = 0;
                    break;
                case 12:
                case 13:
                    leBonus = 1;
                    break;
                case 14:
                case 15:
                    leBonus = 2;
                    break;
                case 16:
                case 17:
                    leBonus = 3;
                    break;
                case 18:
                case 19:
                    leBonus = 4;
                    break;
                default:
                    leBonus = 0; //ca ne doit jamais arriver
                    break;
            }
            return leBonus;
        }

        //les caractéristiques présentes dans le jeu
        public enum Caracteristiques
        {
            // les caract du personnage
            Force,
            Dexterite,
            Constitution,
            Intelligence,
            Sagesse,
            Charisme,
            //les points de vies
            PointsDeVieMax,
            PointsDeVieActuels,
            //l 'expérience et lvl du personnage 
            PointsExperience,

            //pour les combats
            AttaqueMaitriseArme,
            ClasseArmure,

            AttaqueMaitriseMagique,

            //défences
            Reflexe,
            Vigueur,
            Volonte

        }

        public int ValeurDeLaCaracteristiqueATester(Personne laPersonneDontOnDoitTesterSaCaract , Caracteristiques caracteristique)
        {
            int valeurDeLaCaract = 0;

            switch (caracteristique)
            {
                case Caracteristiques.Force:  valeurDeLaCaract = laPersonneDontOnDoitTesterSaCaract.Force;
                    break;
                case Caracteristiques.Intelligence:  valeurDeLaCaract = laPersonneDontOnDoitTesterSaCaract.Intelligence;
                    break;
                case Caracteristiques.Dexterite:  valeurDeLaCaract = laPersonneDontOnDoitTesterSaCaract.Dexterite;
                    break;
                case Caracteristiques.Constitution:  valeurDeLaCaract = laPersonneDontOnDoitTesterSaCaract.Constitution;
                    break;
                case Caracteristiques.Sagesse:
                    valeurDeLaCaract = laPersonneDontOnDoitTesterSaCaract.Sagesse;
                    break;
                case Caracteristiques.Charisme:  valeurDeLaCaract = laPersonneDontOnDoitTesterSaCaract.Sagesse;
                    break;
                //les points de vies
                case Caracteristiques.PointsDeVieMax: valeurDeLaCaract = laPersonneDontOnDoitTesterSaCaract.PointsDeVieMax;
                    break;
                case Caracteristiques.PointsDeVieActuels: valeurDeLaCaract = laPersonneDontOnDoitTesterSaCaract.PointsDeVieActuels;
                    break;
                //l 'expérience et lvl du personnage 
                case Caracteristiques.PointsExperience:  valeurDeLaCaract = laPersonneDontOnDoitTesterSaCaract.PointsExperience;
                    break;

                //pour les combats
                case Caracteristiques.AttaqueMaitriseArme: valeurDeLaCaract = laPersonneDontOnDoitTesterSaCaract.AttaqueMaitriseArme;
                    break; 
               case Caracteristiques.ClasseArmure: valeurDeLaCaract = laPersonneDontOnDoitTesterSaCaract.ClasseArmure;
                    break; 

               case Caracteristiques.AttaqueMaitriseMagique:  valeurDeLaCaract = laPersonneDontOnDoitTesterSaCaract.AttaqueMaitriseMagique;
                    break;
                //pour les défences
                case Caracteristiques.Reflexe:  valeurDeLaCaract = laPersonneDontOnDoitTesterSaCaract.Reflexe;
                    break;
                case Caracteristiques.Vigueur:  valeurDeLaCaract = laPersonneDontOnDoitTesterSaCaract.Vigueur;
                    break;
                case Caracteristiques.Volonte:  valeurDeLaCaract = laPersonneDontOnDoitTesterSaCaract.Volonte;
                    break;
                 default:
                    break;
            }
            return valeurDeLaCaract;
        }

        /// <summary>
        /// valorise les points de vie du perso pour le 1er niveau 
        /// </summary>
        /// <param name="classeDuPerso"></param>
        /// <returns></returns>
        public int PointDeViePremierNiveau(Personne laPersonneDontOnDoitTesterSaCaract, Classe classeDuPerso)
        {
            int pointDeViePremierNiveau = 0;
            switch (laPersonneDontOnDoitTesterSaCaract.ClasseDuPersonnage)
            {
                case Classe.Clerc:
                    pointDeViePremierNiveau = 8 + QuelBonusPourLaCaracteristique(ValeurDeLaCaracteristiqueATester(laPersonneDontOnDoitTesterSaCaract,Caracteristiques.Constitution));
                    break;
                case Classe.Guerrier:
                    pointDeViePremierNiveau = 10 + QuelBonusPourLaCaracteristique(ValeurDeLaCaracteristiqueATester(laPersonneDontOnDoitTesterSaCaract, Caracteristiques.Constitution));
                    break;
                case Classe.Magicien:
                    pointDeViePremierNiveau = 6 + QuelBonusPourLaCaracteristique(ValeurDeLaCaracteristiqueATester(laPersonneDontOnDoitTesterSaCaract, Caracteristiques.Constitution));
                    break;
                case Classe.Roublard:
                    pointDeViePremierNiveau = 8 + QuelBonusPourLaCaracteristique(ValeurDeLaCaracteristiqueATester(laPersonneDontOnDoitTesterSaCaract, Caracteristiques.Constitution));
                    break;
                default:
                    break;
            }
            return pointDeViePremierNiveau;
        }

        /// <summary>
        /// ^point de vie gagner par le perso lors du passage de niveau en fontion de sa classe 
        /// </summary>
        /// <param name="classeDuPerso"></param>
        /// <returns></returns>
        public async Task<int> GagnerPointDeViePassageDeNiveau(Personne laPersonneDontOnDoitTesterSaCaract, Classe classeDuPerso)
        {
            Des leLancerDeDES = new Des();

            int pointDeVieGagnes = 0;
            switch (laPersonneDontOnDoitTesterSaCaract.ClasseDuPersonnage)
            {
                case Classe.Clerc:
                    pointDeVieGagnes = await leLancerDeDES.LanceLeDe(Des.TypeDeDes.D8) + QuelBonusPourLaCaracteristique(ValeurDeLaCaracteristiqueATester(laPersonneDontOnDoitTesterSaCaract, Caracteristiques.Constitution));
                    break;
                case Classe.Guerrier:
                    pointDeVieGagnes = await leLancerDeDES.LanceLeDe(Des.TypeDeDes.D10) + QuelBonusPourLaCaracteristique(ValeurDeLaCaracteristiqueATester(laPersonneDontOnDoitTesterSaCaract, Caracteristiques.Constitution));
                    break;
                case Classe.Magicien:
                    pointDeVieGagnes = await leLancerDeDES.LanceLeDe(Des.TypeDeDes.D6) + QuelBonusPourLaCaracteristique(ValeurDeLaCaracteristiqueATester(laPersonneDontOnDoitTesterSaCaract, Caracteristiques.Constitution));
                    break;
                case Classe.Roublard:
                    pointDeVieGagnes = await leLancerDeDES.LanceLeDe(Des.TypeDeDes.D8) + QuelBonusPourLaCaracteristique(ValeurDeLaCaracteristiqueATester(laPersonneDontOnDoitTesterSaCaract, Caracteristiques.Constitution));
                    break;
                default:
                    break;
            }
            return pointDeVieGagnes;
        }

        /// <summary>
        /// renvoi true si le perso est en vie
        /// </summary>
        /// <param name="laPersonneDontOnDoitTesterSaCaract"></param>
        /// <returns></returns>
        public bool EstVivant(Personne laPersonneDontOnDoitTesterSaCaract)
        {
            if (laPersonneDontOnDoitTesterSaCaract.PointsDeVieActuels >0)
            {
                return true;
            }
            else
            {
                return false;
            }
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
