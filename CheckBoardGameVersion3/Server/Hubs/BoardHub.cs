using CheckBoardGameVersion3.Data.Models;
using CheckBoardGameVersion3.Server.Data;
using Microsoft.AspNetCore.SignalR;

namespace CheckBoardGameVersion3.Server.Hubs
{
    public class BoardHub : Hub
    {
        private readonly TableManager tableManager;

        public BoardHub(TableManager tableManager)
        {
            this.tableManager = tableManager;
        }

        public async Task JoinTable(string tableId)
        {
            if (tableManager.Tables.ContainsKey(tableId))
            {
                if (tableManager.Tables[tableId] < 2)
                {
                    await Groups.AddToGroupAsync(Context.ConnectionId, tableId);

                    await Clients.GroupExcept(tableId, Context.ConnectionId).SendAsync("TableJoined");
                    tableManager.Tables[tableId]++;
                }
            }
            else
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, tableId);
                tableManager.Tables.Add(tableId, 1);
            }
        }
        public async Task Move(string tableId,Dictionary<string,Cell> board)
        {
            await Clients.GroupExcept(tableId, Context.ConnectionId).SendAsync("Move", board);
        }
    }
}
