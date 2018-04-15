using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using SolutionPersonnelleTemplate.Data;
using SolutionPersonnelleTemplate.Models.BLL.Interfaces;
using SolutionPersonnelleTemplate.Models.BO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SolutionPersonnelleTemplate.Models.BLL.Managers
{
    public class MessageRepository : IRepositoryMessage
    {
        private readonly ApplicationDbContext _context;
        private readonly IRepositoryFichier _fichierRepository;
        private readonly IHostingEnvironment _env;
        /// <summary>
        /// contructeur 
        /// </summary>
        /// <param name="context"></param>
        public MessageRepository(ApplicationDbContext context, IRepositoryFichier fichierRepository, IHostingEnvironment env)
        {
            _context = context;
            _fichierRepository = fichierRepository;
            _env = env;
        }

        public async Task<bool> PeuplerLesMessagesDesHistoire()
        {
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Pour MESSAGE
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////

            //HISTOIRE 1
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////
            //On utilise cette facon de faire car sinon on ne peut pas ajouter l'ID 
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////
            using (var db = _context)
            using (var transaction = db.Database.BeginTransaction())
            {
                 //Message 
                Message message1Histoire1 = new Message
                {
                    MessageID = 1,
                    HistoireID = 1,
                    Titre = "Le commencement",
                    Contenu = "Le soleil se levait sur la prairie, et vous vous réveillez peu à peu en sentant l'herbe fraiche sous votre dos au milieu d'une clairière. Que faites vous ?",
                    NumeroMessageEnfant1 = 2,
                    NomAction1 = "Observer autour de soi",
                    NumeroMessageEnfant2 = 3,
                    NomAction2 = "Se lever",
                    NumeroMessageEnfant3 = 4,
                    NomAction3 = "Continuer à dormir"
                };
                _context.Add(message1Histoire1);
                //Message 2
                Message message2Histoire1 = new Message
                {
                    MessageID = 2,
                    HistoireID = 1,
                    Titre = "Aux aguets",
                    Contenu = "Alors que votre regard contemple la clairière tout autour de vous, vous sentez dans l'air une odeur de brulée et remarquez un panache noir face à vous. Que faites vous ?",
                    NumeroMessageEnfant1 = 5,
                    NomAction1 = "Aller vers la fumée",
                    NumeroMessageEnfant2 = 6,
                    NomAction2 = "Partir à l'opposé de la fumée",
                    NumeroMessageEnfant3 = 4,
                    NomAction3 = "Se recoucher pour dormir"
                };
                _context.Add(message2Histoire1);
                //Message 4
                Message message3Histoire1 = new Message
                {
                    MessageID = 3,
                    HistoireID = 1,
                    Titre = "Sur ses deux pieds",
                    Contenu = "Alors que vous vous levez, vous remarquez un panache noir face à vous, de toute évidence il s'agit du résultat d'un incendie. Que faites vous ?",
                    NumeroMessageEnfant1 = 5,
                    NomAction1 = "Aller vers la fumée",
                    NumeroMessageEnfant2 = 6,
                    NomAction2 = "Partir à l'opposé de la fumée",
                    NumeroMessageEnfant3 = 4,
                    NomAction3 = "Se recoucher pour dormir"
                };
                _context.Add(message3Histoire1);
                //Message 4
                Message message4Histoire1 = new Message
                {
                    MessageID = 4,
                    HistoireID = 1,
                    Titre = "Le monde appartient à ceux qui se lévent tôt",
                    Contenu = "Faisant fi de tout ce qui pourrait se passer autour de vous, vous vous laissez attirer par les chaleureux et puissants bras de Morphée. Vous ressentez bien de façon de plus en plus cuisante une odeur de fumée mais votre coprs de vous répondu plus. Votre histoire s'arrête ici Héros ! ."
                };
                _context.Add(message4Histoire1);
                //Message 5
                Message message5Histoire1 = new Message
                {
                    MessageID = 5,
                    HistoireID = 1,
                    Titre = "Petit curieux",
                    Contenu = "Vous sortez de la clairière et vous entrez dans la forêt qui l'enserre. La fumée se fait de plus en plus dense rendant votre vison moins précise, et alors que vous êtes sur le point de vous demander s'il est bien prudent de continuer votre chemin, vous entendez un immense craquement sur votre flanc droit. Vous avez juste le temps d'apercevoir un tronc d'arbre en partie calciné vous frapper à la tête au moment où vous vous tournez vers ce son.",
                    NumeroMessageEnfant1 = 4,
                    NomAction1 = "Entre deux eaux"
                };
                _context.Add(message5Histoire1);
                //Message 6
                Message message6Histoire1 = new Message
                {
                    MessageID = 6,
                    HistoireID = 1,
                    Titre = "La prudence à parfois du bon",
                    Contenu = "Sans autres équipement sur vous et ne vous souvenant plus de rien, vous vous dites qu'il n'est peut être pas déjà temps de vous lancer dans des actions risquées.Faisant acte de prudence, vos pieds se tournent à l'opposé d'un potentiel danger, remarquant un petit sentier au travers de la forêt vous l'empruntez jusqu'à ne plus sentir cette odeur de fumée que loin derrière vous. Après une heure de marche, vos pas vous guide à l'entrée d'un petit village qui vous semblez habité. Que faites-vous ?.",
                    NumeroMessageEnfant1 = 7,
                    NomAction1 = "Chaque chose en son temps"
                };
                _context.Add(message6Histoire1);
                //Message 8
                Message message7Histoire1 = new Message
                {
                    MessageID = 7,
                    HistoireID = 1,
                    Titre = "Chaque chose en son temps",
                    Contenu = "La suite de l'aventure arrivera plus tard, merci d'y avoir participé."
                };
                _context.Add(message7Histoire1);

                //HISTOIRE 2
                //Message 1
                //Message message2Histoire2 = new Message
                //{
                //    MessageID = 2,
                //    HistoireID = 2,
                //    Titre = "Le commencement",
                //    Contenu = "Aujourd’hui, comme tous les lundis, dès la sortie de la classe, je cours vers le parc avec mes copains pour y retrouver monsieur Charles. J’essaye d’arriver le premier pour pouvoir choisir l’histoire qu’il va nous conter. Moi, je choisis toujours des histoires de détectives... Vite je dois me dépêcher.. Que faites vous ?",
                //    NumeroMessageEnfant1 = 3,
                //    NomAction1 = "Continuer vers le parc"
                //};
                //_context.Add(message2Histoire2);
                ////Message 3
                //Message message3Histoire2 = new Message
                //{
                //    MessageID = 3,
                //    HistoireID = 2,
                //    Titre = "Le parc",
                //    Contenu = "Nous voilà arrivés au parc. On repère toujours monsieur Charles de loin grâce à son grand panier rouge. Mais ce soir, le banc vert sur lequel monsieur Charles s’assoit est vide. Nous partons à sa recherche dans le parc.. Que faites vous ?",
                //    NumeroMessageEnfant1 = 4,
                //    NomAction1 = "Nous partons vers le grand arbre",
                //    NumeroMessageEnfant2 = 5,
                //    NomAction2 = "Nous partons vers le petit ruisseau",
                //    NumeroMessageEnfant3 = 7,
                //    NomAction3 = "Nous partons vers l’entrée du parc"
                //};
                //_context.Add(message3Histoire2);
                ////Message 4
                //Message message4Histoire2 = new Message
                //{
                //    MessageID = 4,
                //    HistoireID = 2,
                //    Titre = "Le grand arbre",
                //    Contenu = "Nous avons beau chercher, mais nous ne le trouvons toujours pas. Nous décidons de retourner vers le banc vert",
                //    NumeroMessageEnfant1 = 6,
                //    NomAction1 = "Vers le banc"
                //};
                //_context.Add(message4Histoire2);
                ////Message 5
                //Message message5Histoire2 = new Message
                //{
                //    MessageID = 5,
                //    HistoireID = 2,
                //    Titre = "Le petit ruisseau",
                //    Contenu = "Il n’y a personne du côté du petit ruisseau. Nous décidons de retourner vers le banc vert.Pour aller plus vite nous décidons de revenir par le petit talus.",
                //    NumeroMessageEnfant1 = 6,
                //    NomAction1 = "Passer le talus"
                //};
                //_context.Add(message5Histoire2);
                ////Message 6
                //Message message6Histoire2 = new Message
                //{
                //    MessageID = 6,
                //    HistoireID = 2,
                //    Titre = "Le banc",
                //    Contenu = "Nous nous asseyons sur le banc et attendons un peu. Mais personne ne vient. Pour la première fois, nous n’aurons pas d’histoire...",
                //    NumeroMessageEnfant1 = 7,
                //    NomAction1 = "La suite"
                //};
                //_context.Add(message6Histoire2);
                ////Message 7
                //Message message7Histoire2 = new Message
                //{
                //    MessageID = 7,
                //    HistoireID = 2,
                //    Titre = "L'entrée du parc",
                //    Contenu = "Nous nous dirigeons vers l'entrée du parc.Mais personne ne vient. Pour la première fois, nous n’aurons pas d’histoire... Merci d'avoir suivi cette aventure, la suite arrive très vite"
                //};
                //_context.Add(message7Histoire2);

                //Facon (IDENTITY_INSERT) pour pouvoir SET soit meme l'ID
                db.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.Messages ON;");
                db.SaveChanges();
                db.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.Messages OFF");
                transaction.Commit();
            }
            return true;
        }
        /// <summary>
        /// renvoi tous les messages d'un histoire
        /// </summary>
        /// <param name="histoireID"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Message>> GetAllMessageOfStoryAsync(int? histoireID)
        {
            IEnumerable<Message> lesMessagesDeLHistoire = await _context.Messages
                                                                .Include(h => h.Histoire)
                                                                .Where(m => m.HistoireID == histoireID)
                                                                .OrderBy(m=>m.MessageID).ToListAsync();
             return lesMessagesDeLHistoire;
        }

        /// <summary>
        /// recupere le message ne fonction de son id et de l id de l histoire
        /// </summary>
        /// <param name="messageId"></param>
        /// <param name="HistoireID"></param>
        /// <returns></returns>
        public async Task<Message> GetMessageByMessageIDAndHistoireId(int? messageId, int? histoireID)
        {
            Message leMessage = await _context.Messages
                                 .Include(h=>h.Histoire)
                                 .Where(m => m.MessageID == messageId)
                                 .Where(m => m.HistoireID == histoireID)
                                 .FirstOrDefaultAsync();
            return leMessage;
        }

        /// <summary>
        /// vérifie si le titre du message existe deja ou pas en bdd
        /// </summary>
        /// <param name="titreMessage"></param>
        /// <returns></returns>
        public async Task<bool> messageTitreExistDansCetteHistoire(string titreMessage, int HistoireID)
        {
            Message titreExist = await _context.Messages
                .Where(m => m.Titre.ToUpper().Trim() == titreMessage.ToUpper().Trim())
                .Where(m=>m.HistoireID == HistoireID)
                .FirstOrDefaultAsync();
            if (titreExist != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// ajoute en bdd un message
        /// </summary>
        /// <param name="messageModele"></param>
        /// <returns></returns>
        public async Task<Message> NouveauMessage(Message messageModele)
        {
            Message messageAAjouter = new Message
            {
                 Titre = messageModele.Titre,
                Contenu = messageModele.Contenu,
                HistoireID = messageModele.HistoireID,
                NumeroMessageEnfant1 = messageModele.NumeroMessageEnfant1,
                NomAction1 = messageModele.NomAction1,
                NumeroMessageEnfant2 = messageModele.NumeroMessageEnfant2,
                NomAction2 = messageModele.NomAction2,
                NumeroMessageEnfant3 = messageModele.NumeroMessageEnfant3,
                NomAction3 = messageModele.NomAction3,
                UrlMedia = "/images/message-media-default.jpg"
            };
            _context.Messages.Add(messageAAjouter);
            await _context.SaveChangesAsync();

            return messageAAjouter;
 
        }

        /// <summary>
        /// supprime un message de la bdd lié a une histoire
        /// </summary>
        /// <param name="messageId"></param>
        /// <param name="histoireId"></param>
        /// <returns></returns>
        public async Task<bool> RemoveMessageOfStoryById(int? messageId, int? histoireId)
        {
            //Le message doit etre supprimé en dernier  !!

                //Il faut supprimer ici les médias de ce message par la suite 
                 string webRoot = _env.WebRootPath; // récupère l environnement
                string nameDirectory = "/StoryFiles/"; // nomme le dossier dans lequel le média va se retrouver ici MessageFiles pour l image de histoire
                string lemessageId = Convert.ToString(messageId); // sert à la personnalisation du dossier pour l utilisateur
                string nomDuDossier = "/ImageHistoire/" + Convert.ToString(histoireId) + "_Histoire/Messages/"; // variable qui sert à nommer le dossier dans lequel le fichier sera ajouté, ICI c est le dossier Image

                //Comme l utilisateur ne peut avoir qu'un seul avatar, on vérifie avant d'ajouter un fichier
                //que le dossier n'a pas d autre image en supprimant tous les fichiers qui pourraient s y trouver
                try
                {
                    var sourceDir = Path.Combine(
                                Directory.GetCurrentDirectory(), "wwwroot" + nameDirectory + nomDuDossier + lemessageId);

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

            try {
                //Supprime le message 
                Message leMessage = await _context.Messages
                                         .Where(m => m.MessageID == messageId)
                                        .Where(m => m.HistoireID == histoireId)
                                        .FirstOrDefaultAsync();

                _context.Messages.Remove(leMessage);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("La supression du message a rencontré un problème :" + ex);
                return false;
            }
        }
    

        /// <summary>
        /// met a jour un message en BDD
        /// </summary>
        /// <param name="messageModele"></param>
        /// <returns></returns>
        public async Task<Message> UpdateMessage(Message messageModele)
        {
            try
            {
                _context.Update(messageModele);
                await _context.SaveChangesAsync();
                return messageModele;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                Console.WriteLine("La MAJ du message a rencontré un problème :" + ex);
                return messageModele;
            }
        }
    }
}
