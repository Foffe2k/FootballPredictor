using System;
using System.Collections.Generic;
using System.Text;

namespace FootballPredictor
{
    class Team
    {    
        public string name;
        public int qualifierRank2022;
        public string startingGroup;
        public int groupPlayScore;
        public int goalsScored;
        public int goalsConceded;

        public Team(string teamName, int qualifierRank2022, string startingGroup)
        {
            name = teamName;
            this.qualifierRank2022 = qualifierRank2022;
            this.startingGroup = startingGroup;
            groupPlayScore = 0;
            goalsScored = 0;
            goalsConceded = 0;
        }                      

        public double calculateGoalAverage()
        {
            if (goalsScored > 0 && goalsConceded > 0)
            {
                return goalsScored / goalsConceded;
            }
            else if (goalsScored > 0 && goalsConceded == 0)
            {
                return goalsScored / 1;
            }
            else if (goalsScored == 0 && goalsConceded > 0)
            {
                return  1 / goalsConceded;
            }
            return 0;
        }

        public int calculateGoalDifference()
        {
            return goalsScored - goalsConceded;
        }
    }
}
