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
        private const string PRINTED_FILENAME = "WomensUEFA2022Results";
        private List<Team> listOfParticipatingTeams { get; set; }
        private List<GroupMatch> listOfGroupMatches { get; set; }
        private FinalsMatch quarterFinals1 { get; set; }
        private FinalsMatch quarterFinals2 { get; set; }
        private FinalsMatch quarterFinals3 { get; set; }
        private FinalsMatch quarterFinals4 { get; set; }
        private FinalsMatch semiFinals1 { get; set; }
        private FinalsMatch semiFinals2 { get; set; }
        private FinalsMatch finals { get; set; }
        
        private Team winnerGroupA { get; set; }
        private Team winnerGroupB { get; set; }
        private Team winnerGroupC { get; set; }
        private Team winnerGroupD { get; set; }

        private Team runnerUpGroupA { get; set; }
        private Team runnerUpGroupB { get; set; }
        private Team runnerUpGroupC { get; set; }
        private Team runnerUpGroupD { get; set; }

        private ResultsPrinter resultsPrinter { get; set; }

        public Tournament()
        {
            listOfParticipatingTeams = new List<Team>();
            listOfGroupMatches = new List<GroupMatch>();
            resultsPrinter = new ResultsPrinter();  
            
            RegisterCompetingTeams();
            RegisterGroupPlayMatches();
            ResolveGroupPlay(listOfParticipatingTeams);
            RegisterQuarterFinals();
            RegisterSemiFinals();
            RegisterFinals();
            FormatTournamentResultsForPrinting();
            PrintTournamentResultsToFile(PRINTED_FILENAME);   
        }

        public void RegisterCompetingTeams()
        {
            listOfParticipatingTeams.AddRange(new List<Team>
            {
                new Team(Country.England, Group.A),
                new Team(Country.Norge, Group.A),
                new Team(Country.Österrike, Group.A),
                new Team(Country.Nordirland, Group.A),
                new Team(Country.Tyskland, Group.B),
                new Team(Country.Spanien, Group.B),
                new Team(Country.Danmark, Group.B),
                new Team(Country.Finland, Group.B),
                new Team(Country.Nederländerna, Group.C),
                new Team(Country.Sverige, Group.C),
                new Team(Country.Portugal, Group.C),
                new Team(Country.Schweiz, Group.C),
                new Team(Country.Frankrike, Group.D),
                new Team(Country.Italien, Group.D),
                new Team(Country.Belgien, Group.D),
                new Team(Country.Island, Group.D),
            }); 
        }

        public void RegisterGroupPlayMatches()
        {
            listOfGroupMatches.AddRange(new List<GroupMatch> { 
                
                //Grupp A
                new GroupMatch(Country.England, Country.Österrike, "GPA1", listOfParticipatingTeams),
                new GroupMatch(Country.Norge, Country.Nordirland, "GPA2", listOfParticipatingTeams),
                new GroupMatch(Country.Österrike, Country.Nordirland, "GPA3", listOfParticipatingTeams),
                new GroupMatch(Country.England, Country.Norge, "GPA4", listOfParticipatingTeams),
                new GroupMatch(Country.Nordirland, Country.England, "GPA5", listOfParticipatingTeams),
                new GroupMatch(Country.Österrike, Country.Norge, "GPA6", listOfParticipatingTeams),
                //Grupp B
                new GroupMatch(Country.Spanien, Country.Finland, "GPB1", listOfParticipatingTeams),
                new GroupMatch(Country.Tyskland, Country.Danmark, "GPB2", listOfParticipatingTeams),
                new GroupMatch(Country.Danmark, Country.Finland, "GPB3", listOfParticipatingTeams),
                new GroupMatch(Country.Tyskland, Country.Spanien, "GPB4", listOfParticipatingTeams),
                new GroupMatch(Country.Danmark, Country.Spanien, "GPB5", listOfParticipatingTeams),
                new GroupMatch(Country.Finland, Country.Tyskland, "GPB6", listOfParticipatingTeams),
                //Grupp C
                new GroupMatch(Country.Portugal, Country.Schweiz, "GPC1", listOfParticipatingTeams),
                new GroupMatch(Country.Nederländerna, Country.Sverige, "GPC2", listOfParticipatingTeams),
                new GroupMatch(Country.Sverige, Country.Schweiz, "GPC3", listOfParticipatingTeams),
                new GroupMatch(Country.Nederländerna, Country.Portugal, "GPC4", listOfParticipatingTeams),
                new GroupMatch(Country.Sverige, Country.Portugal, "GPC5", listOfParticipatingTeams),
                new GroupMatch(Country.Schweiz, Country.Nederländerna, "GPC6", listOfParticipatingTeams),
                //Grupp D
                new GroupMatch(Country.Belgien, Country.Island, "GPD1", listOfParticipatingTeams),
                new GroupMatch(Country.Frankrike, Country.Italien, "GPD2", listOfParticipatingTeams),
                new GroupMatch(Country.Italien, Country.Island, "GPD3", listOfParticipatingTeams),
                new GroupMatch(Country.Frankrike, Country.Belgien, "GPD4", listOfParticipatingTeams),
                new GroupMatch(Country.Island, Country.Frankrike, "GPD5", listOfParticipatingTeams),
                new GroupMatch(Country.Italien, Country.Belgien, "GPD6", listOfParticipatingTeams),
            });
        }

        public void ResolveGroupPlay(List<Team> tournamentRoster)
        {
            List<Team> groupA = PopulateGroup(tournamentRoster, Group.A);
            List<Team> groupB = PopulateGroup(tournamentRoster, Group.B);
            List<Team> groupC = PopulateGroup(tournamentRoster, Group.C);
            List<Team> groupD = PopulateGroup(tournamentRoster, Group.D);

            winnerGroupA = FindWinner(groupA);
            winnerGroupB = FindWinner(groupB);
            winnerGroupC = FindWinner(groupC);
            winnerGroupD = FindWinner(groupD);

            runnerUpGroupA = FindRunnerUp(groupA);
            runnerUpGroupB = FindRunnerUp(groupB);
            runnerUpGroupC = FindRunnerUp(groupC);
            runnerUpGroupD = FindRunnerUp(groupD);
        }

        public List<Team> PopulateGroup(List<Team> teamRoster, Group group)
        {
            List<Team> groupMembers = new List<Team>();

            foreach (Team team in teamRoster)
            {
                if (team.startingGroup.Equals(group))
                {
                    groupMembers.Add(team);
                }
            }

            return groupMembers;
        }

        public Team FindWinner(List<Team> group)
        {
            SortGroupAccordingToGroupVictoryCriteria(group);

            return group[0];
        }

        public void SortGroupAccordingToGroupVictoryCriteria(List<Team> group)
        {
            group.OrderBy(team => team.groupPlayScore).
                   ThenBy(team => team.GetGoalDifference()).
                   ThenBy(team => team.qualifierRank2022);
        }

        public Team FindRunnerUp(List<Team> group)
        {
            SortGroupAccordingToGroupVictoryCriteria(group);

            return group[1];
        }

        public void RegisterQuarterFinals()
        {
            quarterFinals1 = new FinalsMatch(winnerGroupA, runnerUpGroupB, "Kvartsfinal 1");
            quarterFinals2 = new FinalsMatch(winnerGroupB, runnerUpGroupA, "Kvartsfinal 2");
            quarterFinals3 = new FinalsMatch(winnerGroupC, runnerUpGroupD, "Kvartsfinal 3");
            quarterFinals4 = new FinalsMatch(winnerGroupD, runnerUpGroupC, "Kvartsfinal 4");
        }

        public void RegisterSemiFinals()
        {
            semiFinals1 = new FinalsMatch(quarterFinals1.winningTeam, quarterFinals3.winningTeam, "Semifinal 1");
            semiFinals2 = new FinalsMatch(quarterFinals2.winningTeam, quarterFinals4.winningTeam, "Semifinal 2");
        }

        public void RegisterFinals()
        {
            finals = new FinalsMatch(semiFinals1.winningTeam, semiFinals2.winningTeam, "Final");
        }

        public void FormatTournamentResultsForPrinting()
        {
            resultsPrinter.FormatGroupMatches(listOfGroupMatches, Group.A);
            resultsPrinter.FormatGroupMatches(listOfGroupMatches, Group.B);
            resultsPrinter.FormatGroupMatches(listOfGroupMatches, Group.C);
            resultsPrinter.FormatGroupMatches(listOfGroupMatches, Group.D);

            resultsPrinter.FormatFinalsMatches(quarterFinals1);
            resultsPrinter.FormatFinalsMatches(quarterFinals2);
            resultsPrinter.FormatFinalsMatches(quarterFinals3);
            resultsPrinter.FormatFinalsMatches(quarterFinals4);

            resultsPrinter.FormatFinalsMatches(semiFinals1);
            resultsPrinter.FormatFinalsMatches(semiFinals2);

            resultsPrinter.FormatFinalsMatches(finals);
            resultsPrinter.FormatWinner(finals);
        }

        public void PrintTournamentResultsToFile(string fileName)
        {
            _ = resultsPrinter.PrintResultsToFile(fileName);
        }

    }
}
