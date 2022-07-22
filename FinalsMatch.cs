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
        
        private Random random = new Random();

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

        protected override void DecideOutcomeOfMatch()
        {
            CalculateGoalsByTeam(team1);
            CalculateGoalsByTeam(team2);
            CalculateWinner();
        }

        private void CalculateGoalsByTeam(Team team)
        {
            int scoredGoals = 0;

            scoredGoals += random.Next(MINIMUM_RANDOM_GOALS, MAXIMUM_RANDOM_GOALS);
            scoredGoals += RandomlyAddAddtionalGoals();

            matchResults.Add(new TeamScore(scoredGoals, team));
        }

        private int RandomlyAddAddtionalGoals()
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

        private void CalculateWinner()
        {
            if (MatchIsADraw())
            {
                RedoMatchOutcome();
            }
            else
            {
                SetWinningAndLosingTeam();
            }    
        }

        private bool MatchIsADraw()
        {
            TeamScore ts1 = matchResults[0];
            TeamScore ts2 = matchResults[1];           

            return ts1.score == ts2.score;
        }

        private void RedoMatchOutcome()
        {
            matchResults.Clear();
            DecideOutcomeOfMatch();
        }

        private void SetWinningAndLosingTeam()
        {
            TeamScore ts1 = matchResults[0];
            TeamScore ts2 = matchResults[1];

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

        public int GetTeamScore(string teamName)
        {
            TeamScore teamScore = matchResults.Single(result => result.GetTeamName().Equals(teamName));

            return teamScore.score;
        }   
        
        public string GetWinningTeamName()
        {
            return winningTeam.name;
        }
    }
}