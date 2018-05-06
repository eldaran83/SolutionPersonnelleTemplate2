using SolutionPersonnelleTemplate.Data;
using SolutionPersonnelleTemplate.Models.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        /// <summary>
        /// les classes disponibles dans le jeu pour un personnage
        /// </summary>
        public enum Classe : int
        {
            [Description("Clerc")]
            Clerc =1,
            [Description("Guerrier")]
            Guerrier=2,
            [Description("Magicien")]
            Magicien=3,
            [Description("Roublard")]
            Roublard=4
        }
        /// <summary>
        /// les sexes disponibles dans le jeu pour le personnage
        /// </summary>
        public enum Sexe : int
        {
            [Description("Homme")]
            Homme = 1,
            [Description("Femme")]
            Femme = 2
        }
        /// <summary>
        /// niveaux disponibles dans le jeu pour le personnage
        /// </summary>
        public enum Niveau : int
        {
            [Description("Niveau 1")]
            Niveau1 = 1,
            [Description("Niveau 2")]
            Niveau2 = 2,
            [Description("Niveau 3")]
            Niveau3 = 3,
            [Description("Niveau 4")]
            Niveau4 =4,
            [Description("Niveau 5")]
            Niveau5 =5,
            [Description("Niveau 6")]
            Niveau6 =6,
            [Description("Niveau 7")]
            Niveau7 =7,
            [Description("Niveau 8")]
            Niveau8 =8,
            [Description("Niveau 9")]
            Niveau9 = 9,
            [Description("Niveau 10")]
            Niveau10 =10,
            [Description("Niveau 11")]
            Niveau11 =11,
            [Description("Niveau 12")]
            Niveau12 =12,
            [Description("Niveau 13")]
            Niveau13=13,
            [Description("Niveau 14")]
            Niveau14=14,
            [Description("Niveau 15")]
            Niveau15=15,
            [Description("Niveau 16")]
            Niveau16=16,
            [Description("Niveau 17")]
            Niveau17=17,
            [Description("Niveau 18")]
            Niveau18=18,
            [Description("Niveau 19")]
            Niveau19=19,
            [Description("Niveau 20")]
            Niveau20=20
        }
    }
}
