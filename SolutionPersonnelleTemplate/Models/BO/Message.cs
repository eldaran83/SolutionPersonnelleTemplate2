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

        [Display(Name = "Titre du message")]
        public string Titre { get; set; }
        [DataType(DataType.MultilineText)]
        public string Contenu { get; set; }

        //fk
        public int HistoireID { get; set; }

        //nav 
        public Histoire Histoire { get; set; }
    }
}
