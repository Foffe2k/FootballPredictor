using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace FootballPredictor
{
    class GroupMatch: Match
    {
        private const int MINIMUM_RANDOM_GOALS = 0;
        private const int MAXIMUM_RANDOM_GOALS = 3;
        private const int POINTS_AWARDED_FOR_WIN = 3;
        private const int POINTS_AWARDED_FOR_DRAW = 1;
        private const int INDEX_OF_FIRST_TEAM = 0;
        private const int INDEX_OF_SECOND_TEAM = 1;
        private const int HIGH_DIFFERENCE_IN_RANKING = 11;
        private const int MEDIUM_DIFFERENCE_IN_RANKING = 6;

        private Random randomGenerator = new Random();

        public Team team1 { get; private set; }
        public Team team2 { get; private set; }
        public string matchName { get; private set; }
        private int ratingDifference { get; set; }
        public List<TeamScore> matchResults { get; private set; }


        public GroupMatch(Country countryName1, Country countryName2, string matchName, List<Team> participatingTeams)
        {
            matchResults = new List<TeamScore>();                        
            team1 = participatingTeams.Single(x => x.name.Equals(countryName1.ToString()));
            team2 = participatingTeams.Single(x => x.name.Equals(countryName2.ToString()));         
            this.matchName = matchName;            
            SetOutcomeOfMatch();
        }       

        protected override void SetOutcomeOfMatch()
        {
            SetDifferenceInRanking();
            SetGoalsScoredByTeam(team1, team2);
            SetGoalsScoredByTeam(team2, team1);
            AdjustMatchPoints();
            AdjustGoalStatistics();
        }

        private void SetDifferenceInRanking()
        {
            if(team1.qualifierRank2022 > team2.qualifierRank2022)
            {
                ratingDifference = team1.qualifierRank2022 - team2.qualifierRank2022;
            }
            else
            {
                ratingDifference = team2.qualifierRank2022 - team1.qualifierRank2022;
            }
        }
       
        private void SetGoalsScoredByTeam(Team currentTeam, Team opposingTeam)
        {
            int scoredGoals = 0;

            scoredGoals += randomGenerator.Next(MINIMUM_RANDOM_GOALS, MAXIMUM_RANDOM_GOALS);
            scoredGoals += RandomlyAddAddtionalGoalsWeightedByQualifierRanking(currentTeam, opposingTeam);

            matchResults.Add(new TeamScore(scoredGoals, currentTeam));
        }

        private int RandomlyAddAddtionalGoalsWeightedByQualifierRanking(Team currentTeam, Team opposingTeam)
        {
            
            if (CurrentTeamIsRatedHigher(currentTeam, opposingTeam))
            {
                return GetAddtionalGoalsForHigherRankedTeam();
            }
            else
            {
                return GetAdditionalGoalsForLowerRankedTeam();
            }
        }

        private bool CurrentTeamIsRatedHigher(Team currentTeam, Team opposingTeam)
        {
            return currentTeam.qualifierRank2022 > opposingTeam.qualifierRank2022;
        }

        private int GetAddtionalGoalsForHigherRankedTeam()
        {
            if (ratingDifference > HIGH_DIFFERENCE_IN_RANKING)
            {
                return GetAdditionalGoals(randomGenerator, 0, 1, 1, 1, 1, 2, 2, 2, 2, 3);
            }
            else if (ratingDifference > MEDIUM_DIFFERENCE_IN_RANKING)
            {
                return GetAdditionalGoals(randomGenerator, 0, 0, 1, 1, 1, 1, 2, 2, 2, 2);
            }
            else 
            {
                return GetAdditionalGoals(randomGenerator, 0, 0, 0, 1, 1, 1, 1, 2, 2, 2);
            }
        }

        private int GetAdditionalGoalsForLowerRankedTeam()
        {
            if (ratingDifference > HIGH_DIFFERENCE_IN_RANKING)
            {
                return GetAdditionalGoals(randomGenerator, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1);
            }
            else if (ratingDifference > MEDIUM_DIFFERENCE_IN_RANKING)
            {
                return GetAdditionalGoals(randomGenerator, 0, 0, 0, 0, 1, 1, 1, 1, 1, 2);
            }
            else
            {
                return GetAdditionalGoals(randomGenerator, 0, 0, 0, 1, 1, 1, 1, 1, 2, 2);
            }
        }
       
        private void AdjustMatchPoints()
        {
            TeamScore ts1 = matchResults[INDEX_OF_FIRST_TEAM];
            TeamScore ts2 = matchResults[INDEX_OF_SECOND_TEAM];

            int scoreTeam1 = ts1.score;
            int scoreTeam2 = ts2.score;

            if(scoreTeam1 > scoreTeam2)
            {
                ts1.team.groupPlayScore += POINTS_AWARDED_FOR_WIN;
            } 
            else if (scoreTeam2 > scoreTeam1)
            {
                ts2.team.groupPlayScore += POINTS_AWARDED_FOR_WIN;
            }
            else if (scoreTeam1 == scoreTeam2)
            {
                ts1.team.groupPlayScore += POINTS_AWARDED_FOR_DRAW;
                ts2.team.groupPlayScore += POINTS_AWARDED_FOR_DRAW;
            }
        }

        private void AdjustGoalStatistics()
        {
            Team team1 = matchResults[INDEX_OF_FIRST_TEAM].team;
            Team team2 = matchResults[INDEX_OF_SECOND_TEAM].team;

            int scoreTeam1 = matchResults[INDEX_OF_FIRST_TEAM].score;
            int scoreTeam2 = matchResults[INDEX_OF_SECOND_TEAM].score;

            team1.goalsScored += scoreTeam1;
            team1.goalsConceded += scoreTeam2;
            team2.goalsScored += scoreTeam2;
            team2.goalsConceded += scoreTeam1;
        }
        
        public override int GetGoalsScoredInMatchByTeam(string teamName)
        {
            TeamScore teamScore = matchResults.Single(result => result.GetTeamName().Equals(teamName));

            return teamScore.score;
        }

        public Group GetGroupID()
        {
            if (team1.startingGroup.Equals(team2.startingGroup))
            {
                return team1.startingGroup;
            }
            else
            {
                throw new InvalidGroupPlayMatchUpException();
            }
        }
    }
}
