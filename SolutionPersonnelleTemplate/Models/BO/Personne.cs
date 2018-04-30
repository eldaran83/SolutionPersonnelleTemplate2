using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SolutionPersonnelleTemplate.Models.BO
{
    public class Personne
    {        //PK
        public int PersonneID { get; set; }

        [Required]
        public string Nom { get; set; }

        //caract
        [Required]
        public int Force { get; set; }
        [Required]
        public int Intelligence { get; set; }
        [Required]
        public int Dexterite { get; set; }
        [Required]
        public int Constitution { get; set; }
        [Required]
        public int Charisme { get; set; }

        //
        public int PointsDeVieMax { get; set; }
        public int PointsDeVieActuels { get; set; }

        public virtual string Presentation()
        {
            return "Je me présente, je suis une personne, je m'apelle " + Nom + "j'ai une force de " + Force + " une Intelligence de " + Intelligence +
                " une Dexterite de " + Dexterite + " une Constitution de " + Constitution + " et un Charisme de " + Charisme;
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
            Force,
            Intelligence,
            Dexterite,
            Constitution,
            Charisme
        }

        public async Task<int> ValeurDeLaCaracteristiqueATester(Caracteristiques caracteristique)
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
                case Caracteristiques.Charisme:  valeurDeLaCaract = this.Charisme;
                    break;
                default:
                    break;
            }
            return valeurDeLaCaract;
        }
    }
}
