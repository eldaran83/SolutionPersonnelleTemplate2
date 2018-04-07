using Microsoft.AspNetCore.Http;
using SolutionPersonnelleTemplate.Models.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SolutionPersonnelleTemplate.Models.BLL.Managers
{
    public class FichierRepository : IRepositoryFichier
    {
        /// <summary>
        /// cette methode supprime un fichier dans un dossier
        /// </summary>
        /// <param name="path"></param>
        /// <param name="fichiersASupprimer"></param>
        public void RemoveFichierAvatar(string path, string fichiersASupprimer)
        {
            try
            {
                string file_name = fichiersASupprimer;
                string path_name = path + file_name;

                FileInfo file = new FileInfo(path_name);
                if (file.Exists)//check file exsit or not
                {
                    file.Delete();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("le fichier n'a pas pu être supprimé " + ex);
            }
        }

        /// <summary>
        /// Cette methode ajoute un fichier dans un dossier
        /// </summary>
        public string SaveFichierAvatar(string webRoot, string nameDirectory, string nameId, string nomDuDossier, IFormCollection form)
        {
            string directoryToPutMedia = nameDirectory;
            try
            {
                //si le dossier UserFiles n existe pas il est créé
                if (!System.IO.Directory.Exists(webRoot + directoryToPutMedia))
                {
                    System.IO.Directory.CreateDirectory(webRoot + directoryToPutMedia);
                }
                //si le dossier personnalisé de l'user dans UserFiles n existe pas il est créé
                if (!System.IO.Directory.Exists(webRoot + directoryToPutMedia + nameId + nomDuDossier))
                {
                    System.IO.Directory.CreateDirectory(webRoot + directoryToPutMedia + nameId + nomDuDossier);
                }
                //chemin de destination où sera ajouté le fichier
                var path = Path.Combine(
                           Directory.GetCurrentDirectory(), "wwwroot" + directoryToPutMedia + nameId + nomDuDossier,
                           form.Files[0].FileName);
                //ouvre la connexion et ajoute le fichier dans le chemin indiqué
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    form.Files[0].CopyToAsync(stream);
                }

                //retourne l url du fichier pour l ajouter à la classe
                string urlFile = "";
                string[] strfiles = Directory.GetFiles(webRoot + directoryToPutMedia + nameId + nomDuDossier, "*.*");
                for (int i = 0; i < strfiles.Length; i++)
                {
                    string fileName = Path.GetFileName(strfiles[i]);
                    string _CurrentFile = strfiles[i].ToString();
                    if (System.IO.File.Exists(_CurrentFile))
                    {
                        string tempFileURL = directoryToPutMedia + nameId + nomDuDossier + Path.GetFileName(_CurrentFile);
                        urlFile = tempFileURL;
                    }
                    //"/UserFiles/"
                }

                return urlFile;

            }
            catch (Exception ex)
            {
                Console.WriteLine("le fichier n'a pas pu être ajouté " + ex);
                return null;
            }

        }
    }
}
