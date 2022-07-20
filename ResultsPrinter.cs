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


        private string formattedOutMessage = "";

        public void FormatGroupMatches(List<GroupMatch> listOfMatches, Group group)
        {            
            string formattedGroupString = "";

            string groupID = group.ToString();

            List<GroupMatch> groupMatches = GetGroupMatches(listOfMatches, groupID);
            
            groupMatches.OrderBy(m => m.matchName).ToList();

            formattedGroupString += "--Grupp " + groupID + "--\n";
            
            foreach (GroupMatch groupMatch in groupMatches)
            {
                string team1Name = groupMatch.team1.name;
                string team2Name = groupMatch.team2.name;
                
                int team1Score = groupMatch.getTeamScore(groupMatch.matchResults, team1Name);
                int team2Score = groupMatch.getTeamScore(groupMatch.matchResults, team2Name);

                formattedGroupString += team1Name + " - " + team2Name + " : " + team1Score + " - " + team2Score + "\n";
            }

            formattedGroupString += "\n";

            formattedOutMessage += formattedGroupString;
        }

        private List<GroupMatch> GetGroupMatches(List<GroupMatch> listOfMatches, string groupID)
        {
            List<GroupMatch> matches = new List<GroupMatch>();

            foreach (GroupMatch match in listOfMatches)
            {
                if (MatchIsPartOfCurrentGroup(match, groupID))
                {
                    matches.Add(match);
                }
            }
            return matches;
        }

        private bool MatchIsPartOfCurrentGroup(GroupMatch match, string groupID)
        {
            if (match.matchName.Equals("GP" + groupID + "1") ||
                match.matchName.Equals("GP" + groupID + "2") ||
                match.matchName.Equals("GP" + groupID + "3") ||
                match.matchName.Equals("GP" + groupID + "4") ||
                match.matchName.Equals("GP" + groupID + "5") ||
                match.matchName.Equals("GP" + groupID + "6"))
            {
                return true;
            }
            return false;
        }

        public void FormatFinalsMatches(FinalsMatch finalsMatch)
        {
            string formattedFinalsString = "";

            string matchName = finalsMatch.matchName;
            string team1Name = finalsMatch.team1.name;
            string team2Name = finalsMatch.team2.name;

            int team1Score = finalsMatch.GetTeamScore(finalsMatch.matchResults, team1Name);
            int team2Score = finalsMatch.GetTeamScore(finalsMatch.matchResults, team2Name);

            formattedFinalsString += "--" + matchName + "--\n";
            formattedFinalsString += team1Name + " - " + team2Name + " : " + team1Score + " - " + team2Score + "\n";
            formattedFinalsString += "\n";

            formattedOutMessage += formattedFinalsString;
        }

        public void FormatWinner(FinalsMatch finalsMatch)
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
