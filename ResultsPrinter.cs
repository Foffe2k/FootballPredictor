using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballPredictor
{
    class ResultsPrinter
    {
        private const string CORRECT_FILENAME = "WomensUEFA2022Results";
        private const string CORRECT_FILE_EXTENSION = ".txt";

        private Tournament currentTournament;

        private string formattedOutMessage;

        public ResultsPrinter(Tournament tournament)
        {
            currentTournament = tournament;
        }

        public void FormatGroupMatches(Group groupID)
        {            
            string formattedGroupString = "";

            List<GroupMatch> groupMatches = currentTournament.GetGroupMatches(groupID);
            
            groupMatches.OrderBy(m => m.matchName).ToList();

            formattedGroupString += "--Grupp " + groupID.ToString() + "--\n";
            
            foreach (GroupMatch groupMatch in groupMatches)
            {
                string team1Name = groupMatch.team1.name;
                string team2Name = groupMatch.team2.name;
                
                int team1Score = groupMatch.GetTeamsScore(team1Name);
                int team2Score = groupMatch.GetTeamsScore(team2Name);

                formattedGroupString += team1Name + " - " + team2Name + " : " + team1Score + " - " + team2Score + "\n";
            }

            formattedGroupString += "\n";

            formattedOutMessage += formattedGroupString;
        }      

        public void FormatFinalsMatches(FinalsMatch finalsMatch)
        {
            string formattedFinalsString = "";

            string matchName = finalsMatch.matchName;
            string team1Name = finalsMatch.team1.name;
            string team2Name = finalsMatch.team2.name;

            int team1Score = finalsMatch.GetTeamScore(team1Name);
            int team2Score = finalsMatch.GetTeamScore(team2Name);

            formattedFinalsString += "--" + matchName + "--\n";
            formattedFinalsString += team1Name + " - " + team2Name + " : " + team1Score + " - " + team2Score + "\n";
            formattedFinalsString += "\n";

            formattedOutMessage += formattedFinalsString;
        }

        public void FormatWinnerOfTournament(FinalsMatch finalsMatch)
        {
            string formattedWinnerString = "";

            formattedWinnerString += "Vinnaren är: " + finalsMatch.GetWinningTeamName() + "\n";

            formattedOutMessage += formattedWinnerString;
        }              

        public async Task PrintResultsToFile()
        {
            string fileName = CORRECT_FILENAME + CORRECT_FILE_EXTENSION;

            await File.WriteAllTextAsync(fileName, formattedOutMessage);
        }

    }
}
