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
        //fk
        public int HistoireID { get; set; }

        //nav 
        public Histoire Histoire { get; set; }

    }
}
