using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SolutionPersonnelleTemplate.Models;
using SolutionPersonnelleTemplate.Models.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SolutionPersonnelleTemplate.Data
{
    public class Seed
    {
        /// <summary>
        /// créer les roles de l application 
        /// "Administrateur", "Manager", "Membre"
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="Configuration"></param>
        /// <returns></returns>
        public static async Task CreateRolesAndApplicationUser(IServiceProvider serviceProvider, IConfiguration Configuration)
        {
            //adding customs roles
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            string[] roleNames = { "Administrateur", "Manager", "Membre" };

            IdentityResult roleResult;

            foreach (var roleName in roleNames)
            {
                // creating the roles and seeding them to the database
                var roleExist = await RoleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    roleResult = await RoleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            //creating a super user who could maintain the web app
            var userAdmin = new ApplicationUser
            {
                UserName = "Eldaran83@gmail.com",
                Email = "Eldaran83@gmail.com",
                EmailConfirmed = true
            };
            string UserPassword = "Sirius@83";
            var _user = await UserManager.FindByEmailAsync("Eldaran83@gmail.com");

            if (_user == null)
            {
                var createAdminUser = await UserManager.CreateAsync(userAdmin, UserPassword);
                if (createAdminUser.Succeeded)
                {
                    //here we tie the new user to the "Admin" role 
                    await UserManager.AddToRoleAsync(userAdmin, "Administrateur");
                }
            }
            // creating a better user who could maintain the web app
            var userManager = new ApplicationUser
            {
                UserName = "Piloupilouvar@hotmail.fr",
                Email = "Piloupilouvar@hotmail.fr",
                EmailConfirmed = true
            };
            string UserManagerPassword = "Sirius@83";
            var _userManager = await UserManager.FindByEmailAsync("Piloupilouvar@hotmail.fr");

            if (_userManager == null)
            {
                var createManagerUser = await UserManager.CreateAsync(userManager, UserManagerPassword);
                if (createManagerUser.Succeeded)
                {
                    //here we tie the new user to the "Manager" role 
                    await UserManager.AddToRoleAsync(userManager, "Manager");
                }
            }
            // creating a basic user who could maintain the web app
            var userMember = new ApplicationUser
            {
                UserName = "TestMembre@gmail.com",
                Email = "TestMembre@gmail.com",
                EmailConfirmed = true
            };
            string UserMemberPassword = "Sirius@83";
            var _userMembre = await UserManager.FindByEmailAsync("TestMembre@gmail.com");

            if (_userMembre == null)
            {
                var createMembreUser = await UserManager.CreateAsync(userMember, UserMemberPassword);
                if (createMembreUser.Succeeded)
                {
                    //here we tie the new user to the "Membre" role 
                    await UserManager.AddToRoleAsync(userMember, "Membre");
                }
            }
        }

    }
}
