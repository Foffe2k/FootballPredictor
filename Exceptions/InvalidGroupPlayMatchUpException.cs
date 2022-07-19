using System;
using System.Collections.Generic;
using System.Text;

namespace FootballPredictor
{
    class InvalidGroupPlayMatchUpException : Exception
    {
        public InvalidGroupPlayMatchUpException(): base(String.Format("Opposing teams are from different groups")) { }
              
    }
}
