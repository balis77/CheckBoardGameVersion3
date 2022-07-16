using CheckBoardGameVersion3.Data.Logic.Validate;
using CheckBoardGameVersion3.Data.Models;
using CheckBoardGameVersion3.Server.Data;
using Microsoft.AspNetCore.SignalR;

namespace CheckBoardGameVersion3.Server.Hubs
{
    public class BoardHub : Hub
    {
        private readonly TableManager _tableManager;

        public BoardHub(TableManager tableManager)
        {
           _tableManager = tableManager;
        }

        public async Task SendMessage(string userName, string message, string roomName)
        {
            await Clients.Group(roomName).SendAsync("ReceiveMessage", userName, message);
        }
        public async Task JoinTable(string tableId)
        {
            if (_tableManager.Tables.ContainsKey(tableId))
            {
                if (_tableManager.Tables[tableId] < 2)
                {
                    await Groups.AddToGroupAsync(Context.ConnectionId, tableId);
                    _tableManager.Tables[tableId]++;
                }
            }
            else
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, tableId);
                _tableManager.Tables.Add(tableId, 1);
            }
        }

        public async Task Move(string tableId, Dictionary<string, Cell> board, SetTeam playerMove)
        {
            await Clients.GroupExcept(tableId, Context.ConnectionId).SendAsync("UpdateBoardOpponent", board, playerMove);
        }

        public async Task SetSecondPlayerColorDask(string tableId, SetTeam setPlayer)
        {
            if (!_tableManager.SetColorTeam.ContainsKey(tableId))
            {
                _tableManager.SetColorTeam.Add(tableId, setPlayer);
            }
        }

        public async Task SetName(string tableId,string name)
        {
            _tableManager.UserName.Add(tableId, name);
        }
    }
}
