﻿using Microsoft.AspNetCore.Hosting;
using SolutionPersonnelleTemplate.Data;
using SolutionPersonnelleTemplate.Models.BLL.Interfaces;
using SolutionPersonnelleTemplate.Models.BO;
using SolutionPersonnelleTemplate.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SolutionPersonnelleTemplate.Models.BLL.Managers
{
    public class PartieRepository : IRepositoryPartie
    {
        private readonly ApplicationDbContext _context;
        private readonly IRepositoryFichier _fichierRepository;
        private readonly IRepositoryHistoire _histoireRepository;
        private readonly IHostingEnvironment _env;
        /// <summary>
        /// contructeur 
        /// </summary>
        /// <param name="context"></param>
        public PartieRepository(ApplicationDbContext context, IRepositoryFichier fichierRepository, IHostingEnvironment env,
            IRepositoryHistoire histoireRepository)
        {
            _context = context;
            _fichierRepository = fichierRepository;
            _env = env;
            _histoireRepository = histoireRepository;
        }

        public async Task<bool> DejaJouerDeCetteHistoire(int HistoireID, string utilisateurID)
        {
            Partie dejaJoueur = await _context.Parties
                 .Where(m => m.HistoireID == HistoireID)
                 .Where(m => m.UtilisateurID == utilisateurID)
                 .FirstOrDefaultAsync(); // il faut PENSER a ajouter le using Microsoft.EntityFrameworkCore;

            if (dejaJoueur != null)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public async Task<Partie> NouvellePartie(CreerSonHerosViewModel herosDeLaPartieModel)
        {
            //creation du heros
            Personne leHeros = new Personne
            {
                Charisme = herosDeLaPartieModel.Heros.Charisme,
                Constitution = herosDeLaPartieModel.Heros.Constitution,
                Dexterite = herosDeLaPartieModel.Heros.Dexterite,
                Force = herosDeLaPartieModel.Heros.Force,
                Intelligence = herosDeLaPartieModel.Heros.Intelligence,
                Nom = herosDeLaPartieModel.Heros.Nom
            };
            _context.Personnes.Add(leHeros);
            await _context.SaveChangesAsync();
            //creation de la partie 
            Partie laPartie = new Partie
            {
                HistoireID = herosDeLaPartieModel.Histoire.HistoireID,
                UtilisateurID = herosDeLaPartieModel.Utilisateur.ApplicationUserID,
                PersonneID = leHeros.PersonneID
            };
            _context.Parties.Add(laPartie);
            await _context.SaveChangesAsync();
            //incrementation du nombre de fois où l histoire a été joué
           Histoire lhistoire= await _histoireRepository.GetHistoireByID(herosDeLaPartieModel.Histoire.HistoireID);
            lhistoire.NombreDeFoisJouee += 1;
            await _histoireRepository.UpdateHistoire(lhistoire);


            return laPartie;
        }
    }
}