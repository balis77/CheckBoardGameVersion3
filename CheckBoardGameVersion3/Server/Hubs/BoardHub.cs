using CheckBoardGameVersion3.Data.Logic.Validate;
using CheckBoardGameVersion3.Data.Models;
using CheckBoardGameVersion3.Server.Data;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.AspNetCore.Components;
using System.Text.Json;
using Microsoft.JSInterop;

namespace CheckBoardGameVersion3.Server.Hubs
{
    public class BoardHub : Hub
    {
        private readonly TableManager _tablesManager;
        
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

        public async Task SetPlayerMove(string tableId,Dictionary<string,Cell> board)
        {
            var playerTableId = _tablesManager.NameUser.FirstOrDefault(n => n.TableId == tableId);
            await Clients.Group(tableId).SendAsync("UpdateBoardOpponent", board, playerTableId.ColorMove);
        }
        public async Task Move(string tableId, Dictionary<string, Cell> board, SetTeam playerMove)
        {
            var playerTableId = _tablesManager.NameUser.FirstOrDefault(n => n.TableId == tableId);
            playerTableId.ColorMove = playerMove;
            await Clients.Group(tableId).SendAsync("UpdateBoardOpponent", board, playerMove);
        }

        public async Task SetSecondPlayerColorDask(string tableId, string setPlayer)
        {
            if (!_tablesManager.SetColorTeam.ContainsKey(tableId))
            {
                _tablesManager.SetColorTeam.Add(tableId, setPlayer);
            }
        }

        public async Task UpdateJoinTable(Dictionary<string,Cell> board)
        {
            await Clients.All.SendAsync("UpdateTable",board);
        }
        public async Task SetName(string tableId, string name)
        {
            _tablesManager.KeyUser.Add(tableId, name);
        }

        public async Task Save(string tableId, Dictionary<string, Cell> board)
        {
            await Clients.Group(tableId).SendAsync("SaveBoard", tableId, board);
        }

        public async Task Read(string tableId)
        {
            await Clients.Group(tableId).SendAsync("ReadBoard", tableId);
        }

        public async Task Delete(string tableId,Dictionary<string,Cell> board)
        {
            await Clients.Group(tableId).SendAsync("DeleteBoard",tableId,board);
        }

        public async Task SaveCount(string tableId,int blackCount,int whiteCount)
        {
            _tablesManager.NameUser.FirstOrDefault(n => n.TableId == tableId).CountBlack = blackCount;
            _tablesManager.NameUser.FirstOrDefault(n => n.TableId == tableId).CountWhite = whiteCount;
            await Clients.Group(tableId).SendAsync("CountSave",blackCount,whiteCount);
        }
        public async Task LoadCount(string tableId)
        {
            int blackCount = _tablesManager.NameUser.FirstOrDefault(n => n.TableId == tableId).CountBlack;
            int whiteCount = _tablesManager.NameUser.FirstOrDefault(n => n.TableId == tableId).CountWhite;
            await Clients.Group(tableId).SendAsync("CountSave", blackCount, whiteCount);
        }
      
        public  override Task OnDisconnectedAsync(Exception? exception)
        {
            Clients.AllExcept(Context.ConnectionId).SendAsync("DisconnectUser");

            return base.OnDisconnectedAsync(exception);
        }
    }
}
