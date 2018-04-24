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

        public virtual string Presentation()
        {
            return "Je me présente, je suis une personne, je m'apelle " + Nom + "j'ai une force de " + Force + " une Intelligence de " + Intelligence +
                " une Dexterite de " + Dexterite + " une Constitution de " + Constitution + " et un Charisme de " + Charisme;
        }
    }
}
