using Microsoft.EntityFrameworkCore;
using SolutionPersonnelleTemplate.Data;
using SolutionPersonnelleTemplate.Models.BLL.Interfaces;
using SolutionPersonnelleTemplate.Models.BO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SolutionPersonnelleTemplate.Models.BLL.Managers
{
    public class HistoireRepository : IRepositoryHistoire
    {
        private readonly ApplicationDbContext _context;
        private readonly IRepositoryMessage _messageRepository;


        public HistoireRepository(ApplicationDbContext context, IRepositoryMessage messageRepository)
        {
            _context = context;
            _messageRepository = messageRepository;
         }

        /// <summary>
        /// peuple la bdd de 6 histoires 
        /// </summary>
        /// <returns></returns>
        public async Task<bool> PeuplerHistoiresBDD()
        {

            try
            {
                ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
                // Pour HISTOIRE
                ///////////////////////////////////////////////////////////////////////////////////////////////////////////////

                //Pour les id utilisateur 
                Utilisateur eldaran = _context.Utilisateurs.Where(u => u.Email == "Eldaran83@gmail.com").FirstOrDefault();
                string eldaranId = eldaran.ApplicationUserID;

                Utilisateur pilou = _context.Utilisateurs.Where(u => u.Email == "Piloupilouvar@hotmail.fr").FirstOrDefault();
                string pilouId = pilou.ApplicationUserID;

                Utilisateur testMembre = _context.Utilisateurs.Where(u => u.Email == "TestMembre@gmail.com").FirstOrDefault();
                string testMembreId = testMembre.ApplicationUserID;

                //histoire 1
                Histoire histoire1 = new Histoire
                {
                    Createur = "Eldaran83",
                    Titre = "La Saga du crépuscule",
                    Synopsis = "Cette aventure est géniale vous devez absolument y participer et relever tous ses défis.",
                    UtilisateurID = eldaranId,
                    NombreDeFoisJouee = 0,
                    Score = 0,
                    UrlMedia = "/images/story-media-default.jpg"
                };
                _context.Add(histoire1);

                //histoire 2
                Histoire histoire2 = new Histoire
                {
                    Createur = "Pilou",
                    Titre = "L'histoire de M. Charles",
                    Synopsis = "Cette aventure est géniale vous devez absolument y participer et relever tous ses défis.",
                    UtilisateurID = pilouId,
                    NombreDeFoisJouee = 0,
                    Score = 0,
                    UrlMedia = "/images/story-media-default.jpg"
                };
                _context.Add(histoire2);

                //histoire 3
                Histoire histoire3 = new Histoire
                {
                    Createur = "TestMembre",
                    Titre = "La fin d'un empire",
                    Synopsis = "Cette aventure est géniale vous devez absolument y participer et relever tous ses défis.",
                    UtilisateurID = testMembreId,
                    NombreDeFoisJouee = 0,
                    Score = 0,
                    UrlMedia = "/images/story-media-default.jpg"
                };
                _context.Add(histoire3);

                //histoire 4
                Histoire histoire4 = new Histoire
                {
                    Createur = "Eldaran83",
                    Titre = "Le démon de la dernière chance",
                    Synopsis = "Cette aventure est géniale vous devez absolument y participer et relever tous ses défis.",
                    UtilisateurID = eldaranId,
                    NombreDeFoisJouee = 0,
                    Score = 0,
                    UrlMedia = "/images/story-media-default.jpg"
                };
                _context.Add(histoire4);

                //histoire 5
                Histoire histoire5 = new Histoire
                {
                    Createur = "Pilou",
                    Titre = "Pour une pièce de cuivre de plus",
                    Synopsis = "Cette aventure est géniale vous devez absolument y participer et relever tous ses défis.",
                    UtilisateurID = pilouId,
                    NombreDeFoisJouee = 0,
                    Score = 0,
                    UrlMedia = "/images/story-media-default.jpg"
                };
                _context.Add(histoire5);

                //histoire 6
                Histoire histoire6 = new Histoire
                {
                    Createur = "TestMembre",
                    Titre = "Pour une pièce de cuivre de plus",
                    Synopsis = "Cette aventure est géniale vous devez absolument y participer et relever tous ses défis.",
                    UtilisateurID = testMembreId,
                    NombreDeFoisJouee = 0,
                    Score = 0,
                    UrlMedia = "/images/story-media-default.jpg"
                };
                _context.Add(histoire6);

                    await _context.SaveChangesAsync();


                ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
                // Pour MESSAGE
                ///////////////////////////////////////////////////////////////////////////////////////////////////////////////

               // SqlConnection sqlConnection1 = new SqlConnection("DefaultConnection");
               // SqlCommand cmd = new SqlCommand();
               // SqlDataReader reader;

               //// cmd.CommandText = "SELECT * FROM Customers";
               // cmd.CommandText = "INSERT INTO dbo.Messages(MessageID, Contenu, HistoireID, NumeroMessageEnfant1, NumeroMessageEnfant2,NumeroMessageEnfant3,Titre,UrlMedia, NomAction1,NomAction2,NomAction3) VALUES(1, 'Vous avez la possibilité de choisir une action que pourra effectuer le héros au cours de l\'aventure', "+histoire1.HistoireID+ ", '','','','--Sélectionner une action--','','','','')";
               // cmd.CommandType = CommandType.Text;
               // cmd.Connection = sqlConnection1;

               // sqlConnection1.Open();

               // reader = cmd.ExecuteReader();

               // sqlConnection1.Close();
                //HISTOIRE 1
                //Message pour la DropDownList
                Message leMessageDeSelection = new Message
                {
                    MessageID = 1,
                    HistoireID = histoire1.HistoireID,
                    Titre = "--Sélectionner une action--",
                    Contenu = "Vous avez la possibilité de choisir une action que pourra effectuer le héros au cours de l'aventure"
                };
                // await _messageRepository.NouveauMessage(leMessageDeSelection);
                _context.Add(leMessageDeSelection);
                _context.SaveChanges();

                //Message 2
                Message message2Histoire1 = new Message
                {
                    MessageID = 2,
                    HistoireID = histoire1.HistoireID,
                    Titre = "Le commencement",
                    Contenu = "Le soleil se levait sur la prairie, et vous vous réveillez peu à peu en sentant l'herbe fraiche sous votre dos au milieu d'une clairière. Que faites vous ?",
                    NumeroMessageEnfant1 =3,
                    NomAction1 ="Observer autour de soi",
                    NumeroMessageEnfant2 = 4 ,
                    NomAction2 ="Se lever" ,
                    NumeroMessageEnfant3 =5, 
                    NomAction3 ="Continuer à dormir"
                };
                await _messageRepository.NouveauMessage(message2Histoire1);
                //Message 3
                Message message3Histoire1 = new Message
                {
                    MessageID = 3,
                    HistoireID = histoire1.HistoireID,
                    Titre = "Aux aguets",
                    Contenu = "Alors que votre regard contemple la clairière tout autour de vous, vous sentez dans l'air une odeur de brulée et remarquez un panache noir face à vous. Que faites vous ?",
                    NumeroMessageEnfant1 = 6,
                    NomAction1 = "Aller vers la fumée",
                    NumeroMessageEnfant2 = 7,
                    NomAction2 = "Partir à l'opposé de la fumée",
                    NumeroMessageEnfant3 = 5,
                    NomAction3 = "Se recoucher pour dormir"
                };
                await _messageRepository.NouveauMessage(message3Histoire1);
                //Message 4
                Message message4Histoire1 = new Message
                {
                    MessageID = 4,
                    HistoireID = histoire1.HistoireID,
                    Titre = "Sur ses deux pieds",
                    Contenu = "Alors que vous vous levez, vous remarquez un panache noir face à vous, de toute évidence il s'agit du résultat d'un incendie. Que faites vous ?",
                    NumeroMessageEnfant1 = 6,
                    NomAction1 = "Aller vers la fumée",
                    NumeroMessageEnfant2 = 7,
                    NomAction2 = "Partir à l'opposé de la fumée",
                    NumeroMessageEnfant3 = 5,
                    NomAction3 = "Se recoucher pour dormir"
                };
                await _messageRepository.NouveauMessage(message4Histoire1);
                //Message 5
                Message message5Histoire1 = new Message
                {
                    MessageID = 5,
                    HistoireID = histoire1.HistoireID,
                    Titre = "Le monde appartient à ceux qui se lévent tôt",
                    Contenu = "Faisant fi de tout ce qui pourrait se passer autour de vous, vous vous laissez attirer par les chaleureux et puissants bras de Morphée. Vous ressentez bien de façon de plus en plus cuisante une odeur de fumée mais votre coprs de vous répondu plus. Votre histoire s'arrête ici Héros ! ."
                };
                await _messageRepository.NouveauMessage(message5Histoire1);
                //Message 6
                Message message6Histoire1 = new Message
                {
                    MessageID = 6,
                    HistoireID = histoire1.HistoireID,
                    Titre = "Petit curieux",
                    Contenu = "Vous sortez de la clairière et vous entrez dans la forêt qui l'enserre. La fumée se fait de plus en plus dense rendant votre vison moins précise, et alors que vous êtes sur le point de vous demander s'il est bien prudent de continuer votre chemin, vous entendez un immense craquement sur votre flanc droit. Vous avez juste le temps d'apercevoir un tronc d'arbre en partie calciné vous frapper à la tête au moment où vous vous tournez vers ce son.",
                    NumeroMessageEnfant1 = 5,
                    NomAction1 = "Entre deux eaux"
                };
                await _messageRepository.NouveauMessage(message6Histoire1);
                //Message 7
                Message message7Histoire1 = new Message
                {
                    MessageID = 7,
                    HistoireID = histoire1.HistoireID,
                    Titre = "La prudence à parfois du bon",
                    Contenu = "Sans autres équipement sur vous et ne vous souvenant plus de rien, vous vous dites qu'il n'est peut être pas déjà temps de vous lancer dans des actions risquées.Faisant acte de prudence, vos pieds se tournent à l'opposé d'un potentiel danger, remarquant un petit sentier au travers de la forêt vous l'empruntez jusqu'à ne plus sentir cette odeur de fumée que loin derrière vous. Après une heure de marche, vos pas vous guide à l'entrée d'un petit village qui vous semblez habité. Que faites-vous ?.",
                    NumeroMessageEnfant1 = 8,
                    NomAction1 = "Chaque chose en son temps"
                };
                await _messageRepository.NouveauMessage(message7Histoire1);
                //Message 8
                Message message8Histoire1 = new Message
                {
                    MessageID = 8,
                    HistoireID = histoire1.HistoireID,
                    Titre = "Chaque chose en son temps",
                    Contenu = "La suite de l'aventure arrivera plus tard, merci d'y avoir participé."
                };
                await _messageRepository.NouveauMessage(message8Histoire1);

                //HISTOIRE 2
                //Message pour la DropDownList
                Message leMessageDeSelection2 = new Message
                {
                    MessageID = 1,
                    HistoireID = histoire2.HistoireID,
                    Titre = "--Sélectionner une action--",
                    Contenu = "Vous avez la possibilité de choisir une action que pourra effectuer le héros au cours de l'aventure"
                };
                await _messageRepository.NouveauMessage(leMessageDeSelection2);
                //Message 2
                Message message2Histoire2 = new Message
                {
                    MessageID = 2,
                    HistoireID = histoire2.HistoireID,
                    Titre = "Le commencement",
                    Contenu = "Aujourd’hui, comme tous les lundis, dès la sortie de la classe, je cours vers le parc avec mes copains pour y retrouver monsieur Charles. J’essaye d’arriver le premier pour pouvoir choisir l’histoire qu’il va nous conter. Moi, je choisis toujours des histoires de détectives... Vite je dois me dépêcher.. Que faites vous ?",
                    NumeroMessageEnfant1 = 3,
                    NomAction1 = "Continuer vers le parc"
                };
                await _messageRepository.NouveauMessage(message2Histoire2);
                //Message 3
                Message message3Histoire2 = new Message
                {
                    MessageID = 3,
                    HistoireID = histoire2.HistoireID,
                    Titre = "Le parc",
                    Contenu = "Nous voilà arrivés au parc. On repère toujours monsieur Charles de loin grâce à son grand panier rouge. Mais ce soir, le banc vert sur lequel monsieur Charles s’assoit est vide. Nous partons à sa recherche dans le parc.. Que faites vous ?",
                    NumeroMessageEnfant1 = 4,
                    NomAction1 = "Nous partons vers le grand arbre",
                    NumeroMessageEnfant2 = 5,
                    NomAction2 = "Nous partons vers le petit ruisseau",
                    NumeroMessageEnfant3 = 7,
                    NomAction3 = "Nous partons vers l’entrée du parc"
                };
                await _messageRepository.NouveauMessage(message3Histoire2);
                 //Message 4
                Message message4Histoire2 = new Message
                {
                    MessageID = 4,
                    HistoireID = histoire2.HistoireID,
                    Titre = "Le grand arbre",
                    Contenu = "Nous avons beau chercher, mais nous ne le trouvons toujours pas. Nous décidons de retourner vers le banc vert",
                    NumeroMessageEnfant1 = 6,
                    NomAction1 = "Vers le banc"
                };
                await _messageRepository.NouveauMessage(message4Histoire2);
                //Message 5
                Message message5Histoire2 = new Message
                {
                    MessageID = 5,
                    HistoireID = histoire2.HistoireID,
                    Titre = "Le petit ruisseau",
                    Contenu = "Il n’y a personne du côté du petit ruisseau. Nous décidons de retourner vers le banc vert.Pour aller plus vite nous décidons de revenir par le petit talus.",
                    NumeroMessageEnfant1 = 6,
                    NomAction1 = "Passer le talus"
                };
                await _messageRepository.NouveauMessage(message5Histoire2);
                //Message 6
                Message message6Histoire2 = new Message
                {
                    MessageID = 6,
                    HistoireID = histoire2.HistoireID,
                    Titre = "Le banc",
                    Contenu = "Nous nous asseyons sur le banc et attendons un peu. Mais personne ne vient. Pour la première fois, nous n’aurons pas d’histoire...",
                    NumeroMessageEnfant1 = 7,
                    NomAction1 = "La suite"
                };
                await _messageRepository.NouveauMessage(message6Histoire2);
                //Message 7
                Message message7Histoire2 = new Message
                {
                    MessageID = 7,
                    HistoireID = histoire2.HistoireID,
                    Titre = "L'entrée du parc",
                    Contenu = "Nous nous dirigeons vers l'entrée du parc.Mais personne ne vient. Pour la première fois, nous n’aurons pas d’histoire... Merci d'avoir suivi cette aventure, la suite arrive très vite"
                };
                await _messageRepository.NouveauMessage(message7Histoire2);

///////////////////////////////////////////////////////////////////
//RESTE ENCORE A FAIRE POUR LES 4 AUTRES HISTOIRES
////////////////////////////////////////////////////////////////////



                return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Le peuplement des histoires s'est mal passé " + ex);
                    return false;
                }

        }


        /// <summary>
        /// ajoute une nouvelle histoire
        /// prend en param l id de l utilisateur
        /// </summary>
        /// <param name="utilisateurID"></param>
        /// <returns></returns>
        public async Task<Histoire> NouvelleHistoire(Histoire histoireModele)
        {
            var utilisateur = await _context.Utilisateurs.Where(u => u.ApplicationUserID == histoireModele.UtilisateurID).FirstOrDefaultAsync();
            string createur = utilisateur.Pseudo;
            Histoire histoireAAjouter = new Histoire
            {
                Titre = histoireModele.Titre,
                NombreDeFoisJouee = 0,
                Score = 0,
                UtilisateurID = histoireModele.UtilisateurID,
                Createur= createur,
                Synopsis = histoireModele.Synopsis,
                UrlMedia = "/images/story-media-default.jpg"
            };

             _context.Histoires.Add(histoireAAjouter);
            await _context.SaveChangesAsync();

            return histoireAAjouter;
        }
         /// <summary>
        /// cherche dans la bdd si l histoire existe ou pas 
        /// fais la recherche sur le titre de l histoire
        /// </summary>
        /// <param name="titreHistoire"></param>
        /// <returns></returns>
        public async Task<bool> HistoireExist(string titreHistoire)
        {
            try
            {
                Histoire histoireAChercher =  await _context.Histoires.Where(h => h.Titre.ToUpper().Trim() == titreHistoire.ToUpper().Trim()).FirstOrDefaultAsync();

                 if (histoireAChercher != null)
                {
                    return true; //l histoire existe en bdd
                }
                else
                {
                    return false; //l histoire n 'existe pas en bdd
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("L'accès a la requête s'est mal passée " + ex);
                return false;
            }
        }
         /// <summary>
        /// renvoie toutes les histoires 
        /// classées par nombre de fois jouées descendant
        /// </summary>
        /// <returns></returns>
        public async  Task<IEnumerable<Histoire>> GetAllHistoiresAsync()
        {
            IEnumerable<Histoire> lesHistoires = await _context.Histoires
                                     .Include(u=>u.Utilisateur)
                                     .OrderByDescending(h=>h.NombreDeFoisJouee)
                                     .ToListAsync();
             return lesHistoires;
        }

        /// <summary>
        /// supprime une histoire
        /// avec tous ses messages
        /// </summary>
        /// <param name="histoireId"></param>
        /// <returns></returns>
        public async Task<bool> RemoveHistoireById(int? histoireId)
        {            try
            {
               // Supprime le dossier qui contient tous les fichiers de l'utilisateur  
                var dirPath = Path.Combine(
                               Directory.GetCurrentDirectory(),
                               "wwwroot" + "/StoryFiles/ImageHistoire/" + Convert.ToString(histoireId) + "_Histoire" + "/");

                Directory.Delete(dirPath, true);


                // var dirPath2 = Path.Combine(
                //Directory.GetCurrentDirectory(),
                //"wwwroot" + "/StoryFiles/" + Convert.ToString(histoireId) + "_Histoire");

                // Directory.Delete(dirPath2, true);
            }
            catch (Exception ex)
            {
                Console.WriteLine("L'histoire n'a pas de dossier image " + ex);
             }

            //L'histoire doit etre supprimé en dernier  !!
            try
            {
                 //suppresion des messages de l histoire
                IEnumerable<Message> lesMessagesDeLhistoire= await _messageRepository.GetAllMessageOfStoryAsync(histoireId);

                //supression des médias des messages 
                foreach (var item in lesMessagesDeLhistoire)
                {
                    await _messageRepository.RemoveMessageOfStoryById(item.MessageID, item.HistoireID);
                }
                //Supprime l histoire
                Histoire histoire = await _context.Histoires
                                         .Where(m => m.HistoireID == histoireId)
                                        .FirstOrDefaultAsync();

                _context.Histoires.Remove(histoire);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("La supression de l histoire a rencontré un problème :" + ex);
                return false;
            }
        }

        /// <summary>
        /// met a jour l histoire
        /// </summary>
        /// <param name="messageModele"></param>
        /// <returns></returns>
        public async Task<Histoire> UpdateHistoire(Histoire histoireModele)
        {
            try
            {
                _context.Update(histoireModele);
                await _context.SaveChangesAsync();
                return histoireModele;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                Console.WriteLine("La MAJ de l histoire a rencontré un problème :" + ex);
                return histoireModele;
            }
        }

        /// <summary>
        /// retourne une histoire grace a son id
        /// </summary>
        /// <param name="histoireID"></param>
        /// <returns></returns>
        public async Task<Histoire> GetHistoireByID(int? histoireID)
        {
            Histoire histoire = await _context.Histoires.Where(h => h.HistoireID == histoireID).FirstOrDefaultAsync();
            return histoire;
        }

        /// <summary>
        /// renvoie toutes les histoires de l utilisateur
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Histoire>> GetAllStoryOfUtilisateur(string userId)
        {
            IEnumerable<Histoire> lesHistoiresDeLUtilisateur = await _context.Histoires.Where(h => h.UtilisateurID == userId).ToListAsync();
            return lesHistoiresDeLUtilisateur;
        }
    }
}
