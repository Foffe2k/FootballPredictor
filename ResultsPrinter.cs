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
                string nameOfTeam1 = groupMatch.team1.name;
                string nameOfTeam2 = groupMatch.team2.name;
                
                int goalsScoredByTeam1 = groupMatch.GetGoalsScoredInMatchByTeam(nameOfTeam1);
                int goalsScoredByTeam2 = groupMatch.GetGoalsScoredInMatchByTeam(nameOfTeam2);

                formattedGroupString += nameOfTeam1 + " - " + nameOfTeam2 + " : " + goalsScoredByTeam1 + " - " + goalsScoredByTeam2 + "\n";
            }

            formattedGroupString += "\n";

            formattedOutMessage += formattedGroupString;
        }      

        public void FormatFinalsMatches(FinalsMatch finalsMatch)
        {
            string formattedFinalsString = "";

            string matchName = finalsMatch.matchName;
            string nameOfTeam1 = finalsMatch.team1.name;
            string nameOfTeam2 = finalsMatch.team2.name;

            int goalsScoredByTeam1 = finalsMatch.GetGoalsScoredInMatchByTeam(nameOfTeam1);
            int goalsScoredByTeam2 = finalsMatch.GetGoalsScoredInMatchByTeam(nameOfTeam2);

            formattedFinalsString += "--" + matchName + "--\n";
            formattedFinalsString += nameOfTeam1 + " - " + nameOfTeam2 + " : " + goalsScoredByTeam1 + " - " + goalsScoredByTeam2 + "\n";
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
