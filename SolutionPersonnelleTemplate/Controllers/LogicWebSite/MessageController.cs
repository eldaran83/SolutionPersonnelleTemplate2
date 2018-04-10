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

            //logic des liens vers les autres messages 

            //pour le lien1
            if (messageVM_Modele.Message.NumeroMessageEnfant1 != null && messageVM_Modele.Message.NumeroMessageEnfant1 >0)
            {
                if (messageVM_Modele.Message.MessageEnfant1 == null)
                {
                    ViewBag.MessageEnfant1 = "vous avez renseigné un N° de destination sans renseigner un titre pour celui-ci";
                    ViewData["HistoireID"] = messageVM_Modele.Message.HistoireID;
                    return View(messageVM_Modele);
                }
            }

            if (messageVM_Modele.Message.MessageEnfant1 != null)
            {
                if (messageVM_Modele.Message.NumeroMessageEnfant1 == null )
                {
                    ViewBag.NumeroMessageEnfant1 = "vous avez renseigné un libellé sans renseigner un N° pour celui-ci";
                    ViewData["HistoireID"] = messageVM_Modele.Message.HistoireID;
                    return View(messageVM_Modele);
                }
            }

            //pour le lien2
            if (messageVM_Modele.Message.NumeroMessageEnfant2 != null && messageVM_Modele.Message.NumeroMessageEnfant2 > 0)
            {
                if (messageVM_Modele.Message.MessageEnfant2 == null)
                {
                    ViewBag.MessageEnfant2 = "vous avez renseigné un N° de destination sans renseigner un titre pour celui-ci";
                    ViewData["HistoireID"] = messageVM_Modele.Message.HistoireID;
                    return View(messageVM_Modele);
                }
            }

            if (messageVM_Modele.Message.MessageEnfant2 != null)
            {
                if (messageVM_Modele.Message.NumeroMessageEnfant2 == null )
                {
                    ViewBag.NumeroMessageEnfant2 = "vous avez renseigné un libellé sans renseigner un N° pour celui-ci";
                    ViewData["HistoireID"] = messageVM_Modele.Message.HistoireID;
                    return View(messageVM_Modele);
                }
            }


            //pour le lien3
            if (messageVM_Modele.Message.NumeroMessageEnfant3 != null && messageVM_Modele.Message.NumeroMessageEnfant3 > 0)
            {
                if (messageVM_Modele.Message.MessageEnfant3 == null)
                {
                    ViewBag.MessageEnfant3 = "vous avez renseigné un N° de destination sans renseigner un titre pour celui-ci";
                    ViewData["HistoireID"] = messageVM_Modele.Message.HistoireID;
                    return View(messageVM_Modele);
                }
            }

            if (messageVM_Modele.Message.MessageEnfant3 != null)
            {
                if (messageVM_Modele.Message.NumeroMessageEnfant3 == null)
                {
                    ViewBag.NumeroMessageEnfant3 = "vous avez renseigné un libellé sans renseigner un N° pour celui-ci";
                    ViewData["HistoireID"] = messageVM_Modele.Message.HistoireID;
                    return View(messageVM_Modele);
                }
            }

            if (ModelState.IsValid)
            {
                Message leNouveauMessage = await _messageRepository.NouveauMessage(messageVM_Modele.Message);

                //////////////////////////////////////////////////////////////////////////////////////////
                //      GESTION de la photo
                //////////////////////////////////////////////////////////////////////////////////////////
                if (messageVM_Modele.Form.Files[0].FileName != "")
                {
                    if (messageVM_Modele.Form.Files[0].Length >= 5242880) //Maxi 5 mo pour l image
                    {
                        ViewBag.errorFichier = "L'image doit avoir une taille inférieure à 5 Mo.";
                    }

                    string webRoot = _env.WebRootPath; // récupère l environnement
                    string nameDirectory = "/StoryFiles/"; // nomme le dossier dans lequel le média va se retrouver ici MessageFiles pour l image de histoire
                    //  //ATTENTION Cest une nouvelle histoire donc leNouveauMessage
                    string messageId = Convert.ToString(leNouveauMessage.MessageID); // sert à la personnalisation du dossier pour l utilisateur
                    string nomDuDossier = "/ImageHistoire/" + messageVM_Modele.Message.HistoireID + "_Histoire/Messages/"; // variable qui sert à nommer le dossier dans lequel le fichier sera ajouté, ICI c est le dossier Image

                    //Comme l utilisateur ne peut avoir qu'un seul avatar, on vérifie avant d'ajouter un fichier
                    //que le dossier n'a pas d autre image en supprimant tous les fichiers qui pourraient s y trouver
                    try
                    {
                        var sourceDir = Path.Combine(
                                    Directory.GetCurrentDirectory(), "wwwroot" + nameDirectory + nomDuDossier +  messageId);

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
                    var imageURl = _fichierRepository.SaveFichier(webRoot, nameDirectory, nomDuDossier, messageId, messageVM_Modele.Form);
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
            //logic des liens vers les autres messages 

            //pour le lien1
            if (messageVM_Modele.Message.NumeroMessageEnfant1 != null && messageVM_Modele.Message.NumeroMessageEnfant1 > 0)
            {
                if (messageVM_Modele.Message.MessageEnfant1 == null)
                {
                    ViewBag.MessageEnfant1 = "vous avez renseigné un N° de destination sans renseigner un titre pour celui-ci";
                    ViewData["HistoireID"] = messageVM_Modele.Message.HistoireID;
                    return View(messageVM_Modele);
                }
            }

            if (messageVM_Modele.Message.MessageEnfant1 != null)
            {
                if (messageVM_Modele.Message.NumeroMessageEnfant1 == null)
                {
                    ViewBag.NumeroMessageEnfant1 = "vous avez renseigné un libellé sans renseigner un N° pour celui-ci";
                    ViewData["HistoireID"] = messageVM_Modele.Message.HistoireID;
                    return View(messageVM_Modele);
                }
            }

            //pour le lien2
            if (messageVM_Modele.Message.NumeroMessageEnfant2 != null && messageVM_Modele.Message.NumeroMessageEnfant2 > 0)
            {
                if (messageVM_Modele.Message.MessageEnfant2 == null)
                {
                    ViewBag.MessageEnfant2 = "vous avez renseigné un N° de destination sans renseigner un titre pour celui-ci";
                    ViewData["HistoireID"] = messageVM_Modele.Message.HistoireID;
                    return View(messageVM_Modele);
                }
            }

            if (messageVM_Modele.Message.MessageEnfant2 != null)
            {
                if (messageVM_Modele.Message.NumeroMessageEnfant2 == null)
                {
                    ViewBag.NumeroMessageEnfant2 = "vous avez renseigné un libellé sans renseigner un N° pour celui-ci";
                    ViewData["HistoireID"] = messageVM_Modele.Message.HistoireID;
                    return View(messageVM_Modele);
                }
            }


            //pour le lien3
            if (messageVM_Modele.Message.NumeroMessageEnfant3 != null && messageVM_Modele.Message.NumeroMessageEnfant3 > 0)
            {
                if (messageVM_Modele.Message.MessageEnfant3 == null)
                {
                    ViewBag.MessageEnfant3 = "vous avez renseigné un N° de destination sans renseigner un titre pour celui-ci";
                    ViewData["HistoireID"] = messageVM_Modele.Message.HistoireID;
                    return View(messageVM_Modele);
                }
            }

            if (messageVM_Modele.Message.MessageEnfant3 != null)
            {
                if (messageVM_Modele.Message.NumeroMessageEnfant3 == null)
                {
                    ViewBag.NumeroMessageEnfant3 = "vous avez renseigné un libellé sans renseigner un N° pour celui-ci";
                    ViewData["HistoireID"] = messageVM_Modele.Message.HistoireID;
                    return View(messageVM_Modele);
                }
            }

            if (ModelState.IsValid)
            {
                //////////////////////////////////////////////////////////////////////////////////////////
                //      GESTION de la photo
                //////////////////////////////////////////////////////////////////////////////////////////
                if (messageVM_Modele.Form.Files[0].FileName != "")
                {
                    if (messageVM_Modele.Form.Files[0].Length >= 5242880) //Maxi 5 mo pour l image
                    {
                        ViewBag.error = "L'image doit avoir une taille inférieure à 5 Mo.";
                    }
                    string webRoot = _env.WebRootPath; // récupère l environnement
                    string nameDirectory = "/StoryFiles/"; // nomme le dossier dans lequel le média va se retrouver ici MessageFiles pour l image de histoire
                    string messageId = Convert.ToString(messageVM_Modele.Message.MessageID); // sert à la personnalisation du dossier pour l utilisateur
                    string nomDuDossier = "/ImageHistoire/" + messageVM_Modele.Message.HistoireID + "_Histoire/Messages/"; // variable qui sert à nommer le dossier dans lequel le fichier sera ajouté, ICI c est le dossier Image
                    
                    //Comme l utilisateur ne peut avoir qu'un seul avatar, on vérifie avant d'ajouter un fichier
                    //que le dossier n'a pas d autre image en supprimant tous les fichiers qui pourraient s y trouver
                    try
                    {
                        var sourceDir = Path.Combine(
                                    Directory.GetCurrentDirectory(), "wwwroot" + nameDirectory + nomDuDossier + messageId);

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
                    var imageURl = _fichierRepository.SaveFichier(webRoot, nameDirectory, nomDuDossier , messageId, messageVM_Modele.Form);
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
            //////////////////////////////////////////////////////////////////////////////////////////
            //      GESTION de la photo
            //////////////////////////////////////////////////////////////////////////////////////////
            if (leMessage.UrlMedia != null)
            {
                string img = leMessage.UrlMedia.ToString();
                ViewBag.ImgPath = img;
            }
            else
            {
                ViewBag.ImgPath = "/images/story-media-default.jpg";
            }
            ///////////////////////////////////////////////////////////////////////
            //  FIN gestion image
            ///////////////////////////////////////////////////////////////////////
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
