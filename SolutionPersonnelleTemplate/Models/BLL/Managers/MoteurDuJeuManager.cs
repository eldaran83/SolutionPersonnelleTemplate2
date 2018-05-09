using SolutionPersonnelleTemplate.Data;
using SolutionPersonnelleTemplate.Models.BLL.Interfaces;
using SolutionPersonnelleTemplate.Models.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using static SolutionPersonnelleTemplate.Models.BO.Personne;
using System.ComponentModel;

namespace SolutionPersonnelleTemplate.Models.BLL.Managers
{
    public class MoteurDuJeuManager : IMoteurDuJeu
    {       
        private readonly ApplicationDbContext _context;

        public MoteurDuJeuManager(ApplicationDbContext context)
        {
            Des _des = new Des();
            _context = context;
        }

        /// <summary>
        /// retourne le bonus de base pour une classe et un niveau donné
        /// le bonus = le bonus par rapport au lvl + le bonus de la caractéristique choisie
        /// </summary>
        /// <param name="laClasseDuPerso"></param>
        /// <param name="leNiveauDuPersonnage"></param>
        /// <returns></returns>
        public int BonusDeBaseDeMaitriseArmesPourLeNiveau(Personne personne)
        {
            int bonusDeBasePourLeNiveau = 0;
                switch (personne.NiveauDuPersonnage)
                {
                    case Niveau.Niveau1:
                    case Niveau.Niveau2:
                    case Niveau.Niveau3:
                    case Niveau.Niveau4:
                    bonusDeBasePourLeNiveau = 2;
                        break;
                    case Niveau.Niveau5:
                    case Niveau.Niveau6:
                    case Niveau.Niveau7:
                    case Niveau.Niveau8:
                    bonusDeBasePourLeNiveau = 3;
                        break;
                    case Niveau.Niveau9:                     
                    case Niveau.Niveau10:
                    case Niveau.Niveau11:
                    case Niveau.Niveau12:
                    bonusDeBasePourLeNiveau = 4;
                        break;
                    case Niveau.Niveau13:
                    case Niveau.Niveau14:
                    case Niveau.Niveau15:
                    case Niveau.Niveau16:
                    bonusDeBasePourLeNiveau = 5;
                        break;
                    case Niveau.Niveau17:
                    case Niveau.Niveau18:
                    case Niveau.Niveau19:
                    case Niveau.Niveau20:
                    bonusDeBasePourLeNiveau = 6;
                        break;
                    default:
                        break;
                }
            int bonusDeBasePourLaCaract = 0;

            if (personne.Force >0)
            {
                bonusDeBasePourLaCaract = QuelBonusPourLaCaracteristique(personne.Force);
            }
            int bonusDeMaitrise = 0;
            return bonusDeMaitrise = bonusDeBasePourLeNiveau+ bonusDeBasePourLaCaract;
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
                case 1: case 2: case 3: leBonus = -4;
                    break;
                case 4:                case 5:                    leBonus = -3;
                    break;
                case 6:                case 7:                    leBonus = -2;
                    break;
                case 8:                case 9:                    leBonus = -1;
                    break;
                case 10:                case 11:                    leBonus = 0;
                    break;
                case 12:                case 13:                    leBonus = 1;
                    break;
                case 14:                case 15:                    leBonus = 2;
                    break;
                case 16:                case 17:                    leBonus = 3;
                    break;
                case 18:                case 19:                    leBonus = 4;
                    break;
                default:                    leBonus = 0; //ca ne doit jamais arriver
                    break;
            }
            return leBonus;
        }

        /// <summary>
        /// renvoi la valeur de la caractéristique de la personne à tester
        /// </summary>
        /// <param name="laPersonneDontOnDoitTesterSaCaract"></param>
        /// <param name="caracteristique"></param>
        /// <returns></returns>
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

        /// <summary>
        /// renvoi la classe d'armure du personnage 
        /// </summary>
        /// <param name="laPersonne"></param>
        /// <returns></returns>
        public int CalculeQuelleClassedArmure(Personne laPersonne)
        {
            //Penser lors de l'implémentation des equipements à la refaire pour prendre en compte les armures
            int classeArmure = 10; //valeur de base de la classe d'armure cf régles du jeu 
            return classeArmure + QuelBonusPourLaCaracteristique(laPersonne.Dexterite);
        }

        /// <summary>
        /// renvoie le bonus de la maitrise magique du perso en fonction de sa classe
        /// clerc = sagesse ; magicien et le reste = intelligence
        /// </summary>
        /// <param name="personne"></param>
        /// <returns></returns>
        public int BonusDeBaseDeMaitriseMagiquePourLeNiveau(Personne personne)
        {
            int bonusDeBasePourLeNiveau = 0;
            switch (personne.NiveauDuPersonnage)
            {
                case Niveau.Niveau1:
                case Niveau.Niveau2:
                case Niveau.Niveau3:
                case Niveau.Niveau4:
                    bonusDeBasePourLeNiveau = 2;
                    break;
                case Niveau.Niveau5:
                case Niveau.Niveau6:
                case Niveau.Niveau7:
                case Niveau.Niveau8:
                    bonusDeBasePourLeNiveau = 3;
                    break;
                case Niveau.Niveau9:
                case Niveau.Niveau10:
                case Niveau.Niveau11:
                case Niveau.Niveau12:
                    bonusDeBasePourLeNiveau = 4;
                    break;
                case Niveau.Niveau13:
                case Niveau.Niveau14:
                case Niveau.Niveau15:
                case Niveau.Niveau16:
                    bonusDeBasePourLeNiveau = 5;
                    break;
                case Niveau.Niveau17:
                case Niveau.Niveau18:
                case Niveau.Niveau19:
                case Niveau.Niveau20:
                    bonusDeBasePourLeNiveau = 6;
                    break;
                default:
                    break;
            }
            int bonusDeBasePourLaCaract = 0;

            switch (personne.ClasseDuPersonnage)
            {
                case Classe.Clerc:
                    bonusDeBasePourLaCaract = QuelBonusPourLaCaracteristique(personne.Sagesse);
                    break;
                case Classe.Guerrier:
                    bonusDeBasePourLaCaract = QuelBonusPourLaCaracteristique(personne.Intelligence);
                    break;
                case Classe.Magicien:
                    bonusDeBasePourLaCaract = QuelBonusPourLaCaracteristique(personne.Intelligence);
                    break;
                case Classe.Roublard:
                    bonusDeBasePourLaCaract = QuelBonusPourLaCaracteristique(personne.Intelligence);
                    break;
                default:
                    break;
            }
            int bonusDeMaitrise = 0;
            return bonusDeMaitrise = bonusDeBasePourLeNiveau + bonusDeBasePourLaCaract;
        }

        /// <summary>
        /// renvoie le bonus aux dégats magique en fonction de la classe
        /// Clerc = sagesse , le reste = intelligence
        /// </summary>
        /// <param name="personne"></param>
        /// <returns></returns>
        public int BonusDeDegatsMagiqueParClasse(Personne personne)
        {
            int bonusDeBasePourLaCaract = 0;

            switch (personne.ClasseDuPersonnage)
            {
                case Classe.Clerc:
                    bonusDeBasePourLaCaract = QuelBonusPourLaCaracteristique(personne.Sagesse);
                    break;
                case Classe.Guerrier:
                    bonusDeBasePourLaCaract = QuelBonusPourLaCaracteristique(personne.Intelligence);
                    break;
                case Classe.Magicien:
                    bonusDeBasePourLaCaract = QuelBonusPourLaCaracteristique(personne.Intelligence);
                    break;
                case Classe.Roublard:
                    bonusDeBasePourLaCaract = QuelBonusPourLaCaracteristique(personne.Intelligence);
                    break;
                default:
                    break;
            }
         
            return bonusDeBasePourLaCaract ;
        }

        /// <summary>
        /// methode pour frapper une personne/monstre
        /// </summary>
        /// <param name="lAttaquant"></param>
        /// <param name="leDefenceur"></param>
        /// <returns></returns>
        public async Task<int> Frapper(Personne lAttaquant, Personne leDefenceur)
        {
            //pour le test les deux combattants ont une arme fistive à 1D6
            Des _des = new Des();
            int degatsArmeAttaquant = await _des.LanceLeDe(Des.TypeDeDes.D6);
             int pointDegatsFaitsParAttaquant = 0;
          
            //l'attaquant touche ou pas 
            int jetDattaque = lAttaquant.AttaqueMaitriseArme + await _des.LanceLeDe(Des.TypeDeDes.D20);

            if (await TestCaracteristique(jetDattaque, leDefenceur.ClasseArmure))
            {
                //Penser à gérer le fait des coups crtiques qui doivent mulpilier les degats !!
                pointDegatsFaitsParAttaquant = degatsArmeAttaquant + lAttaquant.BonusAuDegatMagique;
                if (pointDegatsFaitsParAttaquant > 0)
                {      //il fait des degats
                     leDefenceur.PointsDeVieActuels -= pointDegatsFaitsParAttaquant;
                }
            }
            else
            {
                leDefenceur.PointsDeVieActuels = leDefenceur.PointsDeVieActuels;
            }
             return leDefenceur.PointsDeVieActuels;  //renvoi les points de vies actuels du défenceur
         }

        /// <summary>
        /// les actions disponibles pour le combat 
        /// </summary>
        public enum ActionCombat :int
        {
            CombattrePhysique =1,
            CombattreMagique =2,
            Potion=3
        }
        /// <summary>
        /// nouvelle méthode des combat 
        /// </summary>
        /// <param name="leHeros"></param>
        /// <param name="leMonstre"></param>
        /// <returns></returns>
        public async Task<Dictionary<string, object>> PhaseDeCombat(int nombreRound, bool? joueurInit, ActionCombat? actionChoisie, Personne leHeros, Personne leMonstre)
        {
            // Création du dictionnaire.
            Dictionary<string, object> listeDeroulementDuCombat = new Dictionary<string, object>();
            string leMessage = "";
            //A quel round du combat on est 
            if (nombreRound == 0)
            {
                nombreRound = 1; // si c'est pas valorisé ca veut dire que c est le début du combat , donc on valorise à 1
                listeDeroulementDuCombat.Add("Round n°", nombreRound);
            }
            else
            {
                //nouveau round 
                nombreRound += 1;
                listeDeroulementDuCombat.Add("Round n°", nombreRound);
            }
            //qui a l initiative ?  
            if (joueurInit == null)
            {
                bool leHerosFrappeLePremierPourLePremierRound = await HerosAtIlLinitiative(leHeros, leMonstre);
                if (leHerosFrappeLePremierPourLePremierRound == true)
                {
                    joueurInit = true;
                    //on valorise la liste
                    listeDeroulementDuCombat.Add("initiativeHeros", joueurInit);
                }
                else
                {
                    joueurInit = false;
                    //on valorise la liste
                    listeDeroulementDuCombat.Add("initiativeHeros", joueurInit);
                }
            }

            //Déroulement d'un round 
            //-1 celui qui a l initiative choisit son action : pour le moment uniquement combattre
            if (joueurInit == true) //-1.1 si c est le joueur qui a gagné l init 
                                    //ca sera lui qui attaquera le premier
            { //:on propose le choix
              //-1.11 s il n y a pas de choix de fait
                if (actionChoisie == null)
                {

                    //-1.12 on propose le choix
                    //on valorise la liste avec les protagonistes au combat
                    listeDeroulementDuCombat.Add("leHeros", leHeros);
                    listeDeroulementDuCombat.Add("leMonstre", leMonstre);
                    leMessage = "Vous avez gagné l'initiative au combat, quelle action voulez-vous effectuer ?";
                    listeDeroulementDuCombat.Add("leMessage", leMessage);
                    return listeDeroulementDuCombat;
                }
                else  //-2 si un choix est fait
                {
                    if (actionChoisie == ActionCombat.CombattrePhysique)
                    {
                        //points de vie du monstre avant le combat 
                        int pointsVieAvantCombatMonstre = leMonstre.PointsDeVieActuels;
                        //-2.1 l'action est résolue
                        int pointsDegatsSubisParLeMonstre = await PasseDarme(leHeros, leMonstre);

                        if (pointsDegatsSubisParLeMonstre > 0)
                        {
                            leMessage = "Vous avez porté une attaque et infligé " + pointsDegatsSubisParLeMonstre +
                                " point(s) de dégat(s) à votre adversaire.";
                        }
                        else
                        {
                            leMessage = "Vous avez porté une attaque sans infliger de point de dégat à votre adversaire.";
                        }
                        //mise a jour des points de vie du monstre
                        leMonstre.PointsDeVieActuels -= pointsDegatsSubisParLeMonstre;
                        //met à jour du monstre dans le dictionnaire pour les autres rounds
                        listeDeroulementDuCombat.Remove("leMonstre");
                        listeDeroulementDuCombat.Add("leMonstre", leMonstre);
                        //met à jour du message dans le dictionnaire pour les autres rounds
                        listeDeroulementDuCombat.Remove("leMessage");
                        listeDeroulementDuCombat.Add("leMessage", leMessage);
                        //-2.2 si l'adversaire est encore en vie il peut frapper
                        if (EstVivant(leMonstre))
                        { //le monstre est vivant et c est a lui d attaquer le heros

                            //points de vie du heros avant le combat 
                            int pointsVieAvantCombatHeros = leHeros.PointsDeVieActuels;
                            //-2.1 l'action est résolue
                            int pointsDegatsSubisParLeHeros = await PasseDarme(leMonstre, leHeros);
                            if (pointsDegatsSubisParLeHeros > 0)
                            {
                                leMessage += " Votre adversaire vous a porté une attaque et infligé " + pointsDegatsSubisParLeHeros +
                                    " point(s) de dégat(s).";
                            }
                            else
                            {
                                leMessage += " Votre adversaire vous a porté une attaque sans vous infliger de point de dégat.";
                            }

                            //mise a jour des points de vie du heros
                            leHeros.PointsDeVieActuels -= pointsDegatsSubisParLeHeros;
                            //met à jour du heros dans le dictionnaire pour les autres rounds
                            listeDeroulementDuCombat.Remove("leHeros");
                            listeDeroulementDuCombat.Add("leHeros", leHeros);
                            //met à jour du message dans le dictionnaire pour les autres rounds
                            listeDeroulementDuCombat.Remove("leMessage");
                            listeDeroulementDuCombat.Add("leMessage", leMessage);
                        }
                        else
                        {//le monstre est mort

                            //PENSER A FAIRE GAGNER DES PTS EXP !!!


                            //mise à jour du heros 
                            listeDeroulementDuCombat.Remove("leHeros");
                            listeDeroulementDuCombat.Add("leHeros", leHeros);

                            leMessage = "vous avez tué votre adversaire";
                            listeDeroulementDuCombat.Remove("leMessage");
                            listeDeroulementDuCombat.Add("leMessage", leMessage);
                        }

                        return listeDeroulementDuCombat;
                    }
                }
            }
            else//le joueur n a pas l init
            {
                //si l action est null on renvoi
                if (actionChoisie == null)
                {

                    //-1.12 on propose le choix
                    //on valorise la liste avec les protagonistes au combat
                    listeDeroulementDuCombat.Add("leHeros", leHeros);
                    listeDeroulementDuCombat.Add("leMonstre", leMonstre);
                    leMessage = "Vous avez perdu l'initiative au combat, quelle action voulez-vous effectuer ?";
                    listeDeroulementDuCombat.Add("leMessage", leMessage);
                    return listeDeroulementDuCombat;
                }
                else//si une action est choisie
                {
                    //le monstre attaque car c est lui qui a l init 
                    //points de vie du heros avant le combat 
                    int pointsVieAvantCombatHeros = leHeros.PointsDeVieActuels;
                    //-2.1 l'action est résolue
                    int pointsDegatsSubisParLeHeros = await PasseDarme(leMonstre, leHeros);
                    if (pointsDegatsSubisParLeHeros > 0)
                    {
                        leMessage += " Votre adversaire vous a porté une attaque et infligé " + pointsDegatsSubisParLeHeros +
                            " point(s) de dégat(s).";
                    }
                    else
                    {
                        leMessage += " Votre adversaire vous a porté une attaque sans vous infliger de point de dégat.";
                    }

                    //mise a jour des points de vie du heros
                    leHeros.PointsDeVieActuels -= pointsDegatsSubisParLeHeros;
                    //met à jour du heros dans le dictionnaire pour les autres rounds
                    listeDeroulementDuCombat.Remove("leHeros");
                    listeDeroulementDuCombat.Add("leHeros", leHeros);
                    //met à jour du message dans le dictionnaire pour les autres rounds
                    listeDeroulementDuCombat.Remove("leMessage");
                    listeDeroulementDuCombat.Add("leMessage", leMessage);

                    //si le joueur est vivant ALORS il peut faire l action qu il avait choisir
                    if (EstVivant(leHeros))
                    {
                        //points de vie du monstre avant le combat 
                        int pointsVieAvantCombatMonstre = leMonstre.PointsDeVieActuels;
                        //-2.1 l'action est résolue
                        int pointsDegatsSubisParLeMonstre = await PasseDarme(leHeros, leMonstre);

                        if (pointsDegatsSubisParLeMonstre > 0)
                        {
                            leMessage += "Vous avez porté une attaque et infligé " + pointsDegatsSubisParLeMonstre +
                                " point(s) de dégat(s) à votre adversaire.";
                        }
                        else
                        {
                            leMessage += "Vous avez porté une attaque sans infliger de point de dégat à votre adversaire.";
                        }
                        //mise a jour des points de vie du monstre
                        leMonstre.PointsDeVieActuels -= pointsDegatsSubisParLeMonstre;
                        //met à jour du monstre dans le dictionnaire pour les autres rounds
                        listeDeroulementDuCombat.Remove("leMonstre");
                        listeDeroulementDuCombat.Add("leMonstre", leMonstre);
                        //met à jour du message dans le dictionnaire pour les autres rounds
                        listeDeroulementDuCombat.Remove("leMessage");
                        listeDeroulementDuCombat.Add("leMessage", leMessage);

                        return listeDeroulementDuCombat;
                    }
                    else //Sinon c est que le heros est mort et on renvoi
                    {

                        return listeDeroulementDuCombat;
                    }
                   
                }

            }
            //-5 fin du round , nouveau round, on valorise le contenu du déroulement du combat, plus de jet d init mais on garde le meme ordre que pour le 1er round

            return listeDeroulementDuCombat;
        }

        /// <summary>
        /// methode d'une passe d'arme entre 2 personnes
        /// </summary>
        /// <param name="lAttaquant"></param>
        /// <param name="leDefenceur"></param>
        /// <returns></returns>
        private async Task<int> PasseDarme(Personne lAttaquant, Personne leDefenceur)
        {
            //pour le test les deux combattants ont une arme fistive à 1D6
            Des _des = new Des();
            int degatsArmeAttaquant = await _des.LanceLeDe(Des.TypeDeDes.D6);
            int pointDegatsFaitsParAttaquant = 0;

            //l'attaquant touche ou pas 
            int jetDattaque = lAttaquant.AttaqueMaitriseArme + await _des.LanceLeDe(Des.TypeDeDes.D20);

            if (await TestCaracteristique(jetDattaque, leDefenceur.ClasseArmure)) //il a touché
            {
                //Penser à gérer le fait des coups crtiques qui doivent mulpilier les degats !!
                //pour le moment on ne gére que les dégats physique
                pointDegatsFaitsParAttaquant = degatsArmeAttaquant + lAttaquant.BonusAuDegatPhysique; 
                if (pointDegatsFaitsParAttaquant >= 0)
                {      //il fait des degats
                    return pointDegatsFaitsParAttaquant;
                }
                else
                {
                    return 0;
                }
            }
            else // l'attaque a echoué 
            {
                return 0;
            }
           
        }



        /// <summary>
        /// permet de savoir si le heros a l initiative sur le monstre ou pas ?
        /// init = 10+ 1D20+ la caract de la classe
        /// </summary>
        /// <param name="leHeros"></param>
        /// <param name="leMonstre"></param>
        /// <returns></returns>
        public async Task<bool> HerosAtIlLinitiative(Personne leHeros, Personne leMonstre)
        {
            Des leLancerDeDES = new Des();
            int initiativeHeros = 0;

            switch (leHeros.ClasseDuPersonnage)
            {
                case Classe.Clerc:
                    initiativeHeros = 10 + await leLancerDeDES.LanceLeDe(Des.TypeDeDes.D20) + QuelBonusPourLaCaracteristique(leHeros.Sagesse);
                    break;
                case Classe.Guerrier:
                    initiativeHeros = 10 + await leLancerDeDES.LanceLeDe(Des.TypeDeDes.D20) + QuelBonusPourLaCaracteristique(leHeros.Force);
                    break;
                case Classe.Magicien:
                    initiativeHeros = 10 + await leLancerDeDES.LanceLeDe(Des.TypeDeDes.D20) + QuelBonusPourLaCaracteristique(leHeros.Intelligence);
                    break;
                case Classe.Roublard:
                    initiativeHeros = 10 + await leLancerDeDES.LanceLeDe(Des.TypeDeDes.D20) + QuelBonusPourLaCaracteristique(leHeros.Dexterite);
                    break;
                default:
                    break;
            }

            int initiativeMonstre = 10+await leLancerDeDES.LanceLeDe(Des.TypeDeDes.D20)+QuelBonusPourLaCaracteristique(leMonstre.Dexterite);

            if (initiativeHeros >= initiativeMonstre)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// renvoi la valeur de la caracteristique pour reflexe
        /// </summary>
        /// <param name="leHeros"></param>
        /// <returns></returns>
        public int ValeurDeReflexe(Personne leHeros)
        {
            int valeurReflexe = 10 + QuelBonusPourLaCaracteristique(leHeros.Dexterite);
            return valeurReflexe;
        }
        /// <summary>
        /// renvoi la valeur de la caracteristique pour Vigueur
        /// </summary>
        /// <param name="leHeros"></param>
        /// <returns></returns>
        public int ValeurDeVigueur(Personne leHeros)
        {
            int valeurVigueur = 10 + QuelBonusPourLaCaracteristique(leHeros.Constitution);
            return valeurVigueur;
        }
        /// <summary>
        /// renvoi la valeur de la caracteristique pour Volonte
        /// </summary>
        /// <param name="leHeros"></param>
        /// <returns></returns>
        public int ValeurDeVolonte(Personne leHeros)
        {
            int valeurVolonte = 10 + QuelBonusPourLaCaracteristique(leHeros.Sagesse);
            return valeurVolonte;
        }

        //les caractéristiques présentes dans le jeu
        public enum Caracteristiques : int
        {
            // les caract du personnage
            [Description("Force")]
            Force,
            [Description("Dexterite")]
            Dexterite,
            [Description("Constitution")]
            Constitution,
            [Description("Intelligence")]
            Intelligence,
            [Description("Sagesse")]
            Sagesse,
            [Description("Charisme")]
            Charisme,
            //les points de vies
            [Description("Points de Vie Max")]
            PointsDeVieMax,
            [Description("Points de Vie actuels")]
            PointsDeVieActuels,
            //l 'expérience et lvl du personnage 
            [Description("Points d'expérience")]
            PointsExperience,

            //pour les combats
            [Description("Maitrise Attaque aux armes")]
            AttaqueMaitriseArme,
            [Description("Classe d'armure")]
            ClasseArmure,
            [Description("Maitrise Attaque Magique")]
            AttaqueMaitriseMagique,

            //défences
            [Description("Réflexe")]
            Reflexe,
            [Description("Vigueur")]
            Vigueur,
            [Description("Volonté")]
            Volonte
         }

        //les difficultés du jeu 
        public enum DifficultesDuJeu : int
        {
            [Description("Trés facile")]
            Simple = 5,
            [Description("Facile")]
            Facile = 10,
            [Description("Moyen")]
            Moyen = 15,
            [Description("Difficile")]
            Difficile =20,
            [Description("Trés difficile")]
            Hardue = 25,
            [Description("Impossible")]
            Impossible = 30,


        }
    }
}
