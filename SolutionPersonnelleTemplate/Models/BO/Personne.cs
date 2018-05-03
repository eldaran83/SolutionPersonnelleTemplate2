using SolutionPersonnelleTemplate.Data;
using SolutionPersonnelleTemplate.Models.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SolutionPersonnelleTemplate.Models.BO
{
    public class Personne
    {

        //PK
        public int PersonneID { get; set; }

        [Required]
        public string Nom { get; set; }
        [Required]
        [Display(Name = "Sexe")]
        public Sexe SexePersonnage { get; set; }
        //caract
        [Required]
        public int Force { get; set; }
     
        [Display(Name = "Bonus de Force")]
        public int BonusForce { get; set; }

        [Required]
        public int Dexterite { get; set; }
  
        [Display(Name = "Bonus de Dexterité")]
        public int BonusDexterite { get; set; }

        [Required]
        public int Constitution { get; set; }
     
        [Display(Name = "Bonus de Constitution")]
        public int BonusConstitution { get; set; }

        [Required]
        public int Intelligence { get; set; }
     
        [Display(Name = "Bonus d'Intelligence")]
        public int BonusIntelligence { get; set; }


        [Required]
        public int Sagesse { get; set; }
      
        [Display(Name = "Bonus de Sagesse")]
        public int BonusSagesse { get; set; }

        [Required]
        public int Charisme { get; set; }
    
        [Display(Name = "Bonus de Charisme")]
        public int BonusCharisme { get; set; }


        //Classe du personnage 
        [Display(Name = "Classe")]
        public  Classe ClasseDuPersonnage { get; set; }

        //Point de vie
        [Display(Name = "Points de vie max")]
        public int PointsDeVieMax { get; set; } 
        [Display(Name = "Points de vie actuels")]
        public int PointsDeVieActuels { get; set; }

        //pour le niveau du perso et xp 
        [Display(Name = "Points d'expérience")]
        public int PointsExperience { get; set; }
        [Display(Name = "Niveau")]
        public Niveau NiveauDuPersonnage { get; set; }
        
        //pour les combats
        //phyisque
        [Display(Name = "Attaque physique")]
        public int AttaqueMaitriseArme { get; set; }

        [Display(Name = "Classe Armure")]
        public int ClasseArmure { get; set; }

        [Display(Name = "Bonus dégâts physique")]
         public int BonusAuDegatPhysique { get; set; }
        //magique
        [Display(Name = "Attaque magique")]
        public int AttaqueMaitriseMagique { get; set; }

        [Display(Name = "Bonus dégâts magique")]
        public int BonusAuDegatMagique { get; set; }

        //Défences
         [Display(Name = "Réflexe")]
        public int Reflexe { get; set; }
        public int Vigueur { get; set; }
        [Display(Name = "Volonté")]
        public int Volonte { get; set; }


        //public enum Caracteristiques
        //{
        //    // les caract du personnage
        //    Force,
        //    Dexterite,
        //    Constitution,
        //    Intelligence,
        //    Sagesse,
        //    Charisme,
        //    //les points de vies
        //    PointsDeVieMax,
        //    PointsDeVieActuels,
        //    //l 'expérience et lvl du personnage 
        //    PointsExperience, 

        //    //pour les combats
        //    AttaqueMaitriseArme,
        //    ClasseArmure,

        //    AttaqueMaitriseMagique,

        //    //défences
        //    Reflexe,
        //    Vigueur,
        //    Volonte

        //}

        

       
        /// <summary>
        /// les classes disponibles dans le jeux
        /// </summary>
        public enum Classe
        {
            Clerc,
            Guerrier,
            Magicien,
            Roublard
        }
        
        

        public enum Sexe
        {
            Homme,
            Femme
        }

        public enum Niveau
        {
            Niveau1,Niveau2,Niveau3,Niveau4,Niveau5,Niveau6,Niveau7,Niveau8,Niveau9,Niveau10,Niveau11,Niveau12,Niveau13, Niveau14, Niveau15, Niveau16, Niveau17,Niveau18, Niveau19, Niveau20
        }

        public int BonusDeBasePourLeNiveauDeLaClasse(Classe laClasseDuPerso, Niveau leNiveauDuPersonnage)
        {
            // int leBonus = _context.BonusDesClassesJoueurs.Where(c => c.Classe == laClasseDuPerso).Where(n => n.Niveau == leNiveauDuPersonnage).FirstOrDefault();
            return 0;// a changer apres correction
        }
    }
}
