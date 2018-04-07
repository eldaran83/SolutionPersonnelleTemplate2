using Microsoft.AspNetCore.Http;
using SolutionPersonnelleTemplate.Models.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SolutionPersonnelleTemplate.Models.ViewModels
{
    public class HistoireViewModel
    {
        public Histoire Histoire { get; set; }
        public IFormCollection form { get; set; }
    }
}
