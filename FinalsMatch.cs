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
        private const int INDEX_FOR_FIRST_TEAM = 0;
        private const int INDEX_FOR_SECOND_TEAM = 1;

        private Random randomGenerator = new Random();

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
            SetOutcomeOfMatch();            
        }

        protected override void SetOutcomeOfMatch()
        {
            SetGoalsByTeam(team1);
            SetGoalsByTeam(team2);
            SetWinner();
        }

        private void SetGoalsByTeam(Team team)
        {
            int scoredGoals = 0;

            scoredGoals += randomGenerator.Next(MINIMUM_RANDOM_GOALS, MAXIMUM_RANDOM_GOALS);
            scoredGoals += RandomlyGetAddtionalGoals();

            matchResults.Add(new TeamScore(scoredGoals, team));
        }

        private int RandomlyGetAddtionalGoals()
        {
            return GetAdditionalGoals(randomGenerator, 0, 0, 0, 1, 1, 1, 1, 2, 2, 3);
        }        

        private void SetWinner()
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
            TeamScore ts1 = matchResults[INDEX_FOR_FIRST_TEAM];
            TeamScore ts2 = matchResults[INDEX_FOR_SECOND_TEAM];           

            return ts1.score == ts2.score;
        }

        private void RedoMatchOutcome()
        {
            matchResults.Clear();
            SetOutcomeOfMatch();
        }

        private void SetWinningAndLosingTeam()
        {
            TeamScore ts1 = matchResults[INDEX_FOR_FIRST_TEAM];
            TeamScore ts2 = matchResults[INDEX_FOR_SECOND_TEAM];

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