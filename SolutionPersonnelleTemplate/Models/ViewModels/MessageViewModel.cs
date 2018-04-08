using Microsoft.AspNetCore.Http;
using SolutionPersonnelleTemplate.Models.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SolutionPersonnelleTemplate.Models.ViewModels
{
    public class MessageViewModel
    {
        public Message Message { get; set; }
        public IFormCollection Form { get; set; }
    }
}
