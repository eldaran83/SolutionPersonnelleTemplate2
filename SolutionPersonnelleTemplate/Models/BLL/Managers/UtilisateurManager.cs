using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SolutionPersonnelleTemplate.Data;
using SolutionPersonnelleTemplate.Models.BLL.Interfaces;
using SolutionPersonnelleTemplate.Models.BO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SolutionPersonnelleTemplate.Models.BLL.Managers
{
    public class UtilisateurManager : IUtilisateurInterface
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        /// <summary>
        /// contructeur 
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="context"></param>
        public UtilisateurManager(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        /// <summary>
        /// peuple la bdd avec 3 utilisateurs
        /// 1 de chaque type
        /// </summary>
        /// <returns></returns>
        public async Task<bool> PeuplerLaBddAvec3typesUtilisateur()
        {
            //creation de l utilisateur eldaran
            ApplicationUser eldaran = await _userManager.FindByEmailAsync("Eldaran83@gmail.com");
            var eldaranID = eldaran.Id;

            Utilisateur eldaranUtilisateur = new Utilisateur
            {
                ApplicationUserID = eldaranID,
                ConfirmEmail = true,
                DateCreationUtilisateur = DateTime.Now,
                DateDeNaissance = DateTime.Parse("21/09/1976"),
                Email = eldaran.Email,
                Pseudo = "Eldaran83",
                Role = "Administrateur",
                ProfilUtilisateurComplet = true,
                UrlAvatarImage = "/images/userDefault.png"
            };
            _context.Add(eldaranUtilisateur);
            await _context.SaveChangesAsync();

            //creation de l utilisateur pilou
            ApplicationUser pilou = await _userManager.FindByEmailAsync("Piloupilouvar@hotmail.fr");
            var pilouID = pilou.Id;

            Utilisateur pilouUtilisateur = new Utilisateur
            {
                ApplicationUserID = pilouID,
                ConfirmEmail = true,
                DateCreationUtilisateur = DateTime.Now,
                DateDeNaissance = DateTime.Parse("21/09/1976"),
                Email = pilou.Email,
                Pseudo = "Pilou",
                Role = "Manager",
                ProfilUtilisateurComplet = true,
                UrlAvatarImage = "/images/userDefault.png"
            };
            _context.Add(pilouUtilisateur);
            await _context.SaveChangesAsync();


            //creation de l utilisateur test
            ApplicationUser membreTest = await _userManager.FindByEmailAsync("TestMembre@gmail.com");
            var membreTestID = membreTest.Id;

            Utilisateur testUtilisateur = new Utilisateur
            {
                ApplicationUserID = membreTestID,
                ConfirmEmail = true,
                DateCreationUtilisateur = DateTime.Now,
                DateDeNaissance = DateTime.Parse("21/09/1976"),
                Email = membreTest.Email,
                Pseudo = "MembreTest",
                Role = "Membre",
                ProfilUtilisateurComplet = true,
                UrlAvatarImage = "/images/userDefault.png"
            };
            _context.Add(testUtilisateur);
            await _context.SaveChangesAsync();

            return true;
        }

        /// <summary>
        /// Renvoi la liste de tous les utilisateurs
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Utilisateur>> GetAllUtilisateursAsync()
        {
            IEnumerable<Utilisateur> utilisateurs = await _context.Utilisateurs
               .Include(u => u.ApplicationUser)
               .ToListAsync();

            return utilisateurs;
        }

        /// <summary>
        /// renvoi l utilisateur grace a son id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Utilisateur> GetUtilisateurByIdAsync(string id)
        {
            Utilisateur user = await _context.Utilisateurs
                 .Include(m => m.ApplicationUser)
                 .FirstOrDefaultAsync(u => u.ApplicationUserID == id);
            return user;

        }

        /// <summary>
        /// ajoute un utilisateur en BDD
        /// </summary>
        /// <param name="utilisateur"></param>
        /// <returns></returns>
        public async Task<Utilisateur> AddUtilisateur(ApplicationUser applicationUser, Utilisateur utilisateur)
        {
            _context.Add(utilisateur); // ajoute l'utilisateur
            await _context.SaveChangesAsync();
            var roleUtilisateur = await _userManager.AddToRoleAsync(applicationUser, "Membre"); // ajoute son role
            await _context.SaveChangesAsync();

            return utilisateur;
        }

        /// <summary>
        /// ajoute un utilisateur Admin 
        /// pour le moment c'est uniquement 1 seul admin en bdd 
        /// a revoir apres 
        /// </summary>
        /// <param name="applicationUser"></param>
        /// <param name="utilisateurAdmin"></param>
        /// <returns></returns>
        public async Task<Utilisateur> AddUtilisateurAdmin(Utilisateur utilisateurAdmin)
        {
            _context.Add(utilisateurAdmin); // ajoute l'utilisateur
            await _context.SaveChangesAsync();
            // on ajoute pas de role car pour le moment l'admin eldaran83 a deja un role lors de l instanciation dans le starup
            await _context.SaveChangesAsync();

            ApplicationUser user = await _userManager.FindByIdAsync(utilisateurAdmin.ApplicationUserID);
            user.Email = utilisateurAdmin.Email;
            user.NormalizedEmail = utilisateurAdmin.Email.ToUpper();
            user.UserName = utilisateurAdmin.Email;
            user.NormalizedUserName = utilisateurAdmin.Email.ToUpper();
            _context.Update(user);
            await _context.SaveChangesAsync();
            return utilisateurAdmin;
        }

        /// <summary>
        /// met à jour un utilisateur en BDD
        /// </summary>
        /// <param name="utilisateur"></param>
        /// <returns></returns>
        public async Task<Utilisateur> UpdateUtilisateur(Utilisateur utilisateur)
        {
            try
            {
                _context.Update(utilisateur);
                await _context.SaveChangesAsync();

                ApplicationUser user = await _userManager.FindByIdAsync(utilisateur.ApplicationUserID);
                user.Email = utilisateur.Email;
                user.NormalizedEmail = utilisateur.Email.ToUpper();
                user.UserName = utilisateur.Email;
                user.NormalizedUserName = utilisateur.Email.ToUpper();
                _context.Update(user);
                await _context.SaveChangesAsync();

                return utilisateur;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                Console.WriteLine("La MAJ de l'utilisateur a rencontré un problème :" + ex);
                return utilisateur;
            }
        }

        /// <summary>
        /// met à jour l utilisateur depuis la zone admin
        /// ce qui permet de tout modifier si on le veut 
        /// notammment le role de l utilisateur
        /// </summary>
        /// <param name="updateUtilisateurAdmin"></param>
        /// <returns></returns>
        public async Task<Utilisateur> UpdateUtilisateurAdmin(Utilisateur updateUtilisateurAdmin)
        {
            try
            {
                //je dois récupérer l id de l utilisateur a update , qui n'est pas le meme que celui connecté puisque je suis en mode admin 
                ApplicationUser applicationUser = await _userManager.FindByIdAsync(updateUtilisateurAdmin.ApplicationUserID);

                //trouve l ancien role de l user
                var ancienRole = await _context.UserRoles.Where(u => u.UserId == applicationUser.Id).FirstOrDefaultAsync();
                var ancienRoleId = ancienRole.RoleId;
                var ancienRoleName = await _context.Roles.Where(r => r.Id == ancienRoleId).FirstOrDefaultAsync();
                var ancienRoleName2 = ancienRoleName.Name;

                //je dois trouver l id du role dans la table Role du nom du nouveau role pour l'utilisateur à update//
                var nouveauRole = await _context.Roles.Where(r => r.Id == updateUtilisateurAdmin.Role).FirstOrDefaultAsync();
                var roleIdAModifier = nouveauRole.Id;
                var roleNameAModifier = nouveauRole.Name;

                var roleDeLutilisateurAModifier = await _context.UserRoles.Where(u => u.UserId == applicationUser.Id).FirstOrDefaultAsync();

                //supprimer le UserRole dans la role UserRole
                await _userManager.RemoveFromRoleAsync(applicationUser, ancienRoleName2); // efface son role présent
                //je dois changer dans la table userRole son role pour mettre celui qui est passé en param
                var roleUtilisateur = await _userManager.AddToRoleAsync(applicationUser, roleNameAModifier); // ajoute son role
                await _context.SaveChangesAsync();

                //trouve le nouveau role de l user
                var nouveauRoleUser = await _context.UserRoles.Where(u => u.UserId == applicationUser.Id).FirstOrDefaultAsync();
                var nouveauRoleId = nouveauRoleUser.RoleId;
                var nouveauRoleName = await _context.Roles.Where(r => r.Id == nouveauRoleId).FirstOrDefaultAsync();
                var nouveauRoleNameAPasserDansUtilisateur = nouveauRoleName.Name;

                //mise a jour du role dans utilisateur 
                updateUtilisateurAdmin.Role = nouveauRoleNameAPasserDansUtilisateur;
                //je met a jour tout le reste de l utilisateur 
                _context.Update(updateUtilisateurAdmin);
                await _context.SaveChangesAsync();

                //je met a jour dans applicationUser 
                applicationUser.Email = updateUtilisateurAdmin.Email;
                applicationUser.NormalizedEmail = updateUtilisateurAdmin.Email.ToUpper();
                applicationUser.UserName = updateUtilisateurAdmin.Email;
                applicationUser.NormalizedUserName = updateUtilisateurAdmin.Email.ToUpper();
                applicationUser.EmailConfirmed = updateUtilisateurAdmin.ConfirmEmail;

                _context.Update(applicationUser);
                await _context.SaveChangesAsync();

                return updateUtilisateurAdmin;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                Console.WriteLine("La MAJ de l'utilisateur a rencontré un problème :" + ex);
                return updateUtilisateurAdmin;
            }
        }

        public bool RemoveDossierImageUtilisateur(string userId)
        {
            try
            {
                //Supprime le dossier qui contient tous les fichiers de l'utilisateur  
                var dirPath = Path.Combine(
                               Directory.GetCurrentDirectory(),
                               "wwwroot" + "/UserFiles/" + Convert.ToString(userId) + "/");

                Directory.Delete(dirPath, true);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("L'utilisateur n'a pas de dossier image " + ex);
                return false;
            }
        }
        /// <summary>
        /// permet de supprimer un utilisateur et tout ce qui s en rapporte :
        /// fichiers,utilisateur, role, applicationUser
        /// a supprimer dans cet ordre car sinon ca pete 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<bool> RemoveUtilisateur(string userId)
        {
            //Supprime le dossier qui contient tous les fichiers de l'utilisateur  
            RemoveDossierImageUtilisateur(userId);
            //Le ApplicationUser est à supprimer en DERNIER !!
            try
            {   
                    //Supprime l'utilisateur
                    var utilisateur = await _context.Utilisateurs.SingleOrDefaultAsync(m => m.ApplicationUserID == userId);
                    _context.Utilisateurs.Remove(utilisateur);
                    await _context.SaveChangesAsync();

                    //Supprime le role du ApplicationUser
                    var roleApplicationUser = await _context.UserRoles.SingleOrDefaultAsync(m => m.UserId == userId);
                    _context.UserRoles.Remove(roleApplicationUser);
                    await _context.SaveChangesAsync();

                    //supprime le ApplicationUser 
                    var applicationUser = await _context.Users.SingleOrDefaultAsync(m => m.Id == userId);
                    _context.Users.Remove(applicationUser);
                    await _context.SaveChangesAsync();

                    return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("La supression de l'utilisateur a rencontré un problème :" + ex);
                return false;
            }
        }

        /// <summary>
        /// vérifie en bdd si le speudo est libre
        /// </summary>
        /// <param name="pseudo"></param>
        /// <returns></returns>
        public async Task<bool> PseudoExist(string pseudo)
        {
            try
            {
                Utilisateur pseudoUtilisateur = await _context.Utilisateurs.Where(u => u.Pseudo.ToUpper() == pseudo.ToUpper()).FirstOrDefaultAsync();
                if (pseudoUtilisateur != null)
                {
                    return true; //le pseudo existe en bdd
                }
                else
                {
                    return false; //le pseudo n 'existe pas en bdd
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("L'accès a la requête s'est mal passée " + ex);
                return false;
            }
        }

        /// <summary>
        /// vérifie que le mail n est pas deja pris par un utilisateur 
        /// et donc qu il est unique
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task<bool> EmailExist(string email)
        {
            try
            {
                var emailUtilisateur = await _context.Utilisateurs.Where(u => u.Email.ToUpper() == email.ToUpper()).FirstOrDefaultAsync();
                if (emailUtilisateur != null)
                {
                    return true; //le pseudo existe en bdd
                }
                else
                {
                    return false; //le pseudo n 'existe pas en bdd
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("L'accès a la requête s'est mal passée " + ex);
                return false;
            }
        }
    }

}
