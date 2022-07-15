using CheckBoardGameVersion3.Data.Models;

namespace CheckBoardGameVersion3.Server.Data
{
    public class TableManager
    {
        public Dictionary<string, int> Tables = new();
        public Dictionary<string, SetTeam> SetColorTeam = new();
        public Dictionary<string, string> UserName = new();
    }
}
