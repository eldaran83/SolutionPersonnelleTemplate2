using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SolutionPersonnelleTemplate.Models.BLL.Interfaces
{
    public abstract class StrategyMailContact
    {
        public abstract Task JenvoieCaOuStrategy(string email, string subject, string message);
    }
}
