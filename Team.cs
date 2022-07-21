using System;
using System.Collections.Generic;
using System.Text;

namespace FootballPredictor
{
    class Team
    {    
        public string name { get; private set; }
        public int qualifierRank2022 { get; private set; }
        public Group startingGroup { get; private set; }        
        public int groupPlayScore { get; set; }        
        public int goalsScored { get; set; }
        public int goalsConceded { get; set; }

        public Team(Country country, Group startingGroup)
        {
            name = country.ToString();
            qualifierRank2022 = (int) country;
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
