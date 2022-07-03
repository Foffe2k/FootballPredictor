using System;
using System.Collections.Generic;
using System.Text;

namespace FootballPredictor
{
    class Match
    {
        public Team team1;
        public Team team2;

        public String matchName;
        
        public int scoreTeam1;
        public int scoreTeam2;
        
        public Match(Team team1, Team team2, string matchName)
        {
            this.team1 = team1;
            this.team1 = team2;
            this.matchName = matchName;
        }
    }
}
