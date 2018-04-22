using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using SolutionPersonnelleTemplate.Services;

namespace SolutionPersonnelleTemplate.Services
{
    public static class EmailSenderExtensions
    {
        public static Task SendEmailConfirmationAsync(this IEmailSender emailSender, string email, string link)
        {
            string subject = "Confirmer votre inscription";
            string message = $"<h2>Cliquez sur le lien et à vous de jouer :<a style='text-decoration: none' href='{HtmlEncoder.Default.Encode(link)}'> ça se passe par ici</a></h2>";
            return emailSender.SendEmailAsync(email, subject, message);
                 

        }
    }
}
