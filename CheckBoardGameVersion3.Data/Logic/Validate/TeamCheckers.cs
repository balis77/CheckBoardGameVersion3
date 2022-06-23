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
        public static SetTeam Team { get; set; } 
        public static SetTeam Player { get; set; }
        public static SetTeam Bot { get; set; }
        public TeamCheckers(SetTeam setTeam)
        {
            Team = setTeam;
        }
        public TeamCheckers() { }

        public static void SetPlayerGame(SetTeam team)
        {
            SetTeam setBot = SetTeam.White == team ? SetTeam.Black : SetTeam.White;
            Team = team;
            Bot = setBot;
        }
        public static SetTeam setTeam(SetTeam team)
           => team == SetTeam.White ? SetTeam.Black : SetTeam.White;
    }
}
