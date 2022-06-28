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

        public static void SetPlayerGame(string set)
        {
            if (set == "1")
            {
                Team = SetTeam.White;
                Bot = SetTeam.Black;
            }
            else
            {
                Team = SetTeam.Black;
                Bot = SetTeam.White;
            }
        }
        public static SetTeam setTeam(SetTeam team)
           => team == SetTeam.White ? SetTeam.Black : SetTeam.White;
        
    }
}
