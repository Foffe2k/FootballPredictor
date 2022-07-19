using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FootballPredictor
{
    class FinalsMatch: Match
    {
        private const int MINIMUM_RANDOM_GOALS = 0;
        private const int MAXIMUM_RANDOM_GOALS = 2;

        public Team team1 { get; private set; }
        public Team team2 { get; private set; }
        public Team winningTeam { get; private set; }
        public Team losingTeam { get; private set; }
        public string matchName { get; private set; }
        public List<TeamScore> matchResults { get; private set; }

        public FinalsMatch(Team team1, Team team2, string matchName)
        {
            this.team1 = team1;
            this.team2 = team2;
            this.matchName = matchName;
            matchResults = new List<TeamScore>();
            DecideOutcomeOfMatch();            
        }

        private void DecideOutcomeOfMatch()
        {
            CalculateGoalsByTeam(team1);
            CalculateGoalsByTeam(team2);
            CalculateWinner(matchResults);
        }

        private void CalculateGoalsByTeam(Team team)
        {
            int scoredGoals = 0;
            Random random = new Random();

            scoredGoals += random.Next(MINIMUM_RANDOM_GOALS, MAXIMUM_RANDOM_GOALS);
            scoredGoals += RandomlyAddAddtionalGoals(random);

            matchResults.Add(new TeamScore(scoredGoals, team));
        }

        private int RandomlyAddAddtionalGoals(Random random)
        {
            int randomPercentage = random.Next(1, 100);

            if (randomPercentage == 100)
            {
                return 3;
            }
            else if (randomPercentage >= 85)
            {
                return 2;
            }
            else if (randomPercentage >= 50)
            {
                return 1;
            }
            else if (randomPercentage >= 30)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        private void CalculateWinner(List<TeamScore> matchResult)
        {
            if (MatchIsADraw(matchResult))
            {
                RedoMatchOutcome();
            }
            else
            {
                SetWinningAndLosingTeam(matchResult);
            }    
        }

        private bool MatchIsADraw(List<TeamScore> matchResult)
        {
            TeamScore ts1 = matchResult[0];
            TeamScore ts2 = matchResult[1];

            if (ts1.score == ts2.score)
            {
                return true;
            }

            return false;
        }

        private void RedoMatchOutcome()
        {
            matchResults.Clear();
            DecideOutcomeOfMatch();
        }

        private void SetWinningAndLosingTeam(List<TeamScore> matchResult)
        {
            TeamScore ts1 = matchResult[0];
            TeamScore ts2 = matchResult[1];

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

        public int GetTeamScore(List<TeamScore> matchResult, string teamName)
        {
            TeamScore teamScore = matchResult.Single(ts => ts.GetTeamName().Equals(teamName));

            return teamScore.score;
        }   
        
        public string GetWinningTeamName()
        {
            return winningTeam.name;
        }
    }
}