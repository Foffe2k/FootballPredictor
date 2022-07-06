﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace FootballPredictor
{
    class WorldCup
    {
        private List<Team> listOfTeams;
        private List<GroupMatch> listOfMatches;

        //private List<Team> groupA;
        //private List<Team> groupB;
        //private List<Team> groupC;
        //private List<Team> groupD;
        private FinalsMatch quarterFinals1;
        private FinalsMatch quarterFinals2;
        private FinalsMatch quarterFinals3;
        private FinalsMatch quarterFinals4;
        private FinalsMatch semiFinals1;
        private FinalsMatch semiFinals2;
        private FinalsMatch finals;

        Team winnerGroupA;
        Team winnerGroupB;
        Team winnerGroupC;
        Team winnerGroupD;

        Team runnerUpGroupA;
        Team runnerUpGroupB;
        Team runnerUpGroupC;
        Team runnerUpGroupD;

        public WorldCup()
        {
            listOfTeams = new List<Team>();
            listOfMatches = new List<GroupMatch>();
            
            RegisterCompetingTeams();
            RegisterGroupPlayMatches();
            ResolveGroupPlay(listOfTeams);
            RegisterSemiFinals();
            
        }

        public void RegisterCompetingTeams()
        {
            listOfTeams.AddRange(new List<Team>
            {
                new Team("England", 1, "groupA"),
                new Team("Norge", 4, "groupA"),
                new Team("Österrike", 13, "groupA"),
                new Team("Nordirland", 15, "groupA"),
                new Team("Tyskland", 10, "groupB"),
                new Team("Spanien", 5, "groupB"),
                new Team("Danmark", 3, "groupB"),
                new Team("Finland", 6, "groupB"),
                new Team("Nederländerna", 2, "groupC"),
                new Team("Sverige", 7, "groupC"),
                new Team("Portugal", 16, "groupC"),
                new Team("Schweiz", 14, "groupC"),
                new Team("Frankrike", 8, "groupD"),
                new Team("Italien", 11, "groupD"),
                new Team("Belgien", 9, "groupD"),
                new Team("Island", 12, "groupD"),
            }); 
        }

        public void RegisterGroupPlayMatches()
        {
            listOfMatches.AddRange(new List<GroupMatch> { 
                
                //Grupp A
                new GroupMatch("England", "Österrike", "GPA1", listOfTeams),
                new GroupMatch("Norge", "Nordirland", "GPA2", listOfTeams),
                new GroupMatch("Österrike", "Nordirland", "GPA3", listOfTeams),
                new GroupMatch("England", "Norge", "GPA4", listOfTeams),
                new GroupMatch("Nordirland", "England", "GPA5", listOfTeams),
                new GroupMatch("Österrike", "Norge", "GPA6", listOfTeams),
                //Grupp B
                new GroupMatch("Spanien", "Finland", "GPB1", listOfTeams),
                new GroupMatch("Tyskland", "Danmark", "GPB2", listOfTeams),
                new GroupMatch("Danmark", "Finland", "GPB3", listOfTeams),
                new GroupMatch("Tyskland", "Spanien", "GPB4", listOfTeams),
                new GroupMatch("Danmark", "Spanien", "GPB5", listOfTeams),
                new GroupMatch("Finland", "Tyskland", "GPB6", listOfTeams),
                //Grupp C
                new GroupMatch("Portugal", "Schweiz", "GPC1", listOfTeams),
                new GroupMatch("Nederländerna", "Sverige", "GPC2", listOfTeams),
                new GroupMatch("Sverige", "Schweiz", "GPC3", listOfTeams),
                new GroupMatch("Nederländerna", "Portugal", "GPC4", listOfTeams),
                new GroupMatch("Sverige", "Portugal", "GPC5", listOfTeams),
                new GroupMatch("Schweiz", "Nederländerna", "GPC6", listOfTeams),
                //Grupp D
                new GroupMatch("Belgien", "Island", "GPD1", listOfTeams),
                new GroupMatch("Frankrike", "Italien", "GPD2", listOfTeams),
                new GroupMatch("Italien", "Island", "GPD3", listOfTeams),
                new GroupMatch("Frankrike", "Belgien", "GPD4", listOfTeams),
                new GroupMatch("Island", "Frankrike", "GPD5", listOfTeams),
                new GroupMatch("Italien", "Belgien", "GPD6", listOfTeams),
            });
        }

        public void ResolveGroupPlay(List<Team> teamRoster)
        {
            List<Team> groupA = PopulateGroup(teamRoster, "groupA");
            List<Team> groupB = PopulateGroup(teamRoster, "groupB");
            List<Team> groupC = PopulateGroup(teamRoster, "groupC");
            List<Team> groupD = PopulateGroup(teamRoster, "groupD");

            winnerGroupA = FindWinnerLinq(groupA);
            winnerGroupB = FindWinnerLinq(groupB);
            winnerGroupC = FindWinnerLinq(groupC);
            winnerGroupD = FindWinnerLinq(groupD);

            runnerUpGroupA = FindRunnerUpLinq(groupA);
            runnerUpGroupB = FindRunnerUpLinq(groupB);
            runnerUpGroupC = FindRunnerUpLinq(groupC);
            runnerUpGroupD = FindRunnerUpLinq(groupD);
        }

        public List<Team> PopulateGroup(List<Team> teamRoster, string group)
        {
            List<Team> result = new List<Team>();

            foreach (Team team in teamRoster)
            {
                if (team.startingGroup.Equals(group))
                {
                    result.Add(team);
                }
            }

            return result;
        }

        public Team FindWinnerLinq(List<Team> group)
        {
            group.OrderBy(x => x.groupPlayScore).ThenBy(x => x.calculateGoalDifference()).ThenBy(x => x.qualifierRank2022);

            return group[0];
        }

        public Team FindRunnerUpLinq(List<Team> group)
        {
            group.OrderBy(x => x.groupPlayScore).ThenBy(x => x.calculateGoalDifference()).ThenBy(x => x.qualifierRank2022);

            return group[1];
        }

        public void RegisterSemiFinals()
        {
            quarterFinals1 = new FinalsMatch(winnerGroupA, runnerUpGroupB, "QF1");
            quarterFinals2 = new FinalsMatch(winnerGroupC, runnerUpGroupD, "QF2");
            quarterFinals3 = new FinalsMatch(winnerGroupB, runnerUpGroupA, "QF3");
            quarterFinals4 = new FinalsMatch(winnerGroupD, runnerUpGroupC, "QF4");

        }





        /*
        
        //An ode to wasted effort
        
        public Team FindWinner(List<Team> group)
        {
            List<Team> groupList = group.OrderByDescending(t => t.groupPlayScore).ToList();

            int highestGroupPlayScore = groupList[0].groupPlayScore;

            if (groupPlayScoreIsTied(groupList))
            {
                List<Team> tiedTeams = new List<Team>();
                
                foreach (Team team in groupList)
                {
                    if(team.groupPlayScore == highestGroupPlayScore)
                    {
                        tiedTeams.Add(team);
                    }
                }

                return groupPlayTiebreaker(tiedTeams);
            }
            else
            {
                return group[0];
            }   
        }

        public bool groupPlayScoreIsTied(List<Team> group)
        {
            List<Team> groupList = group.OrderByDescending(t => t.groupPlayScore).ToList();

            if (groupList[0].groupPlayScore == groupList[1].groupPlayScore)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Team groupPlayTiebreaker(List<Team> tiedTeamsGroupPlay) //Ska skicka tillbaka det bästa laget av de lag som delar bäst gruppspelspoäng
        {
            int bestGoalDifference = 0;
            Team bestTeam = new Team("Tiebreaker", 99, "Ö");

            if (goalDifferenceIsTied(tiedTeamsGroupPlay)) //Om den största målskillnaden innehas av flera lag
            {
                List<Team> groupList = tiedTeamsGroupPlay.OrderByDescending(t => t.goalsScored).ToList();

                int mostGoalsScored = groupList[0].goalsScored;

                List<Team> tiedTeamsGoalsScored = new List<Team>();
                
                foreach (Team team in groupList)
                {
                    if(team.goalsScored == mostGoalsScored)
                    {
                        tiedTeamsGoalsScored.Add(team);
                    }
                }

                return goalsScoredTiebreaker(tiedTeamsGoalsScored);

            }
            else
            {
                foreach (Team team in tiedTeamsGroupPlay)
                {
                    int goalDifference = team.calculateGoalDifference();

                    if (goalDifference > bestGoalDifference || bestGoalDifference == 0)
                    {
                        bestGoalDifference = goalDifference;
                        bestTeam = team;
                    }   
                }

                return bestTeam;
            }

        }

        public Team goalsScoredTiebreaker(List<Team> tiedTeams)
        {
            tiedTeams.OrderByDescending(t => t.qualifierRank2022).ToList();

            return tiedTeams[0];
        }

        public bool goalDifferenceIsTied(List<Team> group)
        {
            List<Team> groupList = group.OrderByDescending(t => t.calculateGoalDifference()).ToList();

            if (groupList[0].groupPlayScore == groupList[1].groupPlayScore)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public Team FindRunnerUp(List<Team> group)
        {
            Team runnerUp = new Team("Fake", 98, "Ö");

            return runnerUp;
        }
        */









    }
}
