﻿using CheckBoardGameVersion3.Data.Models;
using Microsoft.AspNetCore.SignalR.Client;

namespace CheckBoardGameVersion3.Server.Data
{
    public class TableManager
    {
        public Dictionary<string, int> Tables = new();
        public Dictionary<string, string> SetColorTeam = new();
        public Dictionary<string, string> KeyUser = new();
        public List<UsersInTable> NameUser = new();

    }

    public class UsersInTable
    {
        public int Number { get; set; }
        public string Name { get; set; }
        public string TableId { get; set; }

        public SetTeam ColorMove { get; set; }

        public int CountBlack { get; set; }

        public int CountWhite { get; set; }
        public Dictionary<string,Cell> Board { get; set; }
        public void AddUser(string tableId, string name, int number)
        {
            Name = name;
            TableId = tableId;
            Number = number;

        }
    }
}
