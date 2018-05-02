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
        //private readonly IPersonneInterface _personneManager;
        //public Personne( IPersonneInterface personneManager)
        //{
        //    _personneManager = personneManager;
        //}

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
        private int _BonusForce;
        [Display(Name = "Bonus de Force")]
        public int BonusForce
        {
            get { return _BonusForce; }

            set { _BonusForce = QuelBonusPourLaCaracteristique(ValeurDeLaCaracteristiqueATester(Caracteristiques.Force)); }
        }

        [Required]
        public int Dexterite { get; set; }
        private int _BonusDexterite;
        [Display(Name = "Bonus de Dexterité")]
        public int BonusDexterite
        {
            get { return _BonusDexterite; }

            set { _BonusDexterite = QuelBonusPourLaCaracteristique(ValeurDeLaCaracteristiqueATester(Caracteristiques.Dexterite)); }
        }

        [Required]
        public int Constitution { get; set; }
        private int _BonusConstitution;
        [Display(Name = "Bonus de Constitution")]
        public int BonusConstitution
        {
            get { return _BonusConstitution; }

            set { _BonusConstitution = QuelBonusPourLaCaracteristique(ValeurDeLaCaracteristiqueATester(Caracteristiques.Constitution)); }
        }

        [Required]
        public int Intelligence { get; set; }
        private int _BonusIntelligence;
        [Display(Name = "Bonus d'Intelligence")]
        public int BonusIntelligence
        {
            get { return _BonusIntelligence; }

            set { _BonusIntelligence = QuelBonusPourLaCaracteristique(ValeurDeLaCaracteristiqueATester(Caracteristiques.Intelligence)); }
        }


        [Required]
        public int Sagesse { get; set; }
        private int _BonusSagesse;
        [Display(Name = "Bonus de Sagesse")]
        public int BonusSagesse
        {
            get { return _BonusSagesse; }

            set { _BonusSagesse = QuelBonusPourLaCaracteristique(ValeurDeLaCaracteristiqueATester(Caracteristiques.Sagesse)); }
        }

        [Required]
        public int Charisme { get; set; }
        private int _BonusCharisme;
        [Display(Name = "Bonus de Charisme")]
        public int BonusCharisme
        {
            get { return _BonusCharisme; }

            set { _BonusCharisme = QuelBonusPourLaCaracteristique(ValeurDeLaCaracteristiqueATester(Caracteristiques.Charisme)); }
        }
        //Classe du personnage 
        [Display(Name = "Classe")]
        public  Classe ClasseDuPersonnage { get; set; }

        //Point de vie
        [Display(Name = "Points de vie max")]
        public int PointsDeVieMax { get; set; } //on pars pour le moment sur Constitution X 3
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
        private int _BonusAuDegatPhysique;
        public int BonusAuDegatPhysique
        {
            get { return _BonusAuDegatPhysique; }

            set { _BonusAuDegatPhysique = QuelBonusPourLaCaracteristique( ValeurDeLaCaracteristiqueATester(Caracteristiques.Force)); }
        }
        //magique
        [Display(Name = "Attaque magique")]
        public int AttaqueMaitriseMagique { get; set; }

        [Display(Name = "Bonus dégâts magique")]
         private int _BonusAuDegatMagique;
        public int BonusAuDegatMagique
        {
            get { return _BonusAuDegatMagique; }

            set { _BonusAuDegatMagique = QuelBonusPourLaCaracteristique(ValeurDeLaCaracteristiqueATester(Caracteristiques.Intelligence)); }
        }

        //Défences
        private int _Reflexe;
        [Display(Name = "Réflexe")]
        public int Reflexe
        {
            get { return _Reflexe; }

            set { _Reflexe = QuelBonusPourLaCaracteristique(ValeurDeLaCaracteristiqueATester(Caracteristiques.Dexterite)) ; }
        }

        private int _Vigueur;
        public int Vigueur
        {
            get { return _Vigueur; }

            set { _Vigueur = QuelBonusPourLaCaracteristique(ValeurDeLaCaracteristiqueATester(Caracteristiques.Constitution)); }
        }

        private int _Volonte;
        [Display(Name = "Volonté")]
        public int Volonte
        {
            get { return _Volonte; }

            set { _Volonte = QuelBonusPourLaCaracteristique(ValeurDeLaCaracteristiqueATester(Caracteristiques.Sagesse)); }
        }


        /// <summary>
        ///  permet de savoir si un personnage est vivant ou pas 
        /// </summary>
        public bool EstVivant
        {
            get { return PointsDeVieActuels > 0; }
        }

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

        public int ValeurDeLaCaracteristiqueATester(Caracteristiques caracteristique)
        {
            int valeurDeLaCaract = 0;

            switch (caracteristique)
            {
                case Caracteristiques.Force:  valeurDeLaCaract =this.Force;
                    break;
                case Caracteristiques.Intelligence:  valeurDeLaCaract = this.Intelligence;
                    break;
                case Caracteristiques.Dexterite:  valeurDeLaCaract = this.Dexterite;
                    break;
                case Caracteristiques.Constitution:  valeurDeLaCaract = this.Constitution;
                    break;
                case Caracteristiques.Sagesse:
                    valeurDeLaCaract = this.Sagesse;
                    break;
                case Caracteristiques.Charisme:  valeurDeLaCaract = this.Sagesse;
                    break;
                //les points de vies
                case Caracteristiques.PointsDeVieMax: valeurDeLaCaract = this.PointsDeVieMax;
                    break;
                case Caracteristiques.PointsDeVieActuels: valeurDeLaCaract = this.PointsDeVieActuels;
                    break;
                //l 'expérience et lvl du personnage 
                case Caracteristiques.PointsExperience:  valeurDeLaCaract = this.PointsExperience;
                    break;

                //pour les combats
                case Caracteristiques.AttaqueMaitriseArme: valeurDeLaCaract = this.AttaqueMaitriseArme;
                    break; 
               case Caracteristiques.ClasseArmure: valeurDeLaCaract = this.ClasseArmure;
                    break; 

               case Caracteristiques.AttaqueMaitriseMagique:  valeurDeLaCaract = this.AttaqueMaitriseMagique;
                    break;
                //pour les défences
                case Caracteristiques.Reflexe:
                    valeurDeLaCaract = this.Reflexe;
                    break;
                case Caracteristiques.Vigueur:
                    valeurDeLaCaract = this.Vigueur;
                    break;
                case Caracteristiques.Volonte:
                    valeurDeLaCaract = this.Volonte;
                    break;

                default:
                    break;
            }
            return valeurDeLaCaract;
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
                case 1: case 2: case 3:
                    leBonus= - 4;
                    break;
                case 4: case 5:
                    leBonus = - 3;
                    break;
                case 6:case 7:
                    leBonus = -2;
                    break;
                case 8:case 9:
                    leBonus = -1;
                    break;
                case 10:case 11:
                    leBonus = 0;
                    break;
                case 12:case 13:
                    leBonus = 1;
                    break;
                case 14:case 15:
                    leBonus = 2;
                    break;
                case 16:case 17:
                    leBonus = 3;
                    break;
                case 18:case 19:
                    leBonus = 4;
                    break;
                default:
                    leBonus = 0; //ca ne doit jamais arriver
                    break;
            }
            return leBonus;
        }
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
        /// <summary>
        /// valorise les points de vie du perso pour le 1er niveau 
        /// </summary>
        /// <param name="classeDuPerso"></param>
        /// <returns></returns>
        public int PointDeViePremierNiveau(Classe classeDuPerso)
        {
            int pointDeViePremierNiveau = 0;
            switch (classeDuPerso)
            {
                case Classe.Clerc:
                    pointDeViePremierNiveau = 8 + QuelBonusPourLaCaracteristique(ValeurDeLaCaracteristiqueATester(Caracteristiques.Constitution));
                    break;
                case Classe.Guerrier:
                    pointDeViePremierNiveau = 10 + QuelBonusPourLaCaracteristique(ValeurDeLaCaracteristiqueATester(Caracteristiques.Constitution));
                    break;
                case Classe.Magicien:
                    pointDeViePremierNiveau = 6 + QuelBonusPourLaCaracteristique(ValeurDeLaCaracteristiqueATester(Caracteristiques.Constitution));
                     break;
                case Classe.Roublard:
                    pointDeViePremierNiveau = 8 + QuelBonusPourLaCaracteristique(ValeurDeLaCaracteristiqueATester(Caracteristiques.Constitution));
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
        public async Task<int> GagnerPointDeViePassageDeNiveau(Classe classeDuPerso)
        {
            Des leLancerDeDES = new Des();

            int pointDeViePremierNiveau = 0;
            switch (classeDuPerso)
            {
                case Classe.Clerc:
                    pointDeViePremierNiveau = await leLancerDeDES.LanceLeDe(Des.TypeDeDes.D8) + QuelBonusPourLaCaracteristique(ValeurDeLaCaracteristiqueATester(Caracteristiques.Constitution));
                    break;
                case Classe.Guerrier:
                    pointDeViePremierNiveau = await leLancerDeDES.LanceLeDe(Des.TypeDeDes.D10) + QuelBonusPourLaCaracteristique(ValeurDeLaCaracteristiqueATester(Caracteristiques.Constitution));
                    break;
                case Classe.Magicien:
                    pointDeViePremierNiveau = await leLancerDeDES.LanceLeDe(Des.TypeDeDes.D6) + QuelBonusPourLaCaracteristique(ValeurDeLaCaracteristiqueATester(Caracteristiques.Constitution));
                    break;
                case Classe.Roublard:
                    pointDeViePremierNiveau =await leLancerDeDES.LanceLeDe(Des.TypeDeDes.D8) + QuelBonusPourLaCaracteristique(ValeurDeLaCaracteristiqueATester(Caracteristiques.Constitution));
                    break;
                default:
                    break;
            }
            return pointDeViePremierNiveau;
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
