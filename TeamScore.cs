using System;
using System.Collections.Generic;
using System.Text;

namespace FootballPredictor
{
    class TeamScore
    {
        public int score;
        
        public Team team;

        public TeamScore(int score, Team team)
        {
            this.score = score;
            this.team = team;
        }


    }
}
