using System;
using System.Collections.Generic;
using System.Text;

namespace FootballPredictor
{
    class WorldCup
    {
        private List<Team> listOfTeams;
        private List<Match> listOfMatches;

        //private List<Team> groupA;
        //private List<Team> groupB;
        //private List<Team> groupC;
        //private List<Team> groupD;
        private List<Team> quarterFinals1;
        private List<Team> quarterFinals2;
        private List<Team> quarterFinals3;
        private List<Team> quarterFinals4;
        private List<Team> semiFinals1;
        private List<Team> semiFinals2;
        private List<Team> finals;

        public WorldCup()
        {
            listOfTeams = new List<Team>();
            RegisterCompetingTeams();
            
        }

        public void RegisterCompetingTeams()
        {
            listOfTeams.AddRange(new List<Team>
            {
                new Team("England", 1, "groupA"),
                new Team("Norge", 1, "groupA"),
                new Team("Österrike", 1, "groupA"),
                new Team("Nordirland", 1, "groupA"),
                new Team("Tyskland", 1, "groupB"),
                new Team("Spanien", 1, "groupB"),
                new Team("Danmark", 1, "groupB"),
                new Team("Finland", 1, "groupB"),
                new Team("Nederländerna", 1, "groupC"),
                new Team("Sverige", 1, "groupC"),
                new Team("Portugal", 1, "groupC"),
                new Team("Schweiz", 1, "groupC"),
                new Team("Frankrike", 1, "groupD"),
                new Team("Italien", 1, "groupD"),
                new Team("Belgien", 1, "groupD"),
                new Team("Island", 1, "groupD"),
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

        public void ResolveGroupA()
        {

        }

        public void ResolveMatch(Team team1, Team team2)
        {

        }

    }
}
