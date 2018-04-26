using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using SolutionPersonnelleTemplate.Data;
using SolutionPersonnelleTemplate.Models;
using SolutionPersonnelleTemplate.Models.BLL.Interfaces;
using SolutionPersonnelleTemplate.Models.BO;
using SolutionPersonnelleTemplate.Models.ViewModels;

namespace SolutionPersonnelleTemplate.Controllers.LogicWebSite
{
    public class PartieController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUtilisateurInterface _utilisateurManager;
        private readonly IRepositoryHistoire _histoireManager;
        private readonly IRepositoryPartie _partieManager;
        private readonly IRepositoryMessage _messageManager;

        public PartieController(ApplicationDbContext context, UserManager<ApplicationUser> userManager,
            IUtilisateurInterface utilisateurManager, IRepositoryHistoire histoireManager,
            IRepositoryPartie partieManager, IRepositoryMessage messageManager)
        {
            _context = context;
            _userManager = userManager;
            _utilisateurManager = utilisateurManager;
            _histoireManager = histoireManager;
            _partieManager = partieManager;
            _messageManager = messageManager;
        }

        public async Task<IActionResult> MonAventure(int? partieID)
        {
            if (partieID == null)
            {
                return NotFound();
            }
             Partie laPartie = await _partieManager.GetPartieById(Convert.ToInt32(partieID));
            //on charge le dernier message qui a été joué
            Message leMessageADistribuer = await _messageManager.GetMessageByMessageIDAndHistoireId(laPartie.DernierMessageJouer, laPartie.HistoireID);
            if (leMessageADistribuer !=null)
            {
                return View(leMessageADistribuer);
            }
            else
            {
                //on cherche le message par lequel commence l histoire ( on suppose - pour le moment- que c 'est le premier message créer)
                 Message leDebutDeLHistoire = await _messageManager.RetourneLePremierMessageDeLHistoire(laPartie.HistoireID);
                if (leDebutDeLHistoire != null)
                {
                    leMessageADistribuer = leDebutDeLHistoire;
                }
            }
            //redirection vers là où en est le jouer dans sa partie 
            return View(leMessageADistribuer); //ATTENTION LA VU N'EST PAS TYPE EN MESSAGEVIEWMODELE !!!!
        }


        public async Task<IActionResult> Jouer(int HistoireID)
        {
            //je recupere la vraie identité de l user
            var applicationUserID = _userManager.GetUserId(HttpContext.User);
            //je recupere l utilisateur
            Utilisateur utilisateur = await _utilisateurManager.GetUtilisateurByIdAsync(applicationUserID);
            string utilisateurID = utilisateur.ApplicationUserID;

            //pour la redirection 
            int idHistoire = HistoireID;
            //verifie si l utilisateur joue deja ou pas a cette histoire
            if (await _partieManager.DejaJouerDeCetteHistoire(HistoireID,utilisateurID))
            {
                Partie laPartie = await _partieManager.GetPartieByUtilisateurAndHistoireID(HistoireID, utilisateurID);
                //redirection vers là où en est le jouer dans sa partie 
                return RedirectToAction("MonAventure", new RouteValueDictionary(new
                {
                    controller = "Partie",
                    action = "MonAventure",
                    partieID = laPartie.PartieID
                }));
            }
            else
            {
                return RedirectToAction("CreerSonHeros", new RouteValueDictionary(new
                {
                    controller = "Partie",
                    action = "CreerSonHeros",
                    HistoireID = idHistoire
                }));
            }
        }


        [HttpGet]
        public async Task<IActionResult> CreerSonHeros(int HistoireID)
        {
            //je recupere la vraie identité de l user
            var applicationUserID = _userManager.GetUserId(HttpContext.User);
            //je recupere l utilisateur
            Utilisateur utilisateur = await _utilisateurManager.GetUtilisateurByIdAsync(applicationUserID);

            Histoire lhistoireAJouer = await _histoireManager.GetHistoireByID(HistoireID);

            CreerSonHerosViewModel creerSonHeros = new CreerSonHerosViewModel
            {
                Partie = null,
                Histoire = lhistoireAJouer,
                Utilisateur = utilisateur,
                Heros = null
            };

            ViewBag.HistoireID = creerSonHeros.Histoire.HistoireID;
            ViewBag.ApplicationUserID = creerSonHeros.Utilisateur.ApplicationUserID;

            return View(creerSonHeros);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreerSonHeros(CreerSonHerosViewModel creerSonHerosModel)
        {
            try
            {
                await _partieManager.NouvellePartie(creerSonHerosModel);
            }
            catch (Exception ex)
            {
                Console.WriteLine("ajout heros ou partie mal passé" + ex);
            }




            return View("Index"); // redirection a changer pour pointer la où il faut 
        }


        // GET: Partie
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Parties.Include(p => p.Histoire).Include(p => p.Utilisateur);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Partie/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var partie = await _context.Parties
                .Include(p => p.Histoire)
                .Include(p => p.Utilisateur)
                .SingleOrDefaultAsync(m => m.PartieID == id);
            if (partie == null)
            {
                return NotFound();
            }

            return View(partie);
        }

        // GET: Partie/Create
        public IActionResult Create()
        {
            ViewData["HistoireID"] = new SelectList(_context.Histoires, "HistoireID", "Synopsis");
            ViewData["UtilisateurID"] = new SelectList(_context.Utilisateurs, "ApplicationUserID", "ApplicationUserID");
            return View();
        }

        // POST: Partie/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PartieID,UtilisateurID,HistoireID")] Partie partie)
        {
            if (ModelState.IsValid)
            {
                _context.Add(partie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["HistoireID"] = new SelectList(_context.Histoires, "HistoireID", "Synopsis", partie.HistoireID);
            ViewData["UtilisateurID"] = new SelectList(_context.Utilisateurs, "ApplicationUserID", "ApplicationUserID", partie.UtilisateurID);
            return View(partie);
        }

        // GET: Partie/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var partie = await _context.Parties.SingleOrDefaultAsync(m => m.PartieID == id);
            if (partie == null)
            {
                return NotFound();
            }
            ViewData["HistoireID"] = new SelectList(_context.Histoires, "HistoireID", "Synopsis", partie.HistoireID);
            ViewData["UtilisateurID"] = new SelectList(_context.Utilisateurs, "ApplicationUserID", "ApplicationUserID", partie.UtilisateurID);
            return View(partie);
        }

        // POST: Partie/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PartieID,UtilisateurID,HistoireID")] Partie partie)
        {
            if (id != partie.PartieID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(partie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PartieExists(partie.PartieID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["HistoireID"] = new SelectList(_context.Histoires, "HistoireID", "Synopsis", partie.HistoireID);
            ViewData["UtilisateurID"] = new SelectList(_context.Utilisateurs, "ApplicationUserID", "ApplicationUserID", partie.UtilisateurID);
            return View(partie);
        }

        // GET: Partie/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var partie = await _context.Parties
                .Include(p => p.Histoire)
                .Include(p => p.Utilisateur)
                .SingleOrDefaultAsync(m => m.PartieID == id);
            if (partie == null)
            {
                return NotFound();
            }

            return View(partie);
        }

        // POST: Partie/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var partie = await _context.Parties.SingleOrDefaultAsync(m => m.PartieID == id);
            _context.Parties.Remove(partie);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PartieExists(int id)
        {
            return _context.Parties.Any(e => e.PartieID == id);
        }
    }
}
