using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.IO;

namespace FootballPredictor
{
    class Tournament
    {
        private List<Team> listOfTeams;
        private List<GroupMatch> listOfMatches;

        private FinalsMatch quarterFinals1;
        private FinalsMatch quarterFinals2;
        private FinalsMatch quarterFinals3;
        private FinalsMatch quarterFinals4;
        private FinalsMatch semiFinals1;
        private FinalsMatch semiFinals2;
        private FinalsMatch finals;

        private Team winnerGroupA;
        private Team winnerGroupB;
        private Team winnerGroupC;
        private Team winnerGroupD;
        
        private Team runnerUpGroupA;
        private Team runnerUpGroupB;
        private Team runnerUpGroupC;
        private Team runnerUpGroupD;

        private ResultsPrinter resultsPrinter;


        public Tournament()
        {
            listOfTeams = new List<Team>();
            listOfMatches = new List<GroupMatch>();
            resultsPrinter = new ResultsPrinter();
            
            RegisterCompetingTeams();
            RegisterGroupPlayMatches();
            ResolveGroupPlay(listOfTeams);
            RegisterQuarterFinals();
            RegisterSemiFinals();
            RegisterFinals();
            FormatTournamentResultsForPrinting();
            PrintTournamentResultsToFile("TournamentResults");

            
            
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

        public void RegisterQuarterFinals()
        {
            quarterFinals1 = new FinalsMatch(winnerGroupA, runnerUpGroupB, "QF1");
            quarterFinals2 = new FinalsMatch(winnerGroupB, runnerUpGroupA, "QF2");
            quarterFinals3 = new FinalsMatch(winnerGroupC, runnerUpGroupD, "QF3");
            quarterFinals4 = new FinalsMatch(winnerGroupD, runnerUpGroupC, "QF4");
        }

        public void RegisterSemiFinals()
        {
            semiFinals1 = new FinalsMatch(quarterFinals1.winningTeam, quarterFinals3.winningTeam, "SF1");
            semiFinals2 = new FinalsMatch(quarterFinals2.winningTeam, quarterFinals4.winningTeam, "SF2");
        }

        public void RegisterFinals()
        {
            finals = new FinalsMatch(semiFinals1.winningTeam, semiFinals2.winningTeam, "Final");
        }

        public void FormatTournamentResultsForPrinting()
        {
            resultsPrinter.FormatGroupMatches(listOfMatches, "groupA");
            resultsPrinter.FormatGroupMatches(listOfMatches, "groupB");
            resultsPrinter.FormatGroupMatches(listOfMatches, "groupC");
            resultsPrinter.FormatGroupMatches(listOfMatches, "groupD");

            resultsPrinter.FormatFinalsMatches(quarterFinals1, "Kvartsfinal 1");
            resultsPrinter.FormatFinalsMatches(quarterFinals2, "Kvartsfinal 2");
            resultsPrinter.FormatFinalsMatches(quarterFinals3, "Kvartsfinal 3");
            resultsPrinter.FormatFinalsMatches(quarterFinals4, "Kvartsfinal 4");

            resultsPrinter.FormatFinalsMatches(semiFinals1, "Semifinal 1");
            resultsPrinter.FormatFinalsMatches(semiFinals2, "Semifinal 2");

            resultsPrinter.FormatFinalsMatches(finals, "Final");
            resultsPrinter.FormatWinner(finals);
        }

        public void PrintTournamentResultsToFile(string fileName)
        {
            _ = resultsPrinter.PrintResultsToFile(fileName);
        }

        

        
    }
}
