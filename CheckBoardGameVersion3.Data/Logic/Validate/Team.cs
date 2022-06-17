using CheckBoardGameVersion3.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckBoardGameVersion3.Data.Logic.Validate
{
    public  class TeamCheckers
    {
        public static SetTeam Team { get; set; } = SetTeam.White;
        public TeamCheckers(SetTeam setTeam)
        {
            Team = setTeam;
        }
        public TeamCheckers()
        {

        }
        public static SetTeam setTeam(SetTeam team)
           => team == SetTeam.White ? SetTeam.Black : SetTeam.White;
    }
}
