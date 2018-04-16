using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using ReflectionIT.Mvc.Paging;
using SolutionPersonnelleTemplate.Data;
using SolutionPersonnelleTemplate.Models;
using SolutionPersonnelleTemplate.Models.BLL.Interfaces;
using SolutionPersonnelleTemplate.Models.BO;
using SolutionPersonnelleTemplate.Models.ViewModels;


namespace SolutionPersonnelleTemplate.Controllers.LogicWebSite
{
    public class HistoireController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly IRepositoryHistoire _histoireRepository;
        private readonly IUtilisateurInterface _utilisateurManager;
        private readonly IHostingEnvironment _env;
        private readonly IRepositoryFichier _fichierRepository;

        public HistoireController(UserManager<ApplicationUser> userManager,ApplicationDbContext context, IRepositoryHistoire histoireRepository, IUtilisateurInterface utilisateurManager, IHostingEnvironment env, IRepositoryFichier fichierRepository)
        {
            _userManager = userManager;
            _context = context;
            _histoireRepository = histoireRepository;
            _utilisateurManager = utilisateurManager;
            _env = env;
            _fichierRepository = fichierRepository;
        }

        // GET: Histoire
        public async Task<IActionResult> Index(string rechercheHistoire/*, int page =1*/)
        {
            //nb d'histoire par page 
            //int nbParPage = 6;
            //var lesHistoires = _context.Histoires.AsNoTracking().OrderByDescending(h => h.NombreDeFoisJouee);
            //var listeHistoires = await PagingList.CreateAsync(lesHistoires, nbParPage, page);

            IEnumerable<Histoire> listeHistoires = await _histoireRepository.GetAllHistoiresAsync();

            if (!String.IsNullOrEmpty(rechercheHistoire))
            {
                listeHistoires = _context.Histoires.Where(h => h.Titre.ToUpper().Contains(rechercheHistoire.ToUpper())
                                            || h.Createur.ToUpper().Contains(rechercheHistoire.ToUpper()));
            }

           
            return View(listeHistoires);

        }

        // GET: Histoire/Details/5
        public async Task<IActionResult> Details(int? histoireID)
        {
            if (histoireID == null)
            {
                return NotFound();
            }

            Histoire histoire = await _histoireRepository.GetHistoireByID(histoireID);
            if (histoire == null)
            {
                return NotFound();
            }

            //////////////////////////////////////////////////////////////////////////////////////////
            //      GESTION de la photo
            //////////////////////////////////////////////////////////////////////////////////////////
            if (histoire.UrlMedia != null)
            {
                string img = histoire.UrlMedia.ToString();
                ViewBag.ImgPath = img;
            }
            else
            {
                ViewBag.ImgPath = "/images/story-media-default.jpg";
            }
            ///////////////////////////////////////////////////////////////////////
            //  FIN gestion image
            ///////////////////////////////////////////////////////////////////////

            return View(histoire);
        }

        // GET: Histoire/Create
        public async Task<IActionResult> Create()
        {
            //je recupere la vraie identité de l user
            var ApplicationUserID = _userManager.GetUserId(HttpContext.User);
            //je recupere l utilisateur
            Utilisateur utilisateur = await _utilisateurManager.GetUtilisateurByIdAsync(ApplicationUserID);

            string role = utilisateur.Role;

            // seul un manager ou un administrateur a le droit de créer une histoire
             if (role == "Membre")
            {
                 return RedirectToAction("MonCompte", new RouteValueDictionary(new
                {
                    controller = "Utilisateur",
                    action = "MonCompte",
                    Id = utilisateur.ApplicationUserID
                }));
            }

            string pseudo = utilisateur.Pseudo;
            ViewBag.Createur = pseudo;
            ViewBag.UtilisateurID = ApplicationUserID;

            return View();
        }

        // POST: Histoire/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(HistoireViewModel histoireVM, IFormCollection form )
         {
            //vérification pour savoir si le titre de l histoire est libre ou pas 
             if (await _histoireRepository.HistoireExist(histoireVM.Histoire.Titre)) //si pas libre on renvoie le formulaire
            {
                ViewBag.error = "Ce titre n'est pas disponible.";
                ViewBag.Createur = histoireVM.Histoire.Createur;
                ViewBag.UtilisateurID = histoireVM.Histoire.UtilisateurID;

                return View(histoireVM.Histoire);
            }
             //si le modele est valide on crée l histoire
            if (ModelState.IsValid)
            {
                Histoire laNouvelleHistoire = await _histoireRepository.NouvelleHistoire(histoireVM.Histoire);

                //////////////////////////////////////////////////////////////////////////////////////////
                //      GESTION de la photo
                //////////////////////////////////////////////////////////////////////////////////////////
                if (form.Files[0].FileName != "")
                {
                    if (histoireVM.form.Files[0].Length >= 5242880) //Maxi 5 mo pour l image
                    {
                        ViewBag.errorFichier = "L'image doit avoir une taille inférieure à 5 Mo.";
                    }
                    string webRoot = _env.WebRootPath; // récupère l environnement
                    string nameDirectory = "/StoryFiles/"; // nomme le dossier dans lequel le média va se retrouver ici StoryFiles pour l image de histoire
                    //ATTENTION Cest une nouvelle histoire donc leNouvelleHistoire
                    string storyId = Convert.ToString(laNouvelleHistoire.HistoireID) + "_Histoire"; // sert à la personnalisation du dossier pour l utilisateur
                    string nomDuDossier = "/ImageHistoire/"; // variable qui sert à nommer le dossier dans lequel le fichier sera ajouté, ICI c est le dossier Image

                    //Comme l utilisateur ne peut avoir qu'un seul avatar, on vérifie avant d'ajouter un fichier
                    //que le dossier n'a pas d autre image en supprimant tous les fichiers qui pourraient s y trouver
                    try
                    {
                        var sourceDir = Path.Combine(
                                    Directory.GetCurrentDirectory(), "wwwroot" + nameDirectory  + nomDuDossier + storyId);

                        string[] listeImage = Directory.GetFiles(sourceDir);
                        // Copy picture files.          
                        foreach (string f in listeImage)
                        {
                            // Remove path from the file name.
                            string fName = f.Substring(sourceDir.Length);
                            _fichierRepository.RemoveFichier(sourceDir, fName);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }

                    //ajoute le fichier 
                    var imageURl = _fichierRepository.SaveFichier(webRoot, nameDirectory, nomDuDossier, storyId, form);
                    laNouvelleHistoire.UrlMedia = imageURl;

                    //je met à jour l histoire pour quelle est le nouveau lien de son image
                    await _histoireRepository.UpdateHistoire(laNouvelleHistoire);
                }
 
                 return RedirectToAction("Create", new RouteValueDictionary(new
                {
                    controller = "Message",
                    action = "Create",
                    histoireId = laNouvelleHistoire.HistoireID
                }));
            }
            else // sinon on renvoi
            {
                return View(histoireVM.Histoire);
            }
        }
 
        // GET: Message/Delete/5
        public async Task<IActionResult> Delete(int? HistoireID)
        {
            if (HistoireID == null)
            {
                return NotFound();
            }
            Histoire histoire = await _histoireRepository.GetHistoireByID(HistoireID);
             if (histoire == null)
            {
                return NotFound();
            }

            ViewData["HistoireID"] = HistoireID;
            return View(histoire);
        }

        // POST: Message/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? HistoireID)
        {

            if (HistoireID == null)
            {
                return NotFound();
            }
            await _histoireRepository.RemoveHistoireById(HistoireID);

            return RedirectToAction("Index", new RouteValueDictionary(new
            {
                controller = "Histoire",
                action = "Index",
              
            }));
        }

        // GET: Message/Edit/5
        public async Task<IActionResult> Edit(int? HistoireID)
        {
            if (HistoireID == null)
            {
                return NotFound();
            }

            Histoire histoire = await _histoireRepository.GetHistoireByID(HistoireID);
            if (histoire == null)
            {
                return NotFound();
            }
            //////////////////////////////////////////////////////////////////////////////////////////
            //      GESTION de la photo
            //////////////////////////////////////////////////////////////////////////////////////////
            if (histoire.UrlMedia != null)
            {
                string img = histoire.UrlMedia.ToString();
                ViewBag.ImgPath = img;
            }
            else
            {
                ViewBag.ImgPath = "/images/story-media-default.jpg";
            }
            ///////////////////////////////////////////////////////////////////////
            //  FIN gestion image
            ///////////////////////////////////////////////////////////////////////

            HistoireViewModel histoireVM = new HistoireViewModel
            {
                Histoire = histoire,
                form = null
            };

            ViewData["HistoireID"] = HistoireID;
            return View(histoireVM);
        }

        // POST: Message/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(HistoireViewModel histoireVM)
        {

            //if (await _histoireRepository.HistoireExist(histoireVM.Histoire.Titre))
            //{
            //    ViewBag.error = "Ce titre de message est déjà utilisé dans cette histoire";
            //    ViewData["HistoireID"] = histoireVM.Histoire.HistoireID;
            //    return View(histoireVM);
            //}

            if (ModelState.IsValid)
            {
                //////////////////////////////////////////////////////////////////////////////////////////
                //      GESTION de la photo
                //////////////////////////////////////////////////////////////////////////////////////////
                if (histoireVM.form.Files[0].FileName != "")
                {
                    if (histoireVM.form.Files[0].Length >= 5242880) //Maxi 5 mo pour l image
                    {
                        ViewBag.errorFichier = "L'image doit avoir une taille inférieure à 5 Mo.";
                    }
                    string webRoot = _env.WebRootPath; // récupère l environnement
                    string nameDirectory = "/StoryFiles/"; // nomme le dossier dans lequel le média va se retrouver ici StoryFiles pour l image de histoire
                    string storyId = Convert.ToString(histoireVM.Histoire.HistoireID)+"_Histoire"; // sert à la personnalisation du dossier pour l utilisateur
                    string nomDuDossier = "/ImageHistoire/"; // variable qui sert à nommer le dossier dans lequel le fichier sera ajouté, ICI c est le dossier Image
                   
                    //Comme l utilisateur ne peut avoir qu'un seul avatar, on vérifie avant d'ajouter un fichier
                    //que le dossier n'a pas d autre image en supprimant tous les fichiers qui pourraient s y trouver
                    try
                    {
                        var sourceDir = Path.Combine(
                                    Directory.GetCurrentDirectory(), "wwwroot" + nameDirectory  + nomDuDossier + storyId);

                        string[] listeImage = Directory.GetFiles(sourceDir);
                        // Copy picture files.          
                        foreach (string f in listeImage)
                        {
                            // Remove path from the file name.
                            string fName = f.Substring(sourceDir.Length);
                            _fichierRepository.RemoveFichier(sourceDir, fName);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }

                    //ajoute le fichier 
                    var imageURl = _fichierRepository.SaveFichier(webRoot, nameDirectory, nomDuDossier, storyId, histoireVM.form);
                    histoireVM.Histoire.UrlMedia = imageURl;

                    //je met à jour l histoire pour quelle est le nouveau lien de son image
                    await _histoireRepository.UpdateHistoire(histoireVM.Histoire);

                    ViewData["HistoireID"] = histoireVM.Histoire.HistoireID;
                    return RedirectToAction("Index", new RouteValueDictionary(new
                    {
                        controller = "Histoire",
                        action = "Index",

                    }));
                }
 
              await _histoireRepository.UpdateHistoire(histoireVM.Histoire);

                ViewData["HistoireID"] = histoireVM.Histoire.HistoireID;
                return RedirectToAction("Index", new RouteValueDictionary(new
                {
                    controller = "Histoire",
                    action = "Index",
                   
                }));
            }

            ViewData["HistoireID"] = histoireVM.Histoire.HistoireID;
            return View(histoireVM);
        }

    }
}
