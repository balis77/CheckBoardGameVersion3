﻿using CheckBoardGameVersion3.Data.Logic.Validate;
using CheckBoardGameVersion3.Data.Models;
using CheckBoardGameVersion3.Server.Data;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.AspNetCore.Components;
namespace CheckBoardGameVersion3.Server.Hubs
{
    public class BoardHub : Hub
    {
        private readonly TableManager _tableManager;
        NavigationManager NavigationManager;
        public BoardHub(TableManager tableManager)
        {
            _tableManager = tableManager;
        }

        public async Task SendMessage(string userName, string message, string roomName)
        {
            await Clients.Group(roomName).SendAsync("ReceiveMessage", userName, message);
        }

        public async Task ConnectionHub(HubConnection hubConnection)
        {
            _tableManager.hubConnection = hubConnection;
            //await Clients.All.SendAsync("HubInitilizationConnect",_tableManager.hubConnection);
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

        public async Task UpdateJoinTable()
        {
            await Clients.All.SendAsync("UpdateTable");
        }
        public async Task SetName(string tableId, string name)
        {
            _tableManager.UserName.Add(tableId, name);
        }
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var removeUserName = _tableManager.Tables.FirstOrDefault();
            const int PlayersInRoom = 2;
            if (removeUserName.Value == PlayersInRoom)
            {
                _tableManager.Tables[removeUserName.Key]--;
            }
            await base.OnDisconnectedAsync(exception);
        }
    }
}
