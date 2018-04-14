using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
            Histoire histoire = await _context.Histoires.Where(h => h.HistoireID == histoireID).FirstOrDefaultAsync();
            ViewBag.TitreHistoire = histoire.Titre;
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
        public async Task<IActionResult> Create(int histoireID)
        {
          IEnumerable<Message> dropDownListActions= await  _messageRepository.GetAllMessageOfStoryAsync(Convert.ToInt16(histoireID));

            if (dropDownListActions.Count() == 0)
            {
                Message leMessageDeSelection = new Message
                {
                    MessageID = 1,
                    Titre = "--Sélectionnez une action--",
                    Contenu = "Vous avez la possibilité de choisir une action que pourra effectuer le héros au cours de l'aventure",
                    HistoireID = histoireID
                };
               await _messageRepository.NouveauMessage(leMessageDeSelection);
            }
            ViewBag.NumeroMessageEnfant1 = new SelectList(dropDownListActions, "MessageID", "Titre");
            ViewBag.NumeroMessageEnfant2 = new SelectList(dropDownListActions, "MessageID", "Titre");
            ViewBag.NumeroMessageEnfant3 = new SelectList(dropDownListActions, "MessageID", "Titre");

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
            var dropDownListActions = await _messageRepository.GetAllMessageOfStoryAsync(Convert.ToInt16(messageVM_Modele.Message.HistoireID));

            if (await _messageRepository.messageTitreExistDansCetteHistoire(messageVM_Modele.Message.Titre, messageVM_Modele.Message.HistoireID))
            {
                ViewBag.NumeroMessageEnfant1 = new SelectList(dropDownListActions, "MessageID", "Titre");
                ViewBag.NumeroMessageEnfant2 = new SelectList(dropDownListActions, "MessageID", "Titre");
                ViewBag.NumeroMessageEnfant3 = new SelectList(dropDownListActions, "MessageID", "Titre");
                ViewBag.error = "Ce titre de message est déjà utilisé dans cette histoire";
                ViewData["HistoireID"] = messageVM_Modele.Message.HistoireID;
                return View(messageVM_Modele);
            }

            //Logic Liens Actions Messages
            bool erreurLiensActionsMessage = false; 
            //1
            if (messageVM_Modele.Message.NumeroMessageEnfant1 != 1) // un N° message a été saisi dans la dropdownList autre que l'id 1 qui est celui par défaut
            {
                if (messageVM_Modele.Message.NomAction1 == null)// vérifie qu'un message pour l action a été saisi
                {
                    ViewBag.errorNomAction1 = "Vous avez choisi un message d'action sans avoir donné un nom à cette action !";
                    erreurLiensActionsMessage = true;
                }

            }
            if (messageVM_Modele.Message.NomAction1 != null) // vérifie qu'un message pour l action a été saisi
            {
                if (messageVM_Modele.Message.NumeroMessageEnfant1 == 1) // un message a été saisi dans la dropdownList autre que l'id 1 qui est celui par défaut
                {
                    ViewBag.errorNumeroAction1 = "Vous avez rempli une action sans avoir choisi dans la liste un message de destination !";
                    erreurLiensActionsMessage = true;
                }
            }

            //2
            if (messageVM_Modele.Message.NumeroMessageEnfant2 != 1) // un N° message a été saisi dans la dropdownList autre que l'id 1 qui est celui par défaut
            {
                if (messageVM_Modele.Message.NomAction2 == null)// vérifie qu'un message pour l action a été saisi
                {
                    ViewBag.errorNomAction2 = "Vous avez choisi un message d'action sans avoir donné un nom à cette action !";
                    erreurLiensActionsMessage = true;
                }

            }
            if (messageVM_Modele.Message.NomAction2 != null) // vérifie qu'un message pour l action a été saisi
            {
                if (messageVM_Modele.Message.NumeroMessageEnfant2 == 1) // un message a été saisi dans la dropdownList autre que l'id 1 qui est celui par défaut
                {
                    ViewBag.errorNumeroAction2 = "Vous avez rempli une action sans avoir choisi dans la liste un message de destination !";
                    erreurLiensActionsMessage = true;
                }
            }
            //3
            if (messageVM_Modele.Message.NumeroMessageEnfant3 != 1) // un N° message a été saisi dans la dropdownList autre que l'id 1 qui est celui par défaut
            {
                if (messageVM_Modele.Message.NomAction3 == null)// vérifie qu'un message pour l action a été saisi
                {
                    ViewBag.errorNomAction3 = "Vous avez choisi un message d'action sans avoir donné un nom à cette action !";
                    erreurLiensActionsMessage = true;
                }

            }
            if (messageVM_Modele.Message.NomAction3 != null) // vérifie qu'un message pour l action a été saisi
            {
                if (messageVM_Modele.Message.NumeroMessageEnfant3 == 1) // un message a été saisi dans la dropdownList autre que l'id 1 qui est celui par défaut
                {
                    ViewBag.errorNumeroAction3 = "Vous avez rempli une action sans avoir choisi dans la liste un message de destination !";
                    erreurLiensActionsMessage = true;
                }
            }

            if (erreurLiensActionsMessage) // il y a un problème avec les liens d action dans le message créé
            {
                //logic des liens vers les autres messages 
                 ViewBag.NumeroMessageEnfant1 = new SelectList(dropDownListActions, "MessageID", "Titre");
                ViewBag.NumeroMessageEnfant2 = new SelectList(dropDownListActions, "MessageID", "Titre");
                ViewBag.NumeroMessageEnfant3 = new SelectList(dropDownListActions, "MessageID", "Titre");
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
            //logic des liens vers les autres messages 
            ViewBag.NumeroMessageEnfant1 = new SelectList(dropDownListActions, "MessageID", "Titre");
            ViewBag.NumeroMessageEnfant2 = new SelectList(dropDownListActions, "MessageID", "Titre");
            ViewBag.NumeroMessageEnfant3 = new SelectList(dropDownListActions, "MessageID", "Titre");
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
            //MessageViewModel messageVM = new MessageViewModel
            //{
            //    Message = message,
            //    Form = null
            //};
            
            //logic des liens vers les autres messages 
            var dropDownListActions = await _messageRepository.GetAllMessageOfStoryAsync(Convert.ToInt16(message.HistoireID));
            ViewData["NumeroMessageEnfant1"] = new SelectList(dropDownListActions, "MessageID", "Titre", message.NumeroMessageEnfant1);
            //ViewBag.NumeroMessageEnfant1 = new SelectList(dropDownListActions, "MessageID", "Titre", message.NumeroMessageEnfant1);
            ViewData["NumeroMessageEnfant2"] = new SelectList(dropDownListActions, "MessageID", "Titre", message.NumeroMessageEnfant2);
            //ViewBag.NumeroMessageEnfant2 = new SelectList(dropDownListActions, "MessageID", "Titre", message.NumeroMessageEnfant2);
            ViewData["NumeroMessageEnfant3"] = new SelectList(dropDownListActions, "MessageID", "Titre", message.NumeroMessageEnfant3);
            //ViewBag.NumeroMessageEnfant3 = new SelectList(dropDownListActions, "MessageID", "Titre", message.NumeroMessageEnfant3);
            ViewData["HistoireID"] = message.HistoireID;
            // return View(messageVM);
            return View(message);
        }


        // POST: Message/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Message messageVM_Modele, IFormCollection form)
        {            
            string imageDejaLa = "";
            try
            {
                imageDejaLa = (from ppl in _context.Messages
                                 .Where(m => m.MessageID == messageVM_Modele.MessageID)
                                 .Where(h => h.HistoireID == messageVM_Modele.HistoireID)
                                      select ppl.UrlMedia).FirstOrDefault().ToString();
                messageVM_Modele.UrlMedia = imageDejaLa;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Le message ne possede pas encore d'image " + ex);
                imageDejaLa = "";
                messageVM_Modele.UrlMedia = imageDejaLa;
            }

            //Logic Liens Actions Messages
            bool erreurLiensActionsMessage = false;
            //1
            if (messageVM_Modele.NumeroMessageEnfant1 != 1) // un N° message a été saisi dans la dropdownList autre que l'id 1 qui est celui par défaut
            {
                if (messageVM_Modele.NomAction1 == null)// vérifie qu'un message pour l action a été saisi
                {
                    ViewBag.errorNomAction1 = "Vous avez choisi un message d'action sans avoir donné un nom à cette action !";
                    erreurLiensActionsMessage = true;
                }

            }
            if (messageVM_Modele.NomAction1 != null) // vérifie qu'un message pour l action a été saisi
            {
                if (messageVM_Modele.NumeroMessageEnfant1 == 1) // un message a été saisi dans la dropdownList autre que l'id 1 qui est celui par défaut
                {
                    ViewBag.errorNumeroAction1 = "Vous avez rempli une action sans avoir choisi dans la liste un message de destination !";
                    erreurLiensActionsMessage = true;
                }
            }
            //2
            if (messageVM_Modele.NumeroMessageEnfant2 != 1) // un N° message a été saisi dans la dropdownList autre que l'id 1 qui est celui par défaut
            {
                if (messageVM_Modele.NomAction2 == null)// vérifie qu'un message pour l action a été saisi
                {
                    ViewBag.errorNomAction2 = "Vous avez choisi un message d'action sans avoir donné un nom à cette action !";
                    erreurLiensActionsMessage = true;
                }
            }
            if (messageVM_Modele.NomAction2 != null) // vérifie qu'un message pour l action a été saisi
            {
                if (messageVM_Modele.NumeroMessageEnfant2 == 1) // un message a été saisi dans la dropdownList autre que l'id 1 qui est celui par défaut
                {
                    ViewBag.errorNumeroAction2 = "Vous avez rempli une action sans avoir choisi dans la liste un message de destination !";
                    erreurLiensActionsMessage = true;
                }
            }
            //3
            if (messageVM_Modele.NumeroMessageEnfant3 != 1) // un N° message a été saisi dans la dropdownList autre que l'id 1 qui est celui par défaut
            {
                if (messageVM_Modele.NomAction3 == null)// vérifie qu'un message pour l action a été saisi
                {
                    ViewBag.errorNomAction3 = "Vous avez choisi un message d'action sans avoir donné un nom à cette action !";
                    erreurLiensActionsMessage = true;
                }
            }
            if (messageVM_Modele.NomAction3 != null) // vérifie qu'un message pour l action a été saisi
            {
                if (messageVM_Modele.NumeroMessageEnfant3 == 1) // un message a été saisi dans la dropdownList autre que l'id 1 qui est celui par défaut
                {
                    ViewBag.errorNumeroAction3 = "Vous avez rempli une action sans avoir choisi dans la liste un message de destination !";
                    erreurLiensActionsMessage = true;
                }
            }
            if (erreurLiensActionsMessage) // il y a un problème avec les liens d action dans le message créé
            {
                //logic des liens vers les autres messages 
                //pour une raison que je ne comprend pas si je fais appel ici pour les DDL de la methode _messageRepository.GetAllMessageOfStoryAsync(histoireID);
                //j'ai une erreur par la suite : The instance of entity type 'Message' cannot be tracked because another instance with the same key value for {'MessageID'} is already being tracked. When attaching existing entities, ensure that only one entity instance with a given key value is attached. Consider using 'DbContextOptionsBuilder.EnableSensitiveDataLogging' to see the conflicting key values.

                ViewBag.NumeroMessageEnfant1 = new SelectList(_context.Messages.Where(h=>h.HistoireID == messageVM_Modele.HistoireID).ToList(), "MessageID", "Titre", messageVM_Modele.NumeroMessageEnfant1);
                ViewBag.NumeroMessageEnfant2 = new SelectList(_context.Messages.Where(h => h.HistoireID == messageVM_Modele.HistoireID).ToList(), "MessageID", "Titre", messageVM_Modele.NumeroMessageEnfant2);
                ViewBag.NumeroMessageEnfant3 = new SelectList(_context.Messages.Where(h => h.HistoireID == messageVM_Modele.HistoireID).ToList(), "MessageID", "Titre", messageVM_Modele.NumeroMessageEnfant3);

                //////////////////////////////////////////////////////////////////////////////////////////
                //      GESTION de la photo
                //////////////////////////////////////////////////////////////////////////////////////////
                if (messageVM_Modele.UrlMedia != null)
                {
                    string img = messageVM_Modele.UrlMedia.ToString();
                    ViewBag.ImgPath = img;
                }
                else
                {
                    ViewBag.ImgPath = "/images/story-media-default.jpg";
                }
                ///////////////////////////////////////////////////////////////////////
                //  FIN gestion image
                ///////////////////////////////////////////////////////////////////////
                return View(messageVM_Modele);
            }
            if (ModelState.IsValid)
            {
                //////////////////////////////////////////////////////////////////////////////////////////
                //      GESTION de la photo
                //////////////////////////////////////////////////////////////////////////////////////////
                if (form.Files[0].FileName != "")
                {
                    if (form.Files[0].Length >= 5242880) //Maxi 5 mo pour l image
                    {
                        ViewBag.errorFichier = "L'image doit avoir une taille inférieure à 5 Mo.";
                    }
                    string webRoot = _env.WebRootPath; // récupère l environnement
                    string nameDirectory = "/StoryFiles/"; // nomme le dossier dans lequel le média va se retrouver ici MessageFiles pour l image de histoire
                    string messageId = Convert.ToString(messageVM_Modele.MessageID); // sert à la personnalisation du dossier pour l utilisateur
                    string nomDuDossier = "/ImageHistoire/" + messageVM_Modele.HistoireID + "_Histoire/Messages/"; // variable qui sert à nommer le dossier dans lequel le fichier sera ajouté, ICI c est le dossier Image

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
                    var imageURl = _fichierRepository.SaveFichier(webRoot, nameDirectory, nomDuDossier, messageId, form);
                    messageVM_Modele.UrlMedia = imageURl;

                    //je met à jour l histoire pour quelle est le nouveau lien de son image
                    await _messageRepository.UpdateMessage(messageVM_Modele);

                    ViewData["HistoireID"] = messageVM_Modele.HistoireID;
                    return RedirectToAction("Index", new RouteValueDictionary(new
                    {
                        controller = "Message",
                        action = "Index",
                        histoireID = messageVM_Modele.HistoireID
                    }));
                }
                await _messageRepository.UpdateMessage(messageVM_Modele);

                ViewData["HistoireID"] = messageVM_Modele.HistoireID;
                return RedirectToAction("Index", new RouteValueDictionary(new
                {
                    controller = "Message",
                    action = "Index",
                    histoireID = messageVM_Modele.HistoireID

                }));
            }

             var dropDownListActions = await _messageRepository.GetAllMessageOfStoryAsync(Convert.ToInt16(messageVM_Modele.HistoireID));
            ViewBag.NumeroMessageEnfant1 = new SelectList(dropDownListActions, "MessageID", "Titre", messageVM_Modele.NumeroMessageEnfant1);
            ViewBag.NumeroMessageEnfant2 = new SelectList(dropDownListActions, "MessageID", "Titre", messageVM_Modele.NumeroMessageEnfant2);
            ViewBag.NumeroMessageEnfant3 = new SelectList(dropDownListActions, "MessageID", "Titre", messageVM_Modele.NumeroMessageEnfant3);

            ViewData["HistoireID"] = messageVM_Modele.HistoireID;
            return View(messageVM_Modele);
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
