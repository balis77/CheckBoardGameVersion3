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
using System.Net.Http.Json;
using System.Text.Json;

namespace CheckBoardGameVersion3.Client.Pages
{
    public partial class CheckBoard
    {
        public const int MAXIMUN_SCORE = 3;
        public const int LACK_OF_CHECKER = 0;

        public Dictionary<string, Cell> Board { get; set; } = new Dictionary<string, Cell>();
        private MockRepositoryBoard _repositoryBoard;
        private ActionCheaker _actionCheaker;
        private QueenCheaker _queenCheaker;
        private ValidateBoard _validateBoard;
        private BoardInformation _boardInformation;


        
        public int CountWhite { get; set; }
        public int CountBlack { get; set; }
        protected override async Task OnInitializedAsync()
        {
            HubConnect = new HubConnectionBuilder()
            .WithUrl(NavigationManager.ToAbsoluteUri("/BoardHub"))
            .Build();

            GenerationBoardAndCheckers();
            await HubConnect.StartAsync();
            await JoinAndCreateTable();
            TeamCheckers.SetPlayerGame(Dask);

            EventHandlerHubConnection();
            HubConnect.SendAsync("SetPlayerMove", TableId, Board);
            await Read();
        }

        private async Task EventHandlerHubConnection()
        {
            HubConnect.On<string, string>("ReceiveMessage", (nickName, message) =>
            {
                var encodedMsg = $"{nickName}: {message}";
                Messages.Add(encodedMsg);
                MessageInput = null;
                InvokeAsync(StateHasChanged);
            });

            HubConnect.On<Dictionary<string, Cell>, SetTeam>("UpdateBoardOpponent", (board, player) =>
            {
                Board = board;
                TeamCheckers.PlayerMove = player;
                InvokeAsync(StateHasChanged);
            });
            HubConnect.On<Dictionary<string, Cell>, SetTeam>("SetDaskColor", (board, player) =>
            {
                Board = board;
                TeamCheckers.PlayerMove = player;
                InvokeAsync(StateHasChanged);
            });
            HubConnect.On<string, Dictionary<string, Cell>>("SaveBoard", (tableId, board) =>
            {
                string jsonFileBoard = JsonSerializer.Serialize(board);
                JSRuntime.InvokeVoidAsync("localStorage.setItem", $"{tableId}", jsonFileBoard);
                InvokeAsync(StateHasChanged);
            });
            HubConnect.On<string>("ReadBoard", async (tableId) =>
            {
                string currentInputValue = await JSRuntime.InvokeAsync<string>("localStorage.getItem", $"{tableId}");

                if (currentInputValue == null)
                    return;

                var jsonFileBoard = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, Cell>>(currentInputValue);

                if (!jsonFileBoard.Equals(null))
                    Board = jsonFileBoard;

                await InvokeAsync(StateHasChanged);
            });
            HubConnect.On<string, Dictionary<string, Cell>>("DeleteBoard", (tableId, board) =>
            {
                JSRuntime.InvokeAsync<string>("localStorage.removeItem", $"{tableId}");
                Board = _repositoryBoard.CreateDesk();
                TeamCheckers.SetPlayerGame(Dask);
                HubConnect.SendAsync("Move", TableId, Board, TeamCheckers.PlayerMove);
                InvokeAsync(StateHasChanged);
            });
            HubConnect.On<int, int>("CountSave", ( blackCount, whiteCount) =>
            {
                CountBlack = blackCount;
                CountWhite = whiteCount;
                InvokeAsync(StateHasChanged);
            });
        }

        private void GenerationBoardAndCheckers()
        {
            _actionCheaker = new ActionCheaker();
            _queenCheaker = new QueenCheaker();
            _repositoryBoard = new MockRepositoryBoard();
            _validateBoard = new ValidateBoard();
            _boardInformation = new BoardInformation();
            Board = _repositoryBoard.CreateDesk();
        }

        private async Task JoinAndCreateTable()
        {
            await HubConnect.SendAsync("JoinBoard", TableId, User);
            await HubConnect.SendAsync("SetSecondPlayerColorDask", TableId, Dask);
            await HubConnect.SendAsync("LoadCount",TableId);
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
       
        public void GameOver( string player)
        {
            int numberBlackChecker = Board.Where(n => n.Value?.Checker?.Team == SetTeam.Black).Count();
            int numberWhiteChecker = Board.Where(n => n.Value?.Checker?.Team == SetTeam.White).Count();

            if (numberBlackChecker.Equals(LACK_OF_CHECKER))
            {
                CountWhite++;
                Board = _repositoryBoard.CreateDesk();
                TeamCheckers.SetPlayerGame(player);
                HubConnect.SendAsync("Move", TableId, Board, TeamCheckers.PlayerMove);
                HubConnect.SendAsync("SaveCount", TableId, CountBlack, CountWhite);
            }
            if (numberWhiteChecker.Equals(LACK_OF_CHECKER))
            {
                CountBlack++;
                Board = _repositoryBoard.CreateDesk();
                TeamCheckers.SetPlayerGame(player);
                HubConnect.SendAsync("Move", TableId, Board, TeamCheckers.PlayerMove);
                HubConnect.SendAsync("SaveCount", TableId, CountBlack, CountWhite);
            }
            if (CountBlack.Equals(MAXIMUN_SCORE) || CountWhite.Equals(MAXIMUN_SCORE))
            {
                CountBlack = 0;
                CountWhite = 0;
            }
            InvokeAsync(StateHasChanged);
        }
        public Dictionary<string, Cell> MoveAndBeatChecker(Dictionary<string, Cell> board, Cell clickCell)
        {
            GameOver(Dask);
            var checkerClick = board.FirstOrDefault(n => n.Value.ClickChecker);

            if (clickCell.Checker != null || checkerClick.Key == null)
                return board;
            

            if (checkerClick.Value?.Checker?.Color == CheckerColor.BlackQueen
                || checkerClick.Value?.Checker?.Color == CheckerColor.WhiteQueen)
                board = _queenCheaker.MoveAndBeatQueen(Board, clickCell);
            else
                board = _actionCheaker.MoveAndBeatCheckers(Board, clickCell);
            
            HubConnect.SendAsync("Move", TableId, Board, TeamCheckers.PlayerMove);
            Save();

            return board;
        }
        public async Task Save()
        {
            await HubConnect.SendAsync("Save", TableId, Board);
        }

        public async Task Read()
        {
            await HubConnect.SendAsync("Read", TableId);
        }
        public async Task Delete()
        {
            await HubConnect.SendAsync("Delete", TableId, Board);
        }
    }



}
