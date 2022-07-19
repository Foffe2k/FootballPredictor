using System;
using System.Collections.Generic;
using System.Text;

namespace FootballPredictor
{
    class MissingTeamException : Exception
    {
        public MissingTeamException(): base(String.Format("Specified team is missing. Maybe check spelling?")) { }
              
    }
}
