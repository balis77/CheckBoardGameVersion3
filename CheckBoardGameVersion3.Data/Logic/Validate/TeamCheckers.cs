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
        public static SetTeam Dask { get; set; }
        public static SetTeam DaskOpponent { get; set; }
        public static SetTeam Player1 { get; set; } 
        public static SetTeam Player2 { get; set; }
        public TeamCheckers(SetTeam setTeam)
        {
            Player1 = setTeam;
        }
        public TeamCheckers() { }

        public static void SetPlayerGame(string set)
        {
            if (set == "1")
            {
                Player1 = SetTeam.White;
                Dask = SetTeam.White;
                DaskOpponent = SetTeam.Black;
                Player2 = SetTeam.Black;
            }
            else
            {
                Player1 = SetTeam.Black;
                Dask = SetTeam.Black;
                DaskOpponent = SetTeam.White;
                Player2 = SetTeam.White;
            }
        }
        public static SetTeam setTeam(SetTeam team)
           => team == SetTeam.White ? SetTeam.Black : SetTeam.White;
        
    }
}
