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
        private const string ACCEPTED_FILE_EXTENSION = ".txt";
        
        private string formattedOutMessage = "";

        public void FormatGroupMatches(List<GroupMatch> listOfMatches, string groupName)
        {            
            string groupID = groupName.Substring(5, 1);
            string formattedGroupString = "";

            List<GroupMatch> matches = SortGroupMatches(listOfMatches, groupID);
            matches.OrderBy(m => m.matchName).ToList();

            formattedGroupString += "--Grupp " + groupID + "--\n";
            
            foreach (GroupMatch match in matches)
            {
                string t1Name = match.team1.name;
                string t2Name = match.team2.name;
                
                int t1Score = match.getTeamScore(match.matchResults, t1Name);
                int t2Score = match.getTeamScore(match.matchResults, t2Name);

                formattedGroupString += t1Name + " - " + t2Name + " : " + t1Score + " - " + t2Score + "\n";
            }

            formattedGroupString += "\n";

            formattedOutMessage += formattedGroupString;
        }

        public void FormatFinalsMatches(FinalsMatch finalsMatch, string matchName)
        {
            string formattedFinalsString = "";
            
            string t1Name = finalsMatch.team1.name;
            string t2Name = finalsMatch.team2.name;

            int t1Score = finalsMatch.getTeamScore(finalsMatch.matchResults, t1Name);
            int t2Score = finalsMatch.getTeamScore(finalsMatch.matchResults, t2Name);


            formattedFinalsString += "--" + matchName + "--\n";
            formattedFinalsString += t1Name + " - " + t2Name + " : " + t1Score + " - " + t2Score + "\n";
            

            formattedFinalsString += "\n";

            formattedOutMessage += formattedFinalsString;
        }

        public void FormatWinner(FinalsMatch finalsMatch)
        {
            string formattedWinnerString = "";

            formattedWinnerString += "Vinnaren är: " + finalsMatch.winningTeam.name + "\n";

            formattedOutMessage += formattedWinnerString;
        }


        private List<GroupMatch> SortGroupMatches(List<GroupMatch> listOfMatches, string groupID)
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

        public async Task PrintResultsToFile(string fileName)
        {
            string textToBePrintedtext = formattedOutMessage;

            string proposedFileName = fileName;

            if (fileNameHasCorrectFileExtension(proposedFileName))
            {
                await File.WriteAllTextAsync(proposedFileName, textToBePrintedtext);

            }
            else if (fileNameHasIncorrectFileExtension(proposedFileName))
            {
                string revisedFileName = changeToCorrectFileExtension(fileName);

                PrintResultsToFile(revisedFileName);
            }
            else
            {
                string revisedFileName = addCorrectFileExtension(fileName);

                PrintResultsToFile(revisedFileName);
            }

        }

        
        private bool fileNameHasCorrectFileExtension(string fileName)
        {


            return true;
        }

        private bool fileNameHasIncorrectFileExtension(string fileName)
        {

            return true;
        }

        private string changeToCorrectFileExtension(string fileName)
        {
            string adjustedFileName;



            return adjustedFileName;
        }

        private string addCorrectFileExtension(string fileName)
        {
            string adjustedFileName;



            return adjustedFileName;
        }

    }
}
