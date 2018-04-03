﻿using System;
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

namespace SolutionPersonnelleTemplate.Controllers.LogicWebSite
{
    public class HistoireController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly IRepositoryHistoire _histoireRepository;
        private readonly IUtilisateurInterface _utilisateurManager;

        public HistoireController(UserManager<ApplicationUser> userManager,ApplicationDbContext context, IRepositoryHistoire histoireRepository, IUtilisateurInterface utilisateurManager)
        {
            _userManager = userManager;
            _context = context;
            _histoireRepository = histoireRepository;
            _utilisateurManager = utilisateurManager;
        }

        // GET: Histoire
        public async Task<IActionResult> Index()
        {
            var lesHistoires = await _histoireRepository.GetAllHistoiresAsync();
            return View(lesHistoires);
        }

        // GET: Histoire/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var histoire = await _context.Histoires
                .SingleOrDefaultAsync(m => m.HistoireID == id);
            if (histoire == null)
            {
                return NotFound();
            }

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
        public async Task<IActionResult> Create(Histoire histoireModele)
         {
            //vérification pour savoir si le titre de l histoire est libre ou pas 
             if (await _histoireRepository.HistoireExist(histoireModele.Titre)) //si pas libre on renvoie le formulaire
            {
                ViewBag.error = "Ce titre n'est pas disponible.";
                ViewBag.Createur = histoireModele.Createur;
                ViewBag.UtilisateurID = histoireModele.UtilisateurID;

                return View(histoireModele);
            }
            if (ModelState.IsValid)
            {
               var laNouvelleHistoire = await _histoireRepository.NouvelleHistoire(histoireModele);
                //_context.Add(histoireModele);
                //await _context.SaveChangesAsync();
                // return RedirectToAction(nameof(Index));
                return RedirectToAction("Create", new RouteValueDictionary(new
                {
                    controller = "Message",
                    action = "Create",
                    histoireId = laNouvelleHistoire.HistoireID
                }));
            }
            return View(histoireModele);
        }
 
    }
}