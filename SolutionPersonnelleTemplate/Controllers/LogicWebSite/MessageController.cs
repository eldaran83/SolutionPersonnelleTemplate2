using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using SolutionPersonnelleTemplate.Data;
using SolutionPersonnelleTemplate.Models.BLL.Interfaces;
using SolutionPersonnelleTemplate.Models.BO;
using SolutionPersonnelleTemplate.Models.ViewModels;

namespace SolutionPersonnelleTemplate.Controllers.LogicWebSite
{
    public class MessageController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IRepositoryMessage _messageRepository;
        private readonly IHostingEnvironment _env;
        private readonly IRepositoryFichier _fichierRepository;

        public MessageController(ApplicationDbContext context, IRepositoryMessage messageRepository, IHostingEnvironment env, IRepositoryFichier fichierRepository)
        {
            _context = context;
            _messageRepository = messageRepository;
            _env = env;
            _fichierRepository = fichierRepository;
        }

        // GET: Message
        public async Task<IActionResult> Index(int? histoireID)
        {
            IEnumerable<Message> listeMessages = await _messageRepository.GetAllMessageOfStoryAsync(histoireID);
            ViewData["HistoireID"] = histoireID;
            return View(listeMessages.OrderByDescending(m => m.MessageID));
        }

        // GET: Message/Details/5
        public async Task<IActionResult> Details(int? messageId, int? HistoireID)
        {
            if (messageId == null)
            {
                return NotFound();
            }
            if (HistoireID == null)
            {
                return NotFound();
            }

            Message message = await _messageRepository.GetMessageByMessageIDAndHistoireId(messageId, HistoireID);
            if (message == null)
            {
                return NotFound();
            }
            ViewData["HistoireID"] = message.HistoireID;
            return View(message);
        }

        // GET: Message/Create
        public IActionResult Create(int histoireID)
        {
            ViewData["HistoireID"] = histoireID;
            return View();
        }

        // POST: Message/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MessageViewModel messageVM_Modele)
        {
            if (await _messageRepository.messageTitreExistDansCetteHistoire(messageVM_Modele.Message.Titre, messageVM_Modele.Message.HistoireID))
            {
                ViewBag.error = "Ce titre de message est déjà utilisé dans cette histoire";
                ViewData["HistoireID"] = messageVM_Modele.Message.HistoireID;
                return View(messageVM_Modele);
            }
            if (ModelState.IsValid)
            {
                Message leNouveauMessage = await _messageRepository.NouveauMessage(messageVM_Modele.Message);

                //////////////////////////////////////////////////////////////////////////////////////////
                //      GESTION de la photo
                //////////////////////////////////////////////////////////////////////////////////////////
                if (messageVM_Modele.Form.Files[0].FileName != "")
                {
                    string webRoot = _env.WebRootPath; // récupère l environnement
                    string nameDirectory = "/MessageFiles/"; // nomme le dossier dans lequel le média va se retrouver ici MessageFiles pour l image de histoire
                    string messageId = Convert.ToString(leNouveauMessage.MessageID); // sert à la personnalisation du dossier pour l utilisateur
                    string nomDuDossier = "/Image/"; // variable qui sert à nommer le dossier dans lequel le fichier sera ajouté, ICI c est le dossier Image

                    //Comme l utilisateur ne peut avoir qu'un seul avatar, on vérifie avant d'ajouter un fichier
                    //que le dossier n'a pas d autre image en supprimant tous les fichiers qui pourraient s y trouver
                    try
                    {
                        var sourceDir = Path.Combine(
                                    Directory.GetCurrentDirectory(), "wwwroot" + nameDirectory + messageId + nomDuDossier);

                        string[] listeImage = Directory.GetFiles(sourceDir);
                        // Copy picture files.          
                        foreach (string f in listeImage)
                        {
                            // Remove path from the file name.
                            string fName = f.Substring(sourceDir.Length);
                            _fichierRepository.RemoveFichierAvatar(sourceDir, fName);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }

                    //ajoute le fichier 
                    var imageURl = _fichierRepository.SaveFichierAvatar(webRoot, nameDirectory, messageId, nomDuDossier, messageVM_Modele.Form);
                    leNouveauMessage.UrlMedia = imageURl;

                    //je met à jour l message pour quelle est le nouveau lien de son image
                    await _messageRepository.UpdateMessage(leNouveauMessage);

                    ViewData["HistoireID"] = leNouveauMessage.HistoireID;
                    return RedirectToAction("Index", new RouteValueDictionary(new
                    {
                        controller = "Message",
                        action = "Index",
                        histoireID = leNouveauMessage.HistoireID

                    }));
                }

                //await _messageRepository.NouveauMessage(messageVM_Modele.Message);

                ViewData["HistoireID"] = messageVM_Modele.Message.HistoireID;
                return RedirectToAction("Index", new RouteValueDictionary(new
                {
                    controller = "Message",
                    action = "Index",
                    histoireID = messageVM_Modele.Message.HistoireID

                }));
            }

            ViewData["HistoireID"] = messageVM_Modele.Message.HistoireID;
            return View(messageVM_Modele);
            //    await _messageRepository.NouveauMessage(messageVM_Modele.Message);

            //    ViewData["HistoireID"] = messageVM_Modele.Message.HistoireID;
            //    return RedirectToAction("Index", new RouteValueDictionary(new
            //    {
            //        controller = "Message",
            //        action = "Index",
            //        histoireID = messageVM_Modele.Message.HistoireID
            //    }));
            //}
            //ViewData["HistoireID"] = messageVM_Modele.Message.HistoireID;
            //return View(messageVM_Modele);
        }

        // GET: Message/Edit/5
        public async Task<IActionResult> Edit(int? messageId, int? HistoireID)
        {
            if (messageId == null)
            {
                return NotFound();
            }
            if (HistoireID == null)
            {
                return NotFound();
            }

            Message message = await _messageRepository.GetMessageByMessageIDAndHistoireId(messageId, HistoireID);
            if (message == null)
            {
                return NotFound();
            }
            //////////////////////////////////////////////////////////////////////////////////////////
            //      GESTION de la photo
            //////////////////////////////////////////////////////////////////////////////////////////
            if (message.UrlMedia != null)
            {
                string img = message.UrlMedia.ToString();
                ViewBag.ImgPath = img;
            }
            else
            {
                ViewBag.ImgPath = "/images/story-media-default.jpg";
            }
            ///////////////////////////////////////////////////////////////////////
            //  FIN gestion image
            ///////////////////////////////////////////////////////////////////////
            MessageViewModel messageVM = new MessageViewModel
            {
                Message = message,
                Form = null
            };

            ViewData["HistoireID"] = message.HistoireID;
            return View(messageVM);
        }

        // POST: Message/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(MessageViewModel messageVM_Modele)
        {

            if (ModelState.IsValid)
            {
                //////////////////////////////////////////////////////////////////////////////////////////
                //      GESTION de la photo
                //////////////////////////////////////////////////////////////////////////////////////////
                if (messageVM_Modele.Form.Files[0].FileName != "")
                {
                    string webRoot = _env.WebRootPath; // récupère l environnement
                    string nameDirectory = "/MessageFiles/"; // nomme le dossier dans lequel le média va se retrouver ici MessageFiles pour l image de histoire
                    string messageId = Convert.ToString(messageVM_Modele.Message.MessageID); // sert à la personnalisation du dossier pour l utilisateur
                    string nomDuDossier = "/Image/"; // variable qui sert à nommer le dossier dans lequel le fichier sera ajouté, ICI c est le dossier Image

                    //Comme l utilisateur ne peut avoir qu'un seul avatar, on vérifie avant d'ajouter un fichier
                    //que le dossier n'a pas d autre image en supprimant tous les fichiers qui pourraient s y trouver
                    try
                    {
                        var sourceDir = Path.Combine(
                                    Directory.GetCurrentDirectory(), "wwwroot" + nameDirectory + messageId + nomDuDossier);

                        string[] listeImage = Directory.GetFiles(sourceDir);
                        // Copy picture files.          
                        foreach (string f in listeImage)
                        {
                            // Remove path from the file name.
                            string fName = f.Substring(sourceDir.Length);
                            _fichierRepository.RemoveFichierAvatar(sourceDir, fName);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }

                    //ajoute le fichier 
                    var imageURl = _fichierRepository.SaveFichierAvatar(webRoot, nameDirectory, messageId, nomDuDossier, messageVM_Modele.Form);
                    messageVM_Modele.Message.UrlMedia = imageURl;

                    //je met à jour l histoire pour quelle est le nouveau lien de son image
                    await _messageRepository.UpdateMessage(messageVM_Modele.Message);

                    ViewData["HistoireID"] = messageVM_Modele.Message.HistoireID;
                    return RedirectToAction("Index", new RouteValueDictionary(new
                    {
                        controller = "Message",
                        action = "Index",
                        histoireID = messageVM_Modele.Message.HistoireID

                }));
                }

                await _messageRepository.UpdateMessage(messageVM_Modele.Message);

                ViewData["HistoireID"] = messageVM_Modele.Message.HistoireID;
                return RedirectToAction("Index", new RouteValueDictionary(new
                {
                    controller = "Histoire",
                    action = "Index",
                    histoireID = messageVM_Modele.Message.HistoireID
                }));
            }

            ViewData["HistoireID"] = messageVM_Modele.Message.HistoireID;
            return View(messageVM_Modele);
        }
    


        // GET: Message/Delete/5
        public async Task<IActionResult> Delete(int? messageId, int? HistoireID)
        {
            if (messageId == null)
            {
                return NotFound();
            }
            if (HistoireID == null)
            {
                return NotFound();
            }

            Message leMessage = await _messageRepository.GetMessageByMessageIDAndHistoireId(messageId, HistoireID);
            if (leMessage == null)
            {
                return NotFound();
            }

            ViewData["HistoireID"] = HistoireID;
            return View(leMessage);
        }

        // POST: Message/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? messageId, int? HistoireID)
        {
            if (messageId == null)
            {
                return NotFound();
            }
            if (HistoireID == null)
            {
                return NotFound();
            }
            await _messageRepository.RemoveMessageOfStoryById(messageId, HistoireID);
            return RedirectToAction("Index", new RouteValueDictionary(new
            {
                controller = "Message",
                action = "Index",
                histoireID = HistoireID
            }));
        }

        private bool MessageExists(int id)
        {
            return _context.Messages.Any(e => e.MessageID == id);
        }
    }
}
