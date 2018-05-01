using Microsoft.AspNetCore.Hosting;
using SolutionPersonnelleTemplate.Data;
using SolutionPersonnelleTemplate.Models.BLL.Interfaces;
using SolutionPersonnelleTemplate.Models.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SolutionPersonnelleTemplate.Models.BLL.Managers
{
    public class PersonneManager : IPersonneInterface
    {
        private readonly ApplicationDbContext _context;
        private readonly IRepositoryFichier _fichierRepository;
        private readonly IHostingEnvironment _env;
        private readonly Personne _perso;
        /// <summary>
        /// contructeur 
        /// </summary>
        /// <param name="context"></param>
        public PersonneManager(ApplicationDbContext context, IRepositoryFichier fichierRepository, IHostingEnvironment env, Personne perso)
        {
            _context = context;
            _fichierRepository = fichierRepository;
            _env = env;
            _perso = perso;
         }

        public async Task<Personne> AjouterPersonneAsync(Personne personne)
        {
            Personne laPersonne = new Personne
            {
                Nom = personne.Nom,
                SexePersonnage = personne.SexePersonnage,
                //Classe du perso 
                ClasseDuPersonnage = personne.ClasseDuPersonnage,
                //caractéristique
                Force = personne.Force,
                BonusForce = _perso.QuelBonusPourLaCaracteristique(personne.Force),
                Dexterite = personne.Dexterite,
                BonusDexterite = _perso.QuelBonusPourLaCaracteristique(personne.Dexterite),
                Constitution = personne.Constitution,
                BonusConstitution = _perso.QuelBonusPourLaCaracteristique(personne.Constitution),
                Intelligence = personne.Intelligence,
                BonusIntelligence = _perso.QuelBonusPourLaCaracteristique(personne.Intelligence),
                Sagesse = personne.Sagesse,
                BonusSagesse = _perso.QuelBonusPourLaCaracteristique(personne.Sagesse),
                Charisme = personne.Charisme,
                BonusCharisme = _perso.QuelBonusPourLaCaracteristique(personne.Charisme),
                // point de vie
                PointsDeVieMax = _perso.PointDeViePremierNiveau(personne.ClasseDuPersonnage),
                PointsDeVieActuels = _perso.PointDeViePremierNiveau(personne.ClasseDuPersonnage),
                //niveau 
                PointsExperience = 1, // le personnage commence avec 1 point
                NiveauDuPersonnage = Personne.Niveau.Niveau1, // le personnage commence au niveau 1
                //Zone de combat 
                AttaqueMaitriseArme = 1, // a changer apres calcul a définir dans les regles 
                ClasseArmure = 1,// a changer apres calcul a définir dans les regles 
                BonusAuDegatPhysique = _perso.BonusAuDegatPhysique,

                AttaqueMaitriseMagique= 1, // a changer apres calcul a définir dans les regles
                BonusAuDegatMagique = _perso.BonusAuDegatMagique,

                // défences
                Reflexe = _perso.Reflexe,
                Vigueur = _perso.Vigueur,
                Volonte = _perso.Volonte
            };

            _context.Personnes.Add(laPersonne);
            await _context.SaveChangesAsync();

            return laPersonne;
        }


        public Task<int> GetPersonneByID(int personneID)
        {
            throw new NotImplementedException();
        }

        public Task<Personne> MiseAJourPersonneByID(Personne personne)
        {
            throw new NotImplementedException();
        }
    }
}
