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
        public static SetTeam PlayerMove { get; set; }
      

        public TeamCheckers(SetTeam setTeam)
        {
            PlayerMove = setTeam;
        }
        public TeamCheckers() { }

        public static void SetPlayerGame(SetTeam set)
        {
            PlayerMove = SetTeam.White;
            Dask = set;
        }
        public static SetTeam setTeam(SetTeam team)
           => team == SetTeam.White ? SetTeam.Black : SetTeam.White;
       
    }
}
