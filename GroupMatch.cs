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
            team1 = teamRoster.Single(x => x.name.Equals(nameTeam1));
            team2 = teamRoster.Single(x => x.name.Equals(nameTeam2));
            this.matchName = matchName;
            calculateDifferenceInRanking();
            calculateGoalsByTeam(team1, team2);
            calculateGoalsByTeam(team2, team1);
            decideWinner(matchResults);
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

            scoredGoals += random.Next(0, 3);
            scoredGoals += adjustForRanking(currentTeam, opposingTeam);

            matchResults.Add(new TeamScore(scoredGoals, currentTeam));
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

        private void decideWinner(List<TeamScore> matchResults)
        {
            //Team t1 = matchResults.Single(x => x.team.name.Equals(team1.name));
            //TODO: Hämta rätt team från Teamscore genom att jämföra teamet i teamscoren mot teamen som redan finns representerade i matchen
        }
    }
}
