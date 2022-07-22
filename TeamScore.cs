using System;
using System.Collections.Generic;
using System.Text;

namespace FootballPredictor
{
    class TeamScore
    {
        public int score { get; private set; }        
        public Team team { get; private set; }
        
        public TeamScore(int score, Team team)
        {
            this.score = score;
            this.team = team;
        }

        public string GetTeamName()
        {
            return team.name;
        }
    }
}
