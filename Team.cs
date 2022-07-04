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
        public double goalAverage;



        public Team(string teamName, int qualifierRank2022, string startingGroup)
        {
            name = teamName;
            this.qualifierRank2022 = qualifierRank2022;
            this.startingGroup = startingGroup;
            groupPlayScore = 0;
            goalsScored = 0;
            goalsConceded = 0;
            goalAverage = 0;

        }    

        public void logMatchResults(int scoredGoals, int concededGoals, int matchScore)
        {
            goalsScored += scoredGoals;
            goalsConceded += concededGoals;
            groupPlayScore += matchScore;
            calculateGoalAverage();
        }        

        public void calculateGoalAverage()
        {
            goalAverage = goalsScored / goalsConceded;
        }
            
      
    }
}
