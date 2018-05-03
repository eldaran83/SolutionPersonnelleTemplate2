using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using SolutionPersonnelleTemplate.Data;
using SolutionPersonnelleTemplate.Models.BLL.Interfaces;
using SolutionPersonnelleTemplate.Models.BO;
using static SolutionPersonnelleTemplate.Models.BLL.Managers.MoteurDuJeuManager;

namespace SolutionPersonnelleTemplate.Controllers
{
    public class TestController : Controller
    {


        private readonly ApplicationDbContext _context;
        private readonly IMoteurDuJeu _moteurDuJeu;
 

        public TestController( ApplicationDbContext context, IMoteurDuJeu moteurDuJeu)
        {
            _context = context;
            _moteurDuJeu = moteurDuJeu;
        }

        public IActionResult Index(int? degatInflige, bool? competence)
        {
            if (degatInflige != null)
            {
                ViewBag.degats = degatInflige;
            }
            if (competence != null)
            {
                if (competence == true)
                {
                    ViewBag.competence = "Réussite !";
                }
                else
                {
                    ViewBag.competence = "Echec !";
                }
                
            }
            return View();
        }

        public async Task<IActionResult> Degats(Des.TypeDeDes leTypeDeDesALancer, int? nbDeDesALancer, int? bonusDegat)
        {
            int lesDegats = await _moteurDuJeu.DegatsDes(leTypeDeDesALancer,nbDeDesALancer,bonusDegat);

            return RedirectToAction("Index", new RouteValueDictionary(new
            {
                controller = "Test",
                action = "Index",
                degatInflige = lesDegats
            }));
        }

        public async Task<IActionResult> Competence(Personne laPersonne, Caracteristiques caracteristiqueATester, int difficulte)
        {
            //pour le test je recupere le personnage deja crée apres ca sera celui dela partie
            Personne lePerso = _context.Personnes.Where(u => u.PersonneID == 4).FirstOrDefault();

            int laValeurATester = _moteurDuJeu.ValeurDeLaCaracteristiqueATester(laPersonne, caracteristiqueATester);

            bool reussiteOuPasAuTest = await _moteurDuJeu.TestCaracteristique(laValeurATester, difficulte);

            return RedirectToAction("Index", new RouteValueDictionary(new
            {
                controller = "Test",
                action = "Index",
                competence = reussiteOuPasAuTest
            }));
        }

    }
}