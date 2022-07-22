using System;
using System.Collections.Generic;
using System.Text;

namespace FootballPredictor
{
    abstract class Match
    {
        protected abstract void SetOutcomeOfMatch();

        protected int GetAdditionalGoals(Random random, int decaOne, int decaTwo, int decaThree, int decaFour, int decaFive, int decaSix, int decaSeven, int decaEight, int decaNine, int decaTen)
        {
            int randomPercentage = random.Next(1, 100);

            if(randomPercentage == 100)
            {
                return decaTen + 1;
            }
            
            else if (randomPercentage >= 90)
            {
                return decaTen;
            }
            else if (randomPercentage >= 80)
            {
                return decaNine;
            }
            else if (randomPercentage >= 70)
            {
                return decaEight;
            }
            else if (randomPercentage >= 60)
            {
                return decaSeven;
            }
            else if (randomPercentage >= 50)
            {
                return decaSix;
            }
            else if (randomPercentage >= 40)
            {
                return decaFive;
            }
            else if (randomPercentage >= 30)
            {
                return decaFour;
            }
            else if (randomPercentage >= 20)
            {
                return decaThree;
            }
            else if (randomPercentage >= 10)
            {
                return decaTwo;
            }
            else
            {
                return decaOne;
            }
        }
    }
}
