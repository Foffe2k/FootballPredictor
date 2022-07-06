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
        private List<GroupMatch> quarterFinals1;
        private List<GroupMatch> quarterFinals2;
        private List<GroupMatch> quarterFinals3;
        private List<GroupMatch> quarterFinals4;
        private List<GroupMatch> semiFinals1;
        private List<GroupMatch> semiFinals2;
        private List<GroupMatch> finals;

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

            /*
            var team01 = new Team("England", 1, "groupA");
            var team02 = new Team("Norge", 1, "groupA");
            var team03 = new Team("Österrike", 1, "groupA");
            var team04 = new Team("Nordirland", 1, "groupA");
            var team05 = new Team("Tyskland", 1, "groupB");
            var team06 = new Team("Spanien", 1, "groupB");
            var team07 = new Team("Danmark", 1, "groupB");
            var team08 = new Team("Finland", 1, "groupB");
            var team09 = new Team("Nederländerna", 1, "groupC");
            var team10 = new Team("Sverige", 1, "groupC");
            var team11 = new Team("Portugal", 1, "groupC");
            var team12 = new Team("Schweiz", 1, "groupC");
            var team13 = new Team("Frankrike", 1, "groupD");
            var team14 = new Team("Italien", 1, "groupD");
            var team15 = new Team("Belgien", 1, "groupD");
            var team16 = new Team("Island", 1, "groupD");
            */  
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

            winnerGroupA = FindWinner(groupA);
            winnerGroupB = FindWinner(groupB);
            winnerGroupC = FindWinner(groupC);
            winnerGroupD = FindWinner(groupD);

            runnerUpGroupA = FindRunnerUp(groupA);
            runnerUpGroupB = FindRunnerUp(groupB);
            runnerUpGroupC = FindRunnerUp(groupC);
            runnerUpGroupD = FindRunnerUp(groupD);



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
            Team runnerUp;


            return runnerUp;
        }

 

       



    }
}
