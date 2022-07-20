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

        public Team team1 { get; private set; }
        public Team team2 { get; private set; }
        public string matchName { get; private set; }
        private int ratingDifference { get; set; }
        public List<TeamScore> matchResults { get; private set; }
        
        public GroupMatch(Country countryName1, Country countryName2, string matchName, List<Team> teamRoster)
        {
            matchResults = new List<TeamScore>();

            try
            {
                team1 = teamRoster.Single(x => x.name.Equals(countryName1.ToString()));
                team2 = teamRoster.Single(x => x.name.Equals(countryName2.ToString()));
            }
            catch (MissingTeamException e)
            {
                Console.WriteLine(e.Message);
            }

            this.matchName = matchName;
            calculateDifferenceInRanking();
            calculateGoalsByTeam(team1, team2);
            calculateGoalsByTeam(team2, team1);
            CalculateMatchPoints(matchResults);
            AdjustGoalStatistics(matchResults);
        }       

        private void calculateDifferenceInRanking()
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
       
        private void calculateGoalsByTeam(Team currentTeam, Team opposingTeam)
        {
            int scoredGoals = 0;
            Random random = new Random();

            scoredGoals += random.Next(MINIMUM_RANDOM_GOALS, MAXIMUM_RANDOM_GOALS);
            scoredGoals += RandomlyAddAddtionalGoalsWeightedByQualifierRanking(currentTeam, opposingTeam);

            matchResults.Add(new TeamScore(scoredGoals, currentTeam));
        }

        private int RandomlyAddAddtionalGoalsWeightedByQualifierRanking(Team currentTeam, Team opposingTeam)
        {
            Random random = new Random();
            int randomPercentage = random.Next(1, 100);

            if (CurrentTeamIsRatedHigher(currentTeam, opposingTeam) && ratingDifference > 11)
            {
                return GetAdditionalGoals(randomPercentage, 0, 1, 2, 2, 3);
            } 
            else if (CurrentTeamIsRatedHigher(currentTeam, opposingTeam) && ratingDifference > 6)
            {
                return GetAdditionalGoals(randomPercentage, 0, 1, 1, 2, 3);
            }
            else if (CurrentTeamIsRatedHigher(currentTeam, opposingTeam) && ratingDifference > 0)
            {
                return GetAdditionalGoals(randomPercentage, 0, 0, 1, 2, 3);
            }

            if (!CurrentTeamIsRatedHigher(currentTeam, opposingTeam) && ratingDifference > 11)
            {
                return GetAdditionalGoals(randomPercentage, 0, 0, 1, 2, 3);
            }
            else if (!CurrentTeamIsRatedHigher(currentTeam, opposingTeam) && ratingDifference > 6)
            {
                return GetAdditionalGoals(randomPercentage, 0, 1, 1, 2, 3);                
            }
            else 
            {
                return GetAdditionalGoals(randomPercentage, 0, 1, 1, 2, 3);
            }
        }

        private bool CurrentTeamIsRatedHigher(Team currentTeam, Team opposingTeam)
        {
            return currentTeam.qualifierRank2022 > opposingTeam.qualifierRank2022;
        }

        //private int GetAddtionalGoalsForHigherRatedTeam
       
        private int GetAdditionalGoals(int randomPercentage, int mediumChance1, int mediumChance2, int mediumChance3, int mediumChance4, int veryRareChance)
        {
            if (randomPercentage == 100)
            {
                return veryRareChance;
            }
            else if (randomPercentage >= 75)
            {
                return mediumChance4;
            }
            else if (randomPercentage >= 50)
            {
                return mediumChance3;
            }
            else if (randomPercentage >= 25)
            {
                return mediumChance2;
            }
            else
            {
                return mediumChance1;
            }
        }

        private void CalculateMatchPoints(List<TeamScore> matchResults)
        {
            TeamScore ts1 = matchResults[0];
            TeamScore ts2 = matchResults[1];

            int scoreTeam1 = ts1.score;
            int scoreTeam2 = ts2.score;

            if(scoreTeam1 > scoreTeam2)
            {
                ts1.team.groupPlayScore += 3;
            } 
            else if (scoreTeam2 > scoreTeam1)
            {
                ts2.team.groupPlayScore += 3;
            }
            else if (scoreTeam1 == scoreTeam2)
            {
                ts1.team.groupPlayScore += 1;
                ts2.team.groupPlayScore += 1;
            }
        }


        private void AdjustGoalStatistics(List<TeamScore> matchResults)
        {
            Team team1 = matchResults[0].team;
            Team team2 = matchResults[1].team;

            int scoreTeam1 = matchResults[0].score;
            int scoreTeam2 = matchResults[1].score;

            team1.goalsScored += scoreTeam1;
            team1.goalsConceded += scoreTeam2;
            team2.goalsScored += scoreTeam2;
            team2.goalsConceded += scoreTeam1;
        }
        
        public int GetTeamScore(List<TeamScore> matchResults, string teamName)
        {
            TeamScore team = matchResults.Single(r => r.team.name.Equals(teamName));

            return team.score;
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
