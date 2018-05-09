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


        public async Task<IActionResult> Combattre2()
        {
            Personne heros = new Personne
            {
                Nom = "Toto le héros",

                //Classe du perso 
                ClasseDuPersonnage = Personne.Classe.Guerrier,
                //caractéristique
                Force = 15,
                BonusForce = _moteurDuJeu.QuelBonusPourLaCaracteristique(15),
                Dexterite = 14,
                BonusDexterite = _moteurDuJeu.QuelBonusPourLaCaracteristique(14),
                Constitution = 15,
                BonusConstitution = _moteurDuJeu.QuelBonusPourLaCaracteristique(15),
                Intelligence = 8,
                BonusIntelligence = _moteurDuJeu.QuelBonusPourLaCaracteristique(8),
                Sagesse = 10,
                BonusSagesse = _moteurDuJeu.QuelBonusPourLaCaracteristique(10),
                Charisme = 10,
                BonusCharisme = _moteurDuJeu.QuelBonusPourLaCaracteristique(10),
                // point de vie
                PointsDeVieMax = 15,
                PointsDeVieActuels = 15,
                //niveau 
                PointsExperience = 1, // le personnage commence avec 1 point
                NiveauDuPersonnage = Personne.Niveau.Niveau1, // le personnage commence au niveau 1
                //Zone de combat 
                AttaqueMaitriseArme = 4,
                ClasseArmure = 14,
                BonusAuDegatPhysique = 2,

                AttaqueMaitriseMagique = -1,
                BonusAuDegatMagique = -1,

                // défences
                Reflexe = 1,
                Vigueur = 2,
                Volonte = -1

            };

            Personne monstre = new Personne
            {
                Nom = "Bou le monstre",

                //Classe du perso 
                ClasseDuPersonnage = Personne.Classe.Guerrier,
                //caractéristique
                Force = 15,
                BonusForce = _moteurDuJeu.QuelBonusPourLaCaracteristique(15),
                Dexterite = 4,
                BonusDexterite = _moteurDuJeu.QuelBonusPourLaCaracteristique(14),
                Constitution = -4,
                BonusConstitution = _moteurDuJeu.QuelBonusPourLaCaracteristique(15),
                Intelligence = 8,
                BonusIntelligence = _moteurDuJeu.QuelBonusPourLaCaracteristique(8),
                Sagesse = 10,
                BonusSagesse = _moteurDuJeu.QuelBonusPourLaCaracteristique(10),
                Charisme = 10,
                BonusCharisme = _moteurDuJeu.QuelBonusPourLaCaracteristique(10),
                // point de vie
                PointsDeVieMax = 15,
                PointsDeVieActuels = 15,
                //niveau 
                PointsExperience = 1, // le personnage commence avec 1 point
                NiveauDuPersonnage = Personne.Niveau.Niveau1, // le personnage commence au niveau 1
                //Zone de combat 
                AttaqueMaitriseArme = 4,
                ClasseArmure = 14,
                BonusAuDegatPhysique = 2,

                AttaqueMaitriseMagique = -1,
                BonusAuDegatMagique = -1,

                // défences
                Reflexe = 1,
                Vigueur = 2,
                Volonte = -1

            };
      
            //on fais le 1er round sans action juste pour l init
            Dictionary<string, object> leCombat = await _moteurDuJeu.PhaseDeCombat(0, null, null, heros, monstre);

            int nbRound = Convert.ToInt16(leCombat["Round n°"]);

            bool leJoueurAtIlLInit;
            var joueurInitVar = leCombat["initiativeHeros"];
            //leJoueurAtIlLInit = joueurInitVar;

            if (joueurInitVar.Equals(true))
            {
                leJoueurAtIlLInit = true;
            }
            else
            {
                leJoueurAtIlLInit = false;
            }


            Personne heroCombat = leCombat["leHeros"] as Personne;
            Personne monstreCombat = leCombat["leMonstre"] as Personne;

            DeroulementDuCombat leDeroulementDuCombat = new DeroulementDuCombat
            {
                NombreRound= nbRound,
                JoueurInit = leJoueurAtIlLInit,
                ActionChoisie = ActionCombat.CombattrePhysique,
                LeHeros = heroCombat,
                LeMonstre =monstreCombat
            };
            var pvHeros = leDeroulementDuCombat.LeHeros.PointsDeVieActuels;
            var pvMonstre = leDeroulementDuCombat.LeMonstre.PointsDeVieActuels;



            while (_moteurDuJeu.EstVivant(leDeroulementDuCombat.LeMonstre) && _moteurDuJeu.EstVivant(leDeroulementDuCombat.LeHeros))
            {
                //on fais un nouveau round avec les infos du round precedent
                Dictionary<string, object> nouveauRound = await _moteurDuJeu.PhaseDeCombat(
                    leDeroulementDuCombat.NombreRound,
                    leDeroulementDuCombat.JoueurInit,
                    leDeroulementDuCombat.ActionChoisie,
                    heros,
                    monstre);

                int nombreRoundnouveauRound = Convert.ToInt16(nouveauRound["Round n°"]);
                leDeroulementDuCombat.NombreRound = nombreRoundnouveauRound;

                leDeroulementDuCombat.JoueurInit = leDeroulementDuCombat.JoueurInit;

                leDeroulementDuCombat.ActionChoisie = ActionCombat.CombattrePhysique;

                Personne heroCombatAutreRound = nouveauRound["leHeros"] as Personne;
                leDeroulementDuCombat.LeHeros = heroCombatAutreRound;
               Personne monstreCombatAutreRound = nouveauRound["leMonstre"] as Personne;
                leDeroulementDuCombat.LeMonstre = monstreCombatAutreRound;


                var pvHerosAutreRound = leDeroulementDuCombat.LeHeros.PointsDeVieActuels;
                var pvMonstreAutreRound = leDeroulementDuCombat.LeMonstre.PointsDeVieActuels;
            }

            if (leDeroulementDuCombat.LeHeros.PointsDeVieActuels >1)
            {
                ViewBag.Combat = "Gagné";
            }
            else
            {
                ViewBag.Combat = "Perdu";
            }
            return View();

        }



        public async Task<IActionResult> Combattre()
        {
            Personne heros = new Personne
            {
                Nom = "Toto le héros",

                //Classe du perso 
                ClasseDuPersonnage = Personne.Classe.Guerrier,
                //caractéristique
                Force = 15,
                BonusForce = _moteurDuJeu.QuelBonusPourLaCaracteristique(15),
                Dexterite = 14,
                BonusDexterite = _moteurDuJeu.QuelBonusPourLaCaracteristique(14),
                Constitution = 15,
                BonusConstitution = _moteurDuJeu.QuelBonusPourLaCaracteristique(15),
                Intelligence = 8,
                BonusIntelligence = _moteurDuJeu.QuelBonusPourLaCaracteristique(8),
                Sagesse = 10,
                BonusSagesse = _moteurDuJeu.QuelBonusPourLaCaracteristique(10),
                Charisme = 10,
                BonusCharisme = _moteurDuJeu.QuelBonusPourLaCaracteristique(10),
                // point de vie
                PointsDeVieMax = 15,
                PointsDeVieActuels = 15,
                //niveau 
                PointsExperience = 1, // le personnage commence avec 1 point
                NiveauDuPersonnage = Personne.Niveau.Niveau1, // le personnage commence au niveau 1
                //Zone de combat 
                AttaqueMaitriseArme = 4,
                ClasseArmure = 14,
                BonusAuDegatPhysique = 2,

                AttaqueMaitriseMagique = -1,
                BonusAuDegatMagique = -1,

                // défences
                Reflexe = 1,
                Vigueur = 2,
                Volonte = -1

            };

            Personne monstre = new Personne
            {
                Nom = "Bou le monstre",

                //Classe du perso 
                ClasseDuPersonnage = Personne.Classe.Guerrier,
                //caractéristique
                Force = 15,
                BonusForce = _moteurDuJeu.QuelBonusPourLaCaracteristique(15),
                Dexterite = 14,
                BonusDexterite = _moteurDuJeu.QuelBonusPourLaCaracteristique(14),
                Constitution = 15,
                BonusConstitution = _moteurDuJeu.QuelBonusPourLaCaracteristique(15),
                Intelligence = 8,
                BonusIntelligence = _moteurDuJeu.QuelBonusPourLaCaracteristique(8),
                Sagesse = 10,
                BonusSagesse = _moteurDuJeu.QuelBonusPourLaCaracteristique(10),
                Charisme = 10,
                BonusCharisme = _moteurDuJeu.QuelBonusPourLaCaracteristique(10),
                // point de vie
                PointsDeVieMax = 15,
                PointsDeVieActuels = 15,
                //niveau 
                PointsExperience = 1, // le personnage commence avec 1 point
                NiveauDuPersonnage = Personne.Niveau.Niveau1, // le personnage commence au niveau 1
                //Zone de combat 
                AttaqueMaitriseArme = 4,
                ClasseArmure = 14,
                BonusAuDegatPhysique = 2,

                AttaqueMaitriseMagique = -1,
                BonusAuDegatMagique = -1,

                // défences
                Reflexe = 1,
                Vigueur = 2,
                Volonte = -1

            };

            string deroulementCombat =  await _moteurDuJeu.Combattre(heros, monstre);
          // ViewData["deroulementCombat"] = deroulementCombat;
           ViewData["deroulementCombat"] = "<p>toto</p><p>titi</p>";
            
            return View();

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