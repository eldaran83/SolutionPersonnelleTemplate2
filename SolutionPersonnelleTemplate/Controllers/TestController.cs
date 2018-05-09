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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PhaseDeCombat(DeroulementDuCombat deroulement)
        {
            int round = deroulement.NombreRound;
            bool? init = deroulement.JoueurInit;
            ActionCombat? action = deroulement.ActionChoisie;
            Personne leHeros = deroulement.LeHeros;
            Personne leMonstre = deroulement.LeMonstre;

            if (ModelState.IsValid)
            {
                //on fais un nouveau round avec les infos du round precedent
                Dictionary<string, object> nouveauRound = await _moteurDuJeu.PhaseDeCombat(round, init, action, leHeros, leMonstre);

                int nbRound = Convert.ToInt16(nouveauRound["Round n°"]);
                Personne heroCombatAutreRound = nouveauRound["leHeros"] as Personne;
                Personne monstreCombatAutreRound = nouveauRound["leMonstre"] as Personne;

                DeroulementDuCombat leDeroulementDuCombatNouveauRound = new DeroulementDuCombat
                {
                    NombreRound = nbRound,
                    JoueurInit = init,
                    ActionChoisie = action,
                    LeHeros = heroCombatAutreRound,
                    LeMonstre = monstreCombatAutreRound,
                    MessageDuCombat = nouveauRound["leMessage"] as string
                };
                leDeroulementDuCombatNouveauRound.LeHeros.PointsDeVieActuels = heroCombatAutreRound.PointsDeVieActuels;
                leDeroulementDuCombatNouveauRound.LeMonstre.PointsDeVieActuels = monstreCombatAutreRound.PointsDeVieActuels;

                if (_moteurDuJeu.EstVivant(leDeroulementDuCombatNouveauRound.LeHeros) && _moteurDuJeu.EstVivant(leDeroulementDuCombatNouveauRound.LeMonstre))
                {
                    //ils sont en vie on continue
                    return View(leDeroulementDuCombatNouveauRound);
                }
                else
                {
                    //on a gagné ou perdu 
                    if (leDeroulementDuCombatNouveauRound.LeHeros.PointsDeVieActuels > 1)
                    {
                        ViewBag.Combat = "Gagné";
                        return View(leDeroulementDuCombatNouveauRound); // A CHANGER APRES TEST
                    }
                    else
                    {
                        ViewBag.Combat = "Perdu";
                        return View(leDeroulementDuCombatNouveauRound); // A CHANGER APRES TEST
                    }
                }

            }
            return View(deroulement);
        }

        public async Task<IActionResult> PhaseDeCombat(int round, bool init, ActionCombat? action, Personne leHeros, Personne leMonstre)
        {
            if (round <1)
            {
                Personne heros = new Personne
                {
                    Nom = "Toto le héros",

                    //Classe du perso 
                    ClasseDuPersonnage = Personne.Classe.Guerrier,
                    //caractéristique
                    Force = 5,
                    BonusForce = _moteurDuJeu.QuelBonusPourLaCaracteristique(5),
                    Dexterite = 1,
                    BonusDexterite = _moteurDuJeu.QuelBonusPourLaCaracteristique(1),
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

                leHeros = heros;

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

                leMonstre = monstre;

                //on fais le 1er round sans action juste pour l init
                Dictionary<string, object> leCombat = await _moteurDuJeu.PhaseDeCombat(0, null, null, leHeros, leMonstre);

                int nbRound = Convert.ToInt16(leCombat["Round n°"]);
                bool leJoueurAtIlLInit;
                var joueurInitVar = leCombat["initiativeHeros"];
                string leMessageCombat = leCombat["leMessage"] as string;
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
                    NombreRound = nbRound,
                    JoueurInit = leJoueurAtIlLInit,
                    ActionChoisie = ActionCombat.CombattrePhysique,
                    LeHeros = heroCombat,
                    LeMonstre = monstreCombat, 
                    MessageDuCombat = leMessageCombat
                };
                var pvHeros = leDeroulementDuCombat.LeHeros.PointsDeVieActuels;
                var pvMonstre = leDeroulementDuCombat.LeMonstre.PointsDeVieActuels;

                return View(leDeroulementDuCombat);

            }
            else // on est dans un round different du premier
            {

                return View();
            }

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