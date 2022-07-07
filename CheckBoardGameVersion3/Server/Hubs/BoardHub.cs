using CheckBoardGameVersion3.Data.Models;
using Microsoft.AspNetCore.SignalR;

namespace CheckBoardGameVersion3.Server.Hubs
{
    public class BoardHub:Hub
    {
        public async Task SendMessage(Dictionary<string,Cell> board,SetTeam player,SetTeam dask)
        {
            await Clients.All.SendAsync("ReceiveMessage", board,player,dask);
        }

    }
}
