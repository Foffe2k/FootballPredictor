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

        private string fileName;

        private Tournament currentTournament;

        private string formattedOutMessage;

        public ResultsPrinter(Tournament tournament)
        {
            currentTournament = tournament;
            fileName = CORRECT_FILENAME + CORRECT_FILE_EXTENSION;
        }

        public void FormatTournamentResults(List<GroupMatch> listOfGroupMatches, List<FinalsMatch> listOfFinalsMatches)
        {
            FormatGroupMatches(listOfGroupMatches, Group.A);
            FormatGroupMatches(listOfGroupMatches, Group.B);
            FormatGroupMatches(listOfGroupMatches, Group.C);
            FormatGroupMatches(listOfGroupMatches, Group.D);

            FormatFinalsMatches(listOfFinalsMatches); 

            FormatWinnerOfTournament(listOfFinalsMatches);
        }

        private void FormatGroupMatches(List<GroupMatch> listOfGroupMatches, Group groupID)
        {            
            string formattedGroupString = "";

            List<GroupMatch> groupMatches = GetGroupMatches(listOfGroupMatches, groupID);
            
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

        private List<GroupMatch> GetGroupMatches(List<GroupMatch> listOfGroupMatches, Group groupID)
        {
            List<GroupMatch> matches = new List<GroupMatch>();

            foreach (GroupMatch match in listOfGroupMatches)
            {
                if (MatchIsPartOfCurrentGroup(match, groupID))
                {
                    matches.Add(match);
                }
            }
            return matches;
        }

        private bool MatchIsPartOfCurrentGroup(GroupMatch match, Group groupID)
        {
            return match.GetGroupID().Equals(groupID);
        }

        private void FormatFinalsMatches(List<FinalsMatch> listOfFinalsMatches) 
        {
            foreach (FinalsMatch match in listOfFinalsMatches)
            {
                FormatFinalsMatch(match);
            }
        }

        private void FormatFinalsMatch(FinalsMatch finalsMatch)
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

        private void FormatWinnerOfTournament(List<FinalsMatch> listOfFinalsMatches)
        {
            FinalsMatch finalMatch = listOfFinalsMatches.Single(result => result.matchName.Equals("Final"));

            string formattedWinnerString = "";

            formattedWinnerString += "Vinnaren är: " + finalMatch.GetWinningTeamName() + "\n";

            formattedOutMessage += formattedWinnerString;
        } 
       

        public async Task PrintResultsToFile()
        {
            await File.WriteAllTextAsync(fileName, formattedOutMessage);
        }

    }
}
