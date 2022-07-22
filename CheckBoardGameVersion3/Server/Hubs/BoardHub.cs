using CheckBoardGameVersion3.Data.Logic.Validate;
using CheckBoardGameVersion3.Data.Models;
using CheckBoardGameVersion3.Server.Data;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.AspNetCore.Components;
namespace CheckBoardGameVersion3.Server.Hubs
{
    public class BoardHub : Hub
    {
        private readonly TableManager _tablesManager;
        NavigationManager NavigationManager;
        public BoardHub(TableManager tableManager)
        {
            _tablesManager = tableManager;
        }

        public async Task SendMessage(string userName, string message, string roomName)
        {
            await Clients.Group(roomName).SendAsync("ReceiveMessage", userName, message);
        }
        public async Task JoinBoard(string tableId, string userName)
        {
            UsersInTable user = new UsersInTable();
            if (_tablesManager.Tables.ContainsKey(tableId))
            {
                if (_tablesManager.Tables[tableId] < 3)
                {
                    bool repetitionUser = false;
                    foreach (var table in _tablesManager.NameUser)
                    {
                        if (table.Name == userName)
                        {
                            repetitionUser = true;
                        }
                    }
                    if (!repetitionUser)
                    {
                        var number = _tablesManager.NameUser.Count();
                        user.AddUser(tableId, userName, number++);
                        _tablesManager.NameUser.Add(user);
                        _tablesManager.Tables[tableId]++;
                    }
                    await Groups.AddToGroupAsync(Context.ConnectionId, tableId);
                }
            }
            else
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, tableId);
                _tablesManager.Tables.Add(tableId, 1);

                user.AddUser(tableId, userName, 1);
                _tablesManager.NameUser.Add(user);
            }
        }

        public async Task JoinTable(string tableId)
        {
           

            if (_tablesManager.Tables.ContainsKey(tableId))
            { 
                if (_tablesManager.Tables[tableId] < 2)
                {
                    await Groups.AddToGroupAsync(Context.ConnectionId, tableId);
                    _tablesManager.Tables[tableId]++;
                }
            }
            else
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, tableId);
                _tablesManager.Tables.Add(tableId, 1);
            }
        }

        public async Task Move(string tableId, Dictionary<string, Cell> board, SetTeam playerMove)
        {
            await Clients.GroupExcept(tableId, Context.ConnectionId).SendAsync("UpdateBoardOpponent", board, playerMove);
        }

        public async Task SetSecondPlayerColorDask(string tableId, string setPlayer)
        {
            if (!_tablesManager.SetColorTeam.ContainsKey(tableId))
            {
                _tablesManager.SetColorTeam.Add(tableId, setPlayer);
            }
        }

        public async Task UpdateJoinTable()
        {
            await Clients.All.SendAsync("UpdateTable");
        }
        public async Task SetName(string tableId, string name)
        {
            _tablesManager.UserName.Add(tableId, name);
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var removeUserName = _tablesManager.Tables.FirstOrDefault();
            const int PlayersInRoom = 2;
            if (removeUserName.Value == PlayersInRoom)
            {
                _tablesManager.Tables[removeUserName.Key]--;
            }
            await base.OnDisconnectedAsync(exception);
        }
    }
}
