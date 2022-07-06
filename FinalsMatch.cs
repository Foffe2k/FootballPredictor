using System;
using System.Collections.Generic;
using System.Text;

namespace FootballPredictor
{
    class FinalsMatch
    {
        public Team team1;
        public Team team2;

        public Team winningTeam;
        public Team losingTeam;

        public string matchName;

        public List<TeamScore> matchResults;

        public FinalsMatch(Team team1, Team team2, string matchName)
        {
            this.team1 = team1;
            this.team2 = team2;
            this.matchName = matchName;
            calculateGoalsByTeam(team1, matchResults);
            calculateGoalsByTeam(team2, matchResults);
            calculateWinner(matchResults);
        }

        public void calculateGoalsByTeam(Team team, List<TeamScore> matchResult)
        {
            int scoredGoals = 0;
            Random random = new Random();

            scoredGoals += random.Next(0, 2);
            scoredGoals += adjustForFate(random);

            matchResult.Add(new TeamScore(scoredGoals, team));
        }

        public int adjustForFate(Random random)
        {
            int fate = random.Next(0, 100);

            if (fate == 100)
            {
                return 3;
            }
            else if (fate >= 85)
            {

                return 2;
            }
            else if (fate >= 50)
            {
                return 1;
            }
            else if (fate >= 30)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public void calculateWinner(List<TeamScore> matchResult)
        {
            TeamScore ts1 = matchResult[0];
            TeamScore ts2 = matchResult[1];

            if (matchIsADraw(matchResult))
            {
                matchResults.Clear();
                calculateGoalsByTeam(team1, matchResult);
                calculateGoalsByTeam(team2, matchResult);
                calculateWinner(matchResults);
            }
            else
            {
                if (ts1.score > ts2.score)
                {
                    winningTeam = ts1.team;
                    losingTeam = ts2.team;
                }
                else if (ts2.score > ts1.score)
                {
                    winningTeam = ts2.team;
                    losingTeam = ts1.team;
                }
            }    
        }

        public bool matchIsADraw(List<TeamScore> matchResults)
        {
            if (matchResults[0].score == matchResults[1].score)
            {
                return true;
            }

            return false;
        }
    }
}