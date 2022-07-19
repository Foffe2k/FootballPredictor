using System;
using System.Collections.Generic;
using System.Text;

namespace FootballPredictor
{
    class Team
    {    
        public string name;
        public int qualifierRank2022;
        public Group startingGroup;
        
        public int groupPlayScore;
        
        public int goalsScored;
        public int goalsConceded;

        public Team(Country country, Group startingGroup)
        {
            name = startingGroup.ToString();
            qualifierRank2022 = (int) startingGroup;
            this.startingGroup = startingGroup;
            groupPlayScore = 0;
            goalsScored = 0;
            goalsConceded = 0;
        }

        public int GetGoalDifference()
        {
            return goalsScored - goalsConceded;
        }
    }
}
