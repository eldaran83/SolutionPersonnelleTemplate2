using Microsoft.Extensions.Options;
using SolutionPersonnelleTemplate.Models;
using SolutionPersonnelleTemplate.Models.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace SolutionPersonnelleTemplate.Services
{
    // This class is used by the application to send email for account confirmation and password reset.
    // For more details see https://go.microsoft.com/fwlink/?LinkID=532713
    public class EmailSender : IEmailSender
    {
        private readonly StrategyMailContact _strategyMailContact;

        public EmailSender(IOptions<EmailSettings> emailSettings, StrategyMailContact strategyMailContact)
        {
            _emailSettings = emailSettings.Value;
            _strategyMailContact = strategyMailContact;
        }

        public EmailSettings _emailSettings { get; }

        public Task SendEmailAsync(string email, string subject, string message)
        {

            Execute(email, subject, message).Wait();
            return Task.FromResult(0);
        }

        public async Task Execute(string email, string subject, string message)
        {         
            try
            {
                await _strategyMailContact.JenvoieCaOuStrategy(email, subject, message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
