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

        [Required(ErrorMessage = "Message créer le")]
        [DataType(DataType.DateTime)]
        public DateTime DateCreationMessage { get; set; }


        public bool EstLePremierMessageDeLHistoire { get; set; }

        //liens vers les 3 actions messages possibles
        //1
        [Display(Name = "N° du message 1")]
        public int? NumeroMessageEnfant1 { get; set; }
        [Display(Name = "Nom de l'action 1")]
        public string NomAction1 { get; set; }

        //2
        [Display(Name = "N° du message 2")]
        public int? NumeroMessageEnfant2 { get; set; }
        [Display(Name = "Nom de l'action 2")]
        public string NomAction2 { get; set; }

        //3
        [Display(Name = "N° du message 3")]
        public int? NumeroMessageEnfant3 { get; set; }
        [Display(Name = "Nom de l'action 3")]
        public string NomAction3 { get; set; }

        //fk
        public int HistoireID { get; set; }

        //nav 
        public Histoire Histoire { get; set; }

    }
}
