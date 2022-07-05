using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace FootballPredictor
{
    class GroupMatch
    {
        public Team team1;
        public Team team2;

        public int ratingDifference;

        public string matchName;

        public List<TeamScore> matchResults;
        
        public GroupMatch(string nameTeam1, string nameTeam2, string matchName, List<Team> teamRoster)
        {
            matchResults = new List<TeamScore>();

            team1 = teamRoster.Single(x => x.name.Equals(nameTeam1));
            team2 = teamRoster.Single(x => x.name.Equals(nameTeam2));
            this.matchName = matchName;
            calculateDifferenceInRanking();
            calculateGoalsByTeam(team1, team2, matchResults);
            calculateGoalsByTeam(team2, team1, matchResults);
            calculateMatchPoints(matchResults);
            adjustGoalStatistics(matchResults);
            adjustTeamGoalAverage(team1, team2);
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

       
        private void calculateGoalsByTeam(Team currentTeam, Team opposingTeam, List<TeamScore> matchResult)
        {
            int scoredGoals = 0;
            Random random = new Random();

            scoredGoals += random.Next(0, 3);
            scoredGoals += adjustForRanking(currentTeam, opposingTeam);

            matchResult.Add(new TeamScore(scoredGoals, currentTeam));
        }

        private int adjustForRanking(Team currentTeam, Team opposingTeam)
        {
            Random random = new Random();
            int fate = random.Next(0, 100);

            if (currentTeam.qualifierRank2022 > opposingTeam.qualifierRank2022 && ratingDifference > 11)
            {
                if (fate == 100)
                {
                    return 3;
                }
                else if (fate >= 75)
                {
                    return 2;
                }
                else if (fate >= 50)
                {
                    return 2;
                }
                else if (fate >= 25)
                {
                    return 1;
                }
                else if (fate >= 0)
                {
                    return 0;
                }
            } 
            else if (currentTeam.qualifierRank2022 > opposingTeam.qualifierRank2022 && ratingDifference > 6)
            {
                if (fate == 100)
                {
                    return 3;
                }
                else if (fate >= 75)
                {
                    return 2;
                }
                else if (fate >= 50)
                {
                    return 1;
                }
                else if (fate >= 25)
                {
                    return 1;
                }
                else if (fate >= 0)
                {
                    return 0;
                }
            }
            else if (currentTeam.qualifierRank2022 > opposingTeam.qualifierRank2022 && ratingDifference > 0)
            {
                if (fate == 100)
                {
                    return 3;
                }
                else if (fate >= 75)
                {
                    return 2;
                }
                else if (fate >= 50)
                {
                    return 1;
                }
                else if (fate >= 25)
                {
                    return 0;
                }
                else if (fate >= 0)
                {
                    return 0;
                }
            }

            if (currentTeam.qualifierRank2022 < opposingTeam.qualifierRank2022 && ratingDifference > 11)
            {
                if (fate == 100)
                {
                    return 3;
                }
                else if (fate >= 75)
                {
                    return 2;
                }
                else if (fate >= 50)
                {
                    return 1;
                }
                else if (fate >= 25)
                {
                    return 0;
                }
                else if (fate >= 0)
                {
                    return 0;
                }
            }
            else if (currentTeam.qualifierRank2022 < opposingTeam.qualifierRank2022 && ratingDifference > 6)
            {
                if (fate == 100)
                {
                    return 3;
                }
                else if (fate >= 75)
                {
                    return 2;
                }
                else if (fate >= 50)
                {
                    return 1;
                }
                else if (fate >= 25)
                {
                    return 1;
                }
                else if (fate >= 0)
                {
                    return 0;
                }
            }
            else if (currentTeam.qualifierRank2022 < opposingTeam.qualifierRank2022 && ratingDifference > 0)
            {
                if (fate == 100)
                {
                    return 3;
                }
                else if (fate >= 75)
                {
                    return 2;
                }
                else if (fate >= 50)
                {
                    return 1;
                }
                else if (fate >= 25)
                {
                    return 1;
                }
                else if (fate >= 0)
                {
                    return 0;
                }
            }

            return random.Next(0,1);
        }

       
        private void calculateMatchPoints(List<TeamScore> matchResults)
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

        private void adjustGoalStatistics(List<TeamScore> matchResults)
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

        private void adjustTeamGoalAverage(Team team1, Team team2)
        {
            team1.calculateGoalAverage();
            team2.calculateGoalAverage();
        }
    }
}
