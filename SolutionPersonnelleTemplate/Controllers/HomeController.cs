using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SolutionPersonnelleTemplate.Data;
using SolutionPersonnelleTemplate.Models;
using SolutionPersonnelleTemplate.Models.BLL.Interfaces;
using SolutionPersonnelleTemplate.Models.BO;
using SolutionPersonnelleTemplate.Models.ViewModels;
using SolutionPersonnelleTemplate.Services;

namespace SolutionPersonnelleTemplate.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger _logger;
        private readonly IHostingEnvironment _env;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _context;
        private readonly IEmailSender _emailSender;
        private readonly IUtilisateurInterface _utilisateurManager;
        /// <summary>
        /// constructeur du controller
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="signInManager"></param>
        /// <param name="logger"></param>
        /// <param name="env"></param>
        public HomeController(UserManager<ApplicationUser> userManager,
           SignInManager<ApplicationUser> signInManager,
            ApplicationDbContext context,
           ILogger<AccountController> logger,
           IHostingEnvironment env,
           IEmailSender emailSender,
           IUtilisateurInterface utilisateurManager
          )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _logger = logger;
            _env = env;
            _emailSender = emailSender;
            _utilisateurManager = utilisateurManager;
          }


        public  IActionResult Index()
        {
            return View();
        }

        public IActionResult About(string toto)
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Contact(string subjet)
        {
            //je recupere la vraie identité de l user
            var applicationUserID = _userManager.GetUserId(HttpContext.User);
            //je recupere l utilisateur
            Utilisateur utilisateur = await _utilisateurManager.GetUtilisateurByIdAsync(applicationUserID);

            if (utilisateur == null)
            {
                return NotFound();
            }

            ViewBag.utilisateurID = utilisateur.ApplicationUserID;
            ViewBag.subjet = "";

            if (subjet != null)
            {
                ViewBag.subjet = subjet;
            }
           
             return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Contact(ContactViewModel model)
        {
            //je recupere la vraie identité de l user
            var applicationUserID = _userManager.GetUserId(HttpContext.User);
            //je recupere l utilisateur
            Utilisateur utilisateur = await _utilisateurManager.GetUtilisateurByIdAsync(applicationUserID);

            if (utilisateur == null)
            {
                return NotFound();
            }

            string subject = "";
            if (model.Subject == "devenirScribe")
            {
                 subject = "L'utilisateur " + model.UserId + " souhaite devenir Sribe, il faut le passer en Manager";
            }
            else
            {
                model.Subject.ToString();
            }

            string email = utilisateur.Email;
         
            string message = model.Message;

            if (ModelState.IsValid)
            {
                await _emailSender.SendEmailAsync(email, subject, message);
            }

            return View("Index"); // a changer avec le retour 
        }


        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
