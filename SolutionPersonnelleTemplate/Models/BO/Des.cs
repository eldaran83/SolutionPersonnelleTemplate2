using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SolutionPersonnelleTemplate.Models.BO
{
    public class Des
    {
        private Random _random;
        public Des()
        {
            _random = new Random();

        }
        /// <summary>
        /// tous les types de dés possibles dansl e moteur du jeu
        /// </summary>
        public enum TypeDeDes
        {
            D4,
            D6,
            D8,
            D10,
            D12,
            D20
        }

        /// <summary>
        /// lance les dés pour l aventure
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<int> LanceLeDe(TypeDeDes dice)
        {
            int degatsDes = 0;

            switch (dice)
            {
                case TypeDeDes.D4:
                    degatsDes = _random.Next(1, 4);
                    break;
                case TypeDeDes.D6:
                    degatsDes = _random.Next(1, 6);
                    break;
                case TypeDeDes.D8:
                    degatsDes = _random.Next(1, 8);
                    break;
                case TypeDeDes.D10:
                    degatsDes = _random.Next(1, 10);
                    break;
                case TypeDeDes.D12:
                    degatsDes = _random.Next(1, 12);
                    break;
                case TypeDeDes.D20:
                    degatsDes = _random.Next(1, 20);
                    break;
                default:
                    break;
            }
            return degatsDes;
        }

        /// <summary>
        /// détermine si le lancer de des est ou pas un critique 
        /// pour le moment parti pris de partir sur une base D20
        /// </summary>
        /// <returns></returns>
        public async Task<bool> EstReussiteCritique(int valeurJetDeDe)
        {
           // int scoreAuDes = 20;

            if (valeurJetDeDe == 20)
            {
                return true;
            }
            else
            {
                return false;
            }
         }

        public async Task<bool> EstEchecCritique(int valeurJetDeDe)
        {
            // int scoreAuDes = 1;

            if (valeurJetDeDe == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
