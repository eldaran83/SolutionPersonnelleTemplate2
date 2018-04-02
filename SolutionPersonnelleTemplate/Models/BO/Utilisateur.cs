using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SolutionPersonnelleTemplate.Models.BO
{
    public class Utilisateur
    {
        [Key]
        public string ApplicationUserID { get; set; }

        public string Pseudo { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Membre depuis")]
        public DateTime DateCreationUtilisateur { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date de naissance")]
        public DateTime DateDeNaissance { get; set; }
        //zone pour verif
        public bool ProfilUtilisateurComplet { get; set; }
        public string Role { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public bool ConfirmEmail { get; set; }
        //image
        public string UrlAvatarImage { get; set; }
        //nav
        public ApplicationUser ApplicationUser { get; set; }
        public ICollection<Partie> Parties { get; set; }
    }
}
