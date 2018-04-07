using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SolutionPersonnelleTemplate.Models.BO
{
    public class Histoire
    {
        public int HistoireID { get; set; }

        public string Titre { get; set; }
        public int Score { get; set; }
        public int NombreDeFoisJouee { get; set; }

        [Display(Name = "Histoire créée par ")]
        public string Createur { get; set; }
        [Display(Name = "Image")]
        public string UrlMedia { get; set; }
        //fk
        public string UtilisateurID { get; set; }
        //nav
        public ICollection<Message> Messages { get; set; }
        public  Utilisateur Utilisateur { get; set; }

    }
}
