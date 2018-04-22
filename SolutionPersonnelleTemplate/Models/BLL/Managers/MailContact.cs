using Microsoft.Extensions.Options;
using SolutionPersonnelleTemplate.Models.BLL.Interfaces;
using SolutionPersonnelleTemplate.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace SolutionPersonnelleTemplate.Models.BLL.Managers
{
    public class MailContact : StrategyMailContact
    {
         public MailContact(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public EmailSettings _emailSettings { get; }

        /// <summary>
        /// pattern strategy, permet de savoir vers quoi on envoie et le cas echant le sujet et le message
        /// </summary>
        /// <param name="email"></param>
        /// <param name="subject"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public async override Task JenvoieCaOuStrategy(string email, string subject, string message)
        {
            //A caque fois que j ajoute un sujet fortement typé il faut le gérer ici aussi 
            //pour le moment y il a comme sujet réservé ca :
            // - Demande pour devenir un Scribe
            // - Confirmer votre inscription

            //pour le sujet devenir sribe
            if (subject == "Demande pour devenir un Scribe")
            {
                string toEmail = string.IsNullOrEmpty(email)
                ? _emailSettings.ToEmail
                : email;
                MailMessage mail = new MailMessage()
                {
                    From = new MailAddress(_emailSettings.UsernameEmail, "Roll a Dice - Faire passer en Sribe")
                };
                mail.To.Add(new MailAddress(toEmail));
                //   mail.CC.Add(new MailAddress(_emailSettings.CcEmail));

                mail.Subject = subject;
                // mail.Body = message;
                mail.Body = "<p>Un utilisateur veut devenir un scribe et il faut le passer en statut manager </p>" + message;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;

                using (SmtpClient smtp = new SmtpClient(_emailSettings.PrimaryDomain, _emailSettings.PrimaryPort))
                {
                    smtp.Credentials = new NetworkCredential(_emailSettings.UsernameEmail, _emailSettings.UsernamePassword);
                    smtp.EnableSsl = true;
                    await smtp.SendMailAsync(mail);
                }
            }

            if (subject == "Confirmer votre inscription")
            {
                string toEmail = string.IsNullOrEmpty(email)
                ? _emailSettings.ToEmail
                : email;
                MailMessage mail = new MailMessage()
                {
                    From = new MailAddress(_emailSettings.UsernameEmail, "Roll a Dice - Confirmer son inscription")
                };
                mail.To.Add(new MailAddress(toEmail));
                //   mail.CC.Add(new MailAddress(_emailSettings.CcEmail));

                mail.Subject = subject;
                // mail.Body = message;
                mail.Body = "<p>Finissez votre inscription</p> <br />" + message;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;

                using (SmtpClient smtp = new SmtpClient(_emailSettings.PrimaryDomain, _emailSettings.PrimaryPort))
                {
                    smtp.Credentials = new NetworkCredential(_emailSettings.UsernameEmail, _emailSettings.UsernamePassword);
                    smtp.EnableSsl = true;
                    await smtp.SendMailAsync(mail);
                }
            }

            if (subject != null && subject != "Demande pour devenir un Scribe"
                && subject != "Confirmer votre inscription")
            {
                string toEmail = string.IsNullOrEmpty(email)
                                    ? _emailSettings.ToEmail
                                    : email;
                MailMessage mail = new MailMessage()
                {
                    From = new MailAddress(_emailSettings.UsernameEmail, "Vous êtes un héros - By ROLL a DICE -")
                };
                mail.To.Add(new MailAddress(toEmail));
                //   mail.CC.Add(new MailAddress(_emailSettings.CcEmail));

                mail.Subject = "Héros vous avez un message ! - " + subject;
                mail.Body = message;
               // mail.Body = monContenuMail + message;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;

                using (SmtpClient smtp = new SmtpClient(_emailSettings.PrimaryDomain, _emailSettings.PrimaryPort))
                {
                    smtp.Credentials = new NetworkCredential(_emailSettings.UsernameEmail, _emailSettings.UsernamePassword);
                    smtp.EnableSsl = true;
                    await smtp.SendMailAsync(mail);
                }
            }

            


        }
    }
}
