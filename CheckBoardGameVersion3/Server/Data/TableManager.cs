using CheckBoardGameVersion3.Data.Models;

namespace CheckBoardGameVersion3.Server.Data
{
    public class TableManager
    {
        public Dictionary<string, int> Tables = new();
        public List<SetTeam> setTeams = new List<SetTeam>(); 
    }
}
