using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        public PartieController(ApplicationDbContext context, UserManager<ApplicationUser> userManager,
            IUtilisateurInterface utilisateurManager, IRepositoryHistoire histoireManager)
        {
            _context = context;
            _userManager = userManager;
            _utilisateurManager = utilisateurManager;
            _histoireManager = histoireManager;
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
                EtreVivant = null
            };

            ViewBag.HistoireID = creerSonHeros.Histoire.HistoireID;
            ViewBag.ApplicationUserID = creerSonHeros.Utilisateur.ApplicationUserID;

            return View(creerSonHeros);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreerSonHeros(CreerSonHerosViewModel creerSonHerosModel)
        {

            Partie laPartie = new Partie
            {
                HistoireID = creerSonHerosModel.Histoire.HistoireID,
                UtilisateurID = creerSonHerosModel.Utilisateur.ApplicationUserID,

            };
            _context.Add(laPartie);
            await _context.SaveChangesAsync();

            EtreVivant heros = new EtreVivant {
                Charisme =creerSonHerosModel.EtreVivant.Charisme,
                Constitution =creerSonHerosModel.EtreVivant.Constitution,
                Dexterite = creerSonHerosModel.EtreVivant.Dexterite,
                Force = creerSonHerosModel.EtreVivant.Force,
                 Intelligence =creerSonHerosModel.EtreVivant.Intelligence,
                 Nom = creerSonHerosModel.EtreVivant.Nom,
                 PartieID = laPartie.PartieID
            };

            _context.Add(heros);
            await _context.SaveChangesAsync();


            return View(creerSonHerosModel);
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
