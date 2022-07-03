using System;
using System.Collections.Generic;
using System.Text;

namespace FootballPredictor
{
    class Team
    {    
        public String name;
        public double averageScorePerMatchWorldCup2018;
        public String startingGroup;
        public int groupPlayScore;
        public int goalsScored;
        public int goalsConceded;
        public double goalAverage;

        

        public Team(String teamName, double avgScorePerMatch, String startingGroup)
        {
            name = teamName;
            averageScorePerMatchWorldCup2018 = avgScorePerMatch;
            this.startingGroup = startingGroup;
            groupPlayScore = 0;
            goalsScored = 0;
            goalsConceded = 0;

        }

        public void logMatchResults(Match match)
        {
            var team1 = match.team1;
            var team2 = match.team2;          

            var team1Score = match.scoreTeam1;
            var team2score = match.scoreTeam2;

            if (name.Equals(team1.name))
            {
                logMatchGoals(team1Score, team2score);
            }
            else if (name.Equals(team2.name))
            {
                logMatchGoals(team2score, team1Score);
            }
        }

        public void logMatchGoals(int scoredGoals, int concededGoals)
        {
            goalsScored = scoredGoals;
            goalsConceded = concededGoals;
        }

        public void calculateGoalAverage()
        {
            goalAverage = goalsScored / goalsConceded;
        }
            
      
    }
}
