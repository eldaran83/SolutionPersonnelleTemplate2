using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SolutionPersonnelleTemplate.Models.BO
{
    public class Message
    {
        public int MessageID { get; set; }
        [Required(ErrorMessage = "Vous devez renseigner un titre de moins de 80 caractères")]
        [MaxLength(80)]
        [Display(Name = "Titre du message")]
        public string Titre { get; set; }
        [Required(ErrorMessage = "Vous devez renseigner un contenu")]
        [DataType(DataType.MultilineText)]
        public string Contenu { get; set; }
        public string UrlMedia { get; internal set; }

        //liens vers les 3 actions messages possibles
        [Display(Name = "Libellé du message")]
        public string MessageEnfant1 { get; set; }
        [Display(Name = "N° du message")]
        public int? NumeroMessageEnfant1 { get; set; }

        [Display(Name = "Libellé du message")]
        public string MessageEnfant2 { get; set; }
        [Display(Name = "N° du message")]
        public int? NumeroMessageEnfant2 { get; set; }

        [Display(Name = "Libellé du message")]
        public string MessageEnfant3 { get; set; }
        [Display(Name = "N° du message")]
        public int? NumeroMessageEnfant3 { get; set; }

        //fk
        public int HistoireID { get; set; }

        //nav 
        public Histoire Histoire { get; set; }

    }
}
