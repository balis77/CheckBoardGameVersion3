using CheckBoardGameVersion3.Data.InformationDask;
using CheckBoardGameVersion3.Data.Logic.Bot;
using CheckBoardGameVersion3.Data.Logic.Validate;
using CheckBoardGameVersion3.Data.Logic.Validate.ValidateBoard;
using CheckBoardGameVersion3.Data.Models;
using CheckBoardGameVersion3.Data.Models.Enums;
using CheckBoardGameVersion3.Data.RepositoryBoard;
using ConsoleApp2.Logic.GameActions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.JSInterop;

using System.Text.Json;

namespace CheckBoardGameVersion3.Client.Pages
{
    public partial class CheckBoard
    {
        public Dictionary<string, Cell> Board { get; set; } = new Dictionary<string, Cell>();
        private MockRepositoryBoard _repositoryBoard;
        private ActionCheaker _actionCheaker;
        private QueenCheaker _queenCheaker;
        private ValidateBoard _validateBoard;
       // private BotChecker _botChecker;
        private BoardInformation _boardInformation;


        private HubConnection? hubConnection;


        private async Task Send()
        {
            if (hubConnection is not null)
            {
                await hubConnection.SendAsync("SendMessage", Board,TeamCheckers.Player2,TeamCheckers.DaskOpponent);
            }
        }

        public bool IsConnected =>
            hubConnection?.State == HubConnectionState.Connected;

        public async ValueTask DisposeAsync()
        {
            if (hubConnection is not null)
            {
                await hubConnection.DisposeAsync();
            }
        }
        protected override async Task OnInitializedAsync()
        {
            _actionCheaker = new ActionCheaker();
            _queenCheaker = new QueenCheaker();
            _repositoryBoard = new MockRepositoryBoard();
            _validateBoard = new ValidateBoard();
            _boardInformation = new BoardInformation();
            TeamCheckers.SetPlayerGame(player);
            Board = _repositoryBoard.CreateDesk();
            await Read();
            hubConnection = new HubConnectionBuilder()
          .WithUrl(NavigationManager.ToAbsoluteUri("/chathub"))
          .Build();
            
            hubConnection.On<Dictionary<string, Cell>, SetTeam,SetTeam>("ReceiveMessage", (Board,player,dask) =>
            {
                this.Board = Board;
                TeamCheckers.Player1 = player;
                TeamCheckers.Dask = dask;
                InvokeAsync(StateHasChanged);
            });
            
            await hubConnection.StartAsync();

        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await Save();
        }

        public Dictionary<string, Cell> MoveAnalise(Dictionary<string, Cell> board, Cell clickChecker)
        {
            board = _validateBoard.ValidateFullBoard(board);

            if (clickChecker.Checker?.Color == CheckerColor.BlackQueen
                || clickChecker.Checker?.Color == CheckerColor.WhiteQueen)
            {
                foreach (var cell in board)
                {
                    if (cell.Value.LockChecker && !clickChecker.ClickChecker)
                    {
                        board = _queenCheaker.AnaliseMoveAndBeatQueen(Board, cell.Value);

                    }
                }

                var lockchecker = board.FirstOrDefault(n => n.Value.LockChecker);

                if (lockchecker.Key != null)
                {
                    return board;
                }

                board = _queenCheaker.AnaliseMoveAndBeatQueen(Board, clickChecker);
            }
            else
            {
                foreach (var cell in board)
                {
                    if (cell.Value.LockChecker && !clickChecker.ClickChecker)
                    {
                        board = _actionCheaker.AnaliseCanMoveAndBeat(Board, cell.Value);

                    }
                }
                var lockchecker = board.FirstOrDefault(n => n.Value.LockChecker);

                if (lockchecker.Key != null)
                {
                    return board;
                }

                board = _actionCheaker.AnaliseCanMoveAndBeat(Board, clickChecker);
            }

            return board;
        }

        public Dictionary<string, Cell> MoveAndBeatChecker(Dictionary<string, Cell> board, Cell clickCell)
        {
            board = _boardInformation.GameOver(Board);
            var checkerClick = board.FirstOrDefault(n => n.Value.ClickChecker);

            if (clickCell.Checker != null || checkerClick.Key == null)
            {
                return board;
            }

            if (checkerClick.Value?.Checker?.Color == CheckerColor.BlackQueen
                || checkerClick.Value?.Checker?.Color == CheckerColor.WhiteQueen)
            {
                board = _queenCheaker.MoveAndBeatQueen(Board, clickCell);
            }
            else
            {
                board = _actionCheaker.MoveAndBeatCheckers(Board, clickCell);
               
            }
            if (TeamCheckers.Player1 == TeamCheckers.Player2)
            {
                //_botChecker = new BotChecker(board);
                //board = _botChecker.LogicBotMove(board);
              
            }
            Send();
            return board;
        }


        public async Task Save()
        {
            string jsonFileBoard = JsonSerializer.Serialize(Board);
            await JSRuntime.InvokeVoidAsync("localStorage.setItem", "CheckBoard", jsonFileBoard);
        }

        public async Task Read()
        {
            string currentInputValue = await JSRuntime.InvokeAsync<string>("localStorage.getItem", "CheckBoard");

            if (currentInputValue == null)
                return;

            var jsonFileBoard = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, Cell>>(currentInputValue);

            if (!jsonFileBoard.Equals(null))
                Board = jsonFileBoard;
        }

        public async Task Delete()
        {
            await JSRuntime.InvokeAsync<string>("localStorage.removeItem", "CheckBoard");
            Board = _repositoryBoard.CreateDesk();
        }
    }



}
