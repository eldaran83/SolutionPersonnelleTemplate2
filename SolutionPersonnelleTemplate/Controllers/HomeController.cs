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
using PaulMiami.AspNetCore.Mvc.Recaptcha;
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
                if (subjet == "devenirScribe")
                {
                    ViewBag.subject = "Demande pour devenir un Scribe";
                }
              
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateRecaptcha]
        public async Task<IActionResult> Contact(ContactViewModel model)
        {
            //pour la gestion du Captcha
            var captcha = ModelState["g-recaptcha-response"];
            if (captcha != null && captcha.Errors.Any())
            {
                this.ViewBag.Error = "Le Captcha est erroné";
                this.ViewBag.subjet = model.Subject.ToString();
                return View(model);
            }
            //je recupere la vraie identité de l user
            var applicationUserID = _userManager.GetUserId(HttpContext.User);
            //je recupere l utilisateur
            Utilisateur utilisateur = await _utilisateurManager.GetUtilisateurByIdAsync(applicationUserID);

            if (utilisateur == null)
            {
                return NotFound();
            }

            //string subject = "";
            //string email = "";
            //string message = "";

            //if (model.Subject == "Demande pour devenir un Scribe")
            //{
            //    subject = "devenirScribe";
            //    email = "eldaran83@gmail.com"; // mail en dur pour le moment , par al suite a récuperer du Json
            //    message = "<p>Un utilisateur veut devenir un scribe et il faut le passer en statut manager </p>" +
            //        "<p>Son ID est le : " + utilisateur.ApplicationUserID + "</p>" +
            //        "<p>Son email est :" + utilisateur.Email + "</p>" +
            //        "<p>Pour le passer directement Sribe vous pouvez cliquer" + "<a href=\"https://localhost:44344/Admin/Edit?userId=" + utilisateur.ApplicationUserID + "\">ICI</a></p>" +
            //        "<p>Son message :</p>" +
            //        "<p>" + model.Message + "</p>";
            //}
            //else
            //{
            //    email = utilisateur.Email.ToString();
            //    subject= model.Subject.ToString();
            //    message = model.Message.ToString();

            //}

            if (ModelState.IsValid)
            {
                string subject = model.Subject;
                string email = "eldaran83@gmail.com"; // mail en dur pour le moment , par al suite a récuperer du Json
                string message ="<p>Son ID est le : " + utilisateur.ApplicationUserID + "</p>" +
                            "<p>Son email est :" + utilisateur.Email + "</p>" +
                             "<p>Son message :</p>" +
                            "<p>" + model.Message + "</p>";


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
